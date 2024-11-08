using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SMSC.Application.DTO;
using SMSC.Application.Queries.InvoiceReport;
using SMSC.Application.Responses;
using SMSC.Core.Logger.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace SMSC.Admin.Controllers
{
    [Authorize(Policy = "RequireEmail")]
    public class RevenueReportController : Controller
    {
        private readonly ILog _logger;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IMediator _mediator;

        public RevenueReportController(ILog logger, IStringLocalizer<SharedResources> localizer, IMediator mediator)
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
        public async Task<JsonResult> DailyInvoiceReport(CancellationToken token, InvoiceReportDto invoiceReportDto)
        {
            
            
            
            var addDailyInvoiceCommand = new GetInvoiceReportListQuery
            {
                InvoiceReportDto = invoiceReportDto
            };

            var dailyInvoiceReportResult = await _mediator.Send(addDailyInvoiceCommand, token);
            var response = ResponseMapper.MapResponse(dailyInvoiceReportResult, dailyInvoiceReportResult.IsSuccess ? dailyInvoiceReportResult.Value : null);
            return Json(response);
        }

    
    }
}
