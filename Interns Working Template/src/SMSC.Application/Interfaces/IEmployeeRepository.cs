using SMSC.Core.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SMSC.Application.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetEmployeeListAsync<Employee>(CancellationToken token);
        Task<long> AddEmployeeAsync(CancellationToken cancellationToken, Employee employee);
        Task<Employee> GetEmployeeByIdAsync(CancellationToken cancellationToken, int employeeId);
        Task<long> UpdateEmployeeAsync(CancellationToken cancellationToken, Employee employee);
        Task<long> DeleteEmployeeByIdAsync(CancellationToken cancellationToken, int employeeId);
        //Task<long> DeleteAllEmployeesAsync(CancellationToken cancellationToken);
    }
}
