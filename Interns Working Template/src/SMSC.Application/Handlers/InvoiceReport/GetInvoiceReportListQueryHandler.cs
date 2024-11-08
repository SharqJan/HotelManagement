using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SMSC.Application.Interfaces;
using SMSC.Application.DTO;
using FluentResults;
using SMSC.Application.Queries.InvoiceReport;

namespace SMSC.Application.Handlers.InvoiceReport
{
    public class GetInvoiceReportListQueryHandler : IRequestHandler<GetInvoiceReportListQuery, Result<IEnumerable<InvoiceReportDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetInvoiceReportListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<InvoiceReportDto>>> Handle(GetInvoiceReportListQuery request, CancellationToken cancellationToken)
        {

            try
            {
                var resultInvoice = await _unitOfWork.InvoiceReportRepository.GetDailyInvoice(cancellationToken, request.InvoiceReportDto);
                return Result.Ok(resultInvoice);
            }

            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }

        }
    }
}