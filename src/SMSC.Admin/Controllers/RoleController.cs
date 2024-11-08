using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SMSC.Application.Commands.Role;
using SMSC.Application.DTO;
using SMSC.Application.Queries.Role;
using SMSC.Application.Responses;
using SMSC.Core.Logger.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace SMSC.Admin.Controllers
{
    [Authorize(Policy = "RequireEmail")]
    public class RoleController : Controller
    {
        private readonly ILog _logger;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IMediator _mediator;

        public RoleController(ILog logger, IStringLocalizer<SharedResources> localizer, IMediator mediator)
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
        public async Task<JsonResult> AddRole(CancellationToken token, RoleDTO roleDto)
        {
            roleDto.CreatedBy = (int) HttpContext.Session.GetInt32("UserId");

            var addRoleCommand = new AddRoleCommand
            {
                RoleDTO = roleDto
            };

            var roleResult = await _mediator.Send(addRoleCommand, token);
            var response = ResponseMapper.MapResponse(roleResult, roleResult.IsSuccess ? roleResult.Value : null);
            return Json(response);
        }

        [HttpGet]
        public async Task<JsonResult> GetRoleById(CancellationToken cancellationToken, int RoleId)
        {
            var getRoleByIdQuery = new GetRoleByIdQuery
            {
                RoleId = RoleId
            };

            var resultRoleDto = await _mediator.Send(getRoleByIdQuery, cancellationToken);
            var response = ResponseMapper.MapResponse(resultRoleDto, resultRoleDto.IsSuccess ? resultRoleDto.Value : null);
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


        public async Task<JsonResult> UpdateRole(CancellationToken cancellationToken, RoleDTO roleDto)
        {

            var updateRoleCommand = new UpdateRoleCommand
            {
                RoleDTO = roleDto
            };

            var roleResult = await _mediator.Send(updateRoleCommand, cancellationToken);
            var response = ResponseMapper.MapResponse(roleResult, roleResult.IsSuccess ? roleResult.Value : null);
            return Json(response);
        }

        public async Task<JsonResult> DeleteRoleById(CancellationToken cancellationToken, int RoleId)
        {

            var deleteRoleByIdCommand = new DeleteRoleByIdCommand
            {
                RoleId = RoleId
            };

            var resultRoleDto = await _mediator.Send(deleteRoleByIdCommand, cancellationToken);
            var response = ResponseMapper.MapResponse(resultRoleDto, resultRoleDto.IsSuccess ? resultRoleDto.Value : null);
            return Json(response);
        }
    }
}
