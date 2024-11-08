using SMSC.Core.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace SMSC.Application.Interfaces
{
    public interface IEmployeeRepository    
    {
        Task<long> AddEmployeeAsync(CancellationToken cancellationToken, Employee employee);
        Task<Employee> GetEmployeeByIdAsync(CancellationToken cancellationToken, long employeeId);
    }
}
