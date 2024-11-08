using SMSC.Application.DTO;
using SMSC.Core.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SMSC.Application.Interfaces
{
    public interface IServiceUsageReportRepository
    {

        Task<IEnumerable<ServiceUsageReportDto>> GetRevenueByServiceType(CancellationToken cancellationToken, ServiceUsageReportDto serviceUsageReportDto);
    }
}


