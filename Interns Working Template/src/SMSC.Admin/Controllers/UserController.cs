using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SMSC.Application.Commands.User;
using SMSC.Application.DTO;
using SMSC.Application.Queries.Role;
using SMSC.Application.Queries.User;
using SMSC.Application.Responses;
using SMSC.Core.Logger.Interfaces;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SMSC.Admin.Controllers
{
    [Authorize(Policy = "RequireEmail")]
    public class UserController : Controller
    {
        private readonly ILog _logger;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IMediator _mediator;

        public UserController(ILog logger, IStringLocalizer<SharedResources> localizer, IMediator mediator)
        {
            _logger = logger;
            _localizer = localizer;
            _mediator = mediator;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> AddUser(CancellationToken token, UserDTO userDTO)
        {

            if (Request.Form.Files.Count > 0)
            {
                IFormFile file = Request.Form.Files[0];
                if (file != null && file.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await file.CopyToAsync(memoryStream);
                        userDTO.ProfileImage = memoryStream.ToArray();
                    }
                }
            }

            userDTO.Password = EncryptMd5(userDTO.Password);
            userDTO.CreatedBy = (int)HttpContext.Session.GetInt32("UserId");

            var addUserCommand = new AddUserCommand
            {
                UserDTO = userDTO
            };

            var userResult = await _mediator.Send(addUserCommand, token);
            var response = ResponseMapper.MapResponse(userResult, userResult.IsSuccess ? userResult.Value : null);
            return Json(response);
        }





        [HttpPost]
        public async Task<JsonResult> UpdateUser(CancellationToken token, UserDTO userDTO)


        {

            if (Request.Form.Files.Count > 0)
            {
                IFormFile file = Request.Form.Files[0];
                if (file != null && file.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await file.CopyToAsync(memoryStream);
                        userDTO.ProfileImage = memoryStream.ToArray();
                    }
                }
            }
            userDTO.Password = EncryptMd5(userDTO.Password);
            var updateUserCommand = new UpdateUserCommand
            {
                UserDTO = userDTO
            };

            var userResult = await _mediator.Send(updateUserCommand, token);
            var response = ResponseMapper.MapResponse(userResult, userResult.IsSuccess ? userResult.Value : null);
            return Json(response);
        }

        [HttpGet]
        public async Task<JsonResult> GetUserById(CancellationToken cancellationToken, int UserId)

        {
            var getUserByIdQuery = new GetUserByIdQuery
            {
                UserId = UserId
            };

            var resultUserDto = await _mediator.Send(getUserByIdQuery, cancellationToken);
            var response = ResponseMapper.MapResponse(resultUserDto, resultUserDto.IsSuccess ? resultUserDto.Value : null);
            return Json(response);
        }



        public async Task<JsonResult> GetUserList(CancellationToken cancellationToken)
        {

            var getUserListQuery = new GetUserListQuery();

            var resultUserDto = await _mediator.Send(getUserListQuery, cancellationToken);
            var response = ResponseMapper.MapResponse(resultUserDto, resultUserDto.IsSuccess ? resultUserDto.Value : null);
            return Json(response);
        }




        public async Task<JsonResult> DeleteUserById(CancellationToken cancellationToken, int userId)
        {

            var deleteUserByIdCommand = new DeleteUserByIdCommand
            {
                UserId = userId
            };

            var resultUserDto = await _mediator.Send(deleteUserByIdCommand, cancellationToken);
            var response = ResponseMapper.MapResponse(resultUserDto, resultUserDto.IsSuccess ? resultUserDto.Value : null);
            return Json(response);
        }





        [HttpGet]
        public async Task<JsonResult> GetRoleList(CancellationToken cancellationToken)
        {
            var getRoleListQuery = new GetRoleListQuery();

            var resultRoleDto = await _mediator.Send(getRoleListQuery, cancellationToken);
            var response = ResponseMapper.MapResponse(resultRoleDto, resultRoleDto.IsSuccess ? resultRoleDto.Value : null);
            return Json(response);
        }



        public string EncryptMd5(string password)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] hashBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder hashString = new StringBuilder();

                foreach (byte b in hashBytes)
                {
                    hashString.Append(b.ToString("x2")); // Convert to hexadecimal format
                }

                return hashString.ToString();
            }
        }



        /*   public bool VerifyPassword(string inputPassword, string storedHash)
           {
               // Hash the input password
               string hashedInput = EncryptMd5(inputPassword);
               // Compare the hashes
               return hashedInput.Equals(storedHash, StringComparison.OrdinalIgnoreCase);
           }*/

    }

}

