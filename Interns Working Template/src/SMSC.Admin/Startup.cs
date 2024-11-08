using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using MediatR;
using SMSC.Infrastructure;
using Microsoft.AspNetCore.Mvc.Razor;
using SMSC.Application.Mapper;
using OfficeOpenXml;

namespace SMSC.Admin
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            services.AddDistributedMemoryCache(); // Required for session storage
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true; // Make the session cookie essential
            });
            services.AddControllersWithViews();
            services.AddHttpContextAccessor();

            // Configure localization
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddMvc()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization();

            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[] { "en-US", "hi-IN", "fr-FR" };
                options.SetDefaultCulture(supportedCultures[0])
                    .AddSupportedCultures(supportedCultures)
                    .AddSupportedUICultures(supportedCultures);
            });

            // Configure database context with command timeout
            var commandTimeout = Configuration.GetSection("AdminConfiguration:DBCommandTimeOut");
            services.AddDbContext<DbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                sqlServerOptions => sqlServerOptions.CommandTimeout(Convert.ToInt32(commandTimeout.Value))));

            services.AddInfrastructure();
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());

            // Configure authentication
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Login/Index"; // Set your login path
                    options.LogoutPath = "/Login/SignOut"; // Set your logout path
                });

            // Configure authorization policy
            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireEmail", policy =>
                    policy.RequireClaim(System.Security.Claims.ClaimTypes.Email)
                    );


            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            var supportedCultures = new[] { "en-US", "hi-IN", "fr-FR" };
            var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(supportedCultures[0])
                .AddSupportedCultures(supportedCultures)
                .AddSupportedUICultures(supportedCultures);

            app.UseRequestLocalization(localizationOptions);

            app.UseStaticFiles();
            app.UseRouting();
            app.UseSession();
            app.UseAuthentication(); // Enable authentication
            app.UseAuthorization(); // Enable authorization

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Login}/{action=Index}/{id?}"); // Default route
            });
        }
    }
}
