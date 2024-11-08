using FluentResults;
using MediatR;
using SMSC.Application.DTO;
using System.Collections.Generic;


namespace SMSC.Application.Queries.ServiceUsageReport
{
    public class ServiceUsageReportListQuery : IRequest<Result<IEnumerable<ServiceUsageReportDto>>>
    {
        public ServiceUsageReportDto ServiceUsageReportDto { get; set; }
    }
}
