using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SMSC.Application.DTO;
using SMSC.Application.Queries.InvoiceReport;
using SMSC.Application.Queries.Service;
using SMSC.Application.Queries.ServiceUsageReport;
using SMSC.Application.Responses;
using SMSC.Core.Logger.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace SMSC.Admin.Controllers
{
    [Authorize(Policy = "RequireEmail")]
    public class ServiceUsageReportController : Controller
    {
        private readonly ILog _logger;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IMediator _mediator;

        public ServiceUsageReportController(ILog logger, IStringLocalizer<SharedResources> localizer, IMediator mediator)
        {
            _logger = logger;
            _localizer = localizer;
            _mediator = mediator;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetListByServiceType(CancellationToken token, ServiceUsageReportDto serviceUsageReportDto)
        {



            var addServiceUsageCommand = new ServiceUsageReportListQuery
            {
                ServiceUsageReportDto = serviceUsageReportDto
            };

            var serviceUsageReportResult = await _mediator.Send(addServiceUsageCommand, token);
            var response = ResponseMapper.MapResponse(serviceUsageReportResult, serviceUsageReportResult.IsSuccess ? serviceUsageReportResult.Value : null);
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

    }
}
