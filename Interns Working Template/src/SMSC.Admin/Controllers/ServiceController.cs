using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SMSC.Application.Commands.Service;
using SMSC.Application.DTO;
using SMSC.Application.Queries.Service;
using SMSC.Application.Responses;
using SMSC.Core.Logger.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace SMSC.Admin.Controllers
{
    [Authorize(Policy = "RequireEmail")]
    public class ServiceController : Controller
    {
        private readonly ILog _logger;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IMediator _mediator;

        public ServiceController(ILog logger, IStringLocalizer<SharedResources> localizer, IMediator mediator)
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
        public async Task<JsonResult> AddService(CancellationToken token, ServiceDTO serviceDto)
        {
            var addServiceCommand = new AddServiceCommand
            {
                ServiceDTO = serviceDto
            };

            var roleService = await _mediator.Send(addServiceCommand, token);
            var response = ResponseMapper.MapResponse(roleService, roleService.IsSuccess ? roleService.Value : null);
            return Json(response);
        }

        [HttpGet]
        public async Task<JsonResult> GetServiceById(CancellationToken cancellationToken, int ServiceId)
        {
            var getServiceByIdQuery = new GetServiceByIdQuery
            {
                ServiceId = ServiceId
            };

            var resultServiceDto = await _mediator.Send(getServiceByIdQuery, cancellationToken);
            var response = ResponseMapper.MapResponse(resultServiceDto, resultServiceDto.IsSuccess ? resultServiceDto.Value : null);
            return Json(response);
        }


        [HttpGet]
        public async Task<JsonResult> GetServiceList(CancellationToken cancellationToken, ServiceDTO serviceDTO)
        {

            var getServiceListQuery = new GetServiceListQuery
            {
                ServiceDTO = serviceDTO
            };

            var resultServiceDto = await _mediator.Send(getServiceListQuery, cancellationToken);
            var response = ResponseMapper.MapResponse(resultServiceDto, resultServiceDto.IsSuccess ? resultServiceDto.Value : null);
            return Json(response);
        }


        public async Task<JsonResult> UpdateService(CancellationToken cancellationToken, ServiceDTO serviceDto)
        {

            var updateServiceCommand = new UpdateServiceCommand
            {
                ServiceDTO = serviceDto
            };

            var roleService = await _mediator.Send(updateServiceCommand, cancellationToken);
            var response = ResponseMapper.MapResponse(roleService, roleService.IsSuccess ? roleService.Value : null);
            return Json(response);
        }

        public async Task<JsonResult> DeleteServiceById(CancellationToken cancellationToken, int ServiceId)
        {

            var deleteServiceByIdCommand = new DeleteServiceByIdCommand
            {
                ServiceId = ServiceId
            };

            var resultServiceDto = await _mediator.Send(deleteServiceByIdCommand, cancellationToken);
            var response = ResponseMapper.MapResponse(resultServiceDto, resultServiceDto.IsSuccess ? resultServiceDto.Value : null);
            return Json(response);
        }
    }
}

