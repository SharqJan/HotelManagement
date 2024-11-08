using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SMSC.Application.Interfaces;
using SMSC.Application.DTO;
using FluentResults;
using SMSC.Application.Queries.ServiceUsageReport;

namespace SMSC.Application.Handlers.ServiceUsageReport
{
    public class ServiceUsageReportListQueryHandler : IRequestHandler<ServiceUsageReportListQuery, Result<IEnumerable<ServiceUsageReportDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ServiceUsageReportListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<ServiceUsageReportDto>>> Handle(ServiceUsageReportListQuery request, CancellationToken cancellationToken)
        {

            try
            {
                var resultInvoice = await _unitOfWork.ServiceUsageReportRepository.GetRevenueByServiceType(cancellationToken, request.ServiceUsageReportDto);
                return Result.Ok(resultInvoice);
            }

            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }

        }
    }
}

