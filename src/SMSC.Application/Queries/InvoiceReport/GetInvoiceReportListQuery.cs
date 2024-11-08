using FluentResults;
using MediatR;
using SMSC.Application.DTO;
using System.Collections.Generic;


namespace SMSC.Application.Queries.InvoiceReport
{
    public class GetInvoiceReportListQuery : IRequest<Result<IEnumerable<InvoiceReportDto>>>
    {
        public InvoiceReportDto InvoiceReportDto { get; set; }
    }
}
