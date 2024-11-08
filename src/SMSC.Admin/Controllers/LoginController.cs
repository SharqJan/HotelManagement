using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SMSC.Application.DTO;
using SMSC.Application.Queries.Login;
using SMSC.Application.Responses;
using SMSC.Core.Logger.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using System;

namespace SMSC.Admin.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILog _logger;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IMediator _mediator;

        public LoginController(ILog logger, IStringLocalizer<SharedResources> localizer, IMediator mediator)
        {
            _logger = logger;
            _localizer = localizer;
            _mediator = mediator;
        }
        public IActionResult Index(CancellationToken token)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> LoginEmail(CancellationToken token, LoginDto loginDto)
        {
            var loginQuery = new LoginQuery
            {
                LoginDto = loginDto
            };

            var loginResult = await _mediator.Send(loginQuery, token);

            if (loginResult.IsSuccess && loginResult.Value.Any())
            {
                var user = loginResult.Value.First();

                bool authenticUser = VerifyPassword(loginDto.Password, user.Password); 

                if (authenticUser)
                {
                    var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                // new Claim(ClaimTypes.Name, user.Name) 
            };

                    var claimsIdentity = new ClaimsIdentity(claims, "login");

                    await HttpContext.SignInAsync(new ClaimsPrincipal(claimsIdentity));

                    HttpContext.Session.SetString("UserName", user.FirstName + " " + user.LastName);
                    HttpContext.Session.SetInt32("UserId", user.UserId);
                    //For Testing
                    var UserName = HttpContext.Session.GetString("UserName");
                    int? UserId = HttpContext.Session.GetInt32("UserId");

                    var response = ResponseMapper.MapResponse(loginResult, loginResult.IsSuccess ? loginResult.Value : null);
                   
                    return Json(response);
                   
                }
                else
                {
                    return Json(new { success = false, message = "Incorrect Password " });
                }
            }

            return Json(new { success = false, message = "This Email doesn't exist in our system" });
        }



        [HttpGet]
        public async Task<IActionResult> SignOut(CancellationToken token)
        {
            await HttpContext.SignOutAsync();
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");

        }

  




        public bool VerifyPassword(string inputPassword, string storedHash)
        {
            
            using (MD5 md5 = MD5.Create())
            {
                byte[] hashBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(inputPassword));
                StringBuilder hashString = new StringBuilder();

                foreach (byte b in hashBytes)
                {
                    hashString.Append(b.ToString("x2")); 
                }

                
                return hashString.ToString().Equals(storedHash, StringComparison.OrdinalIgnoreCase);
            }
        }












    }


}



