using Microsoft.Extensions.DependencyInjection;
using SMSC.Application.Interfaces;
using SMSC.Core.Interfaces;
using SMSC.Core.Logger.Interfaces;
using SMSC.Core.Logger.Services;
using SMSC.Core.Repositories;
using SMSC.Infrastructure.Repositories;

namespace SMSC.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<ILog, LogService>();
            services.AddTransient<IRepository, Repository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }
}
