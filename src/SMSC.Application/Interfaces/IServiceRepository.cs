using SMSC.Core.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SMSC.Application.Interfaces
{
    public interface IServiceRepository
    {
        Task<IEnumerable<Service>> GetServiceListAsync<Service>(CancellationToken token);
        Task<long> AddServiceAsync(CancellationToken cancellationToken, Service service);
        Task<Service> GetServiceByIdAsync(CancellationToken cancellationToken, int serviceId);
        Task<long> UpdateServiceAsync(CancellationToken cancellationToken, Service service);
        Task<long> DeleteServiceByIdAsync(CancellationToken cancellationToken, int serviceId);
        //Task<long> DeleteAllEmployeesAsync(CancellationToken cancellationToken);
    }
}
