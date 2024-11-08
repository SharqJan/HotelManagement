using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SMSC.Application.DTO;
using SMSC.Application.Queries.RoomOccupancyReport;
using SMSC.Application.Responses;
using SMSC.Core.Logger.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace SMSC.Admin.Controllers
{
    [Authorize(Policy = "RequireEmail")]
    public class RoomOccupancyReportController : Controller
    {
        private readonly ILog _logger;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IMediator _mediator;

        public RoomOccupancyReportController(ILog logger, IStringLocalizer<SharedResources> localizer, IMediator mediator)
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
        public async Task<JsonResult> RoomOccupancyInvoiceReport(CancellationToken token, RoomOccupancyInvoiceReportDto roomOccupancyInvoiceReportDto)
        {
            var getRoomOccupancyInvoiceReportList = new GetRoomOccupancyInvoiceReportListQuery
            {
                RoomOccupancyInvoiceReportDto = roomOccupancyInvoiceReportDto
            };

            var roomOccupancyInvoiceReportResult = await _mediator.Send(getRoomOccupancyInvoiceReportList, token);
            var response = ResponseMapper.MapResponse(roomOccupancyInvoiceReportResult, roomOccupancyInvoiceReportResult.IsSuccess ? roomOccupancyInvoiceReportResult.Value : null);
            return Json(response);
        }


    }
}
