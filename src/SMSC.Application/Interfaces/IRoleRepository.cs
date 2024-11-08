using SMSC.Core.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SMSC.Application.Interfaces
{
    public interface IRoleRepository
    {
        Task<IEnumerable<Roles>> GetRoleListAsync<Roles>(CancellationToken token);
        Task<long> AddRoleAsync(CancellationToken cancellationToken, Roles role);
        Task<Roles> GetRoleByIdAsync(CancellationToken cancellationToken, int roleId);
        Task<long> UpdateRoleAsync(CancellationToken cancellationToken, Roles role);
        Task<long> DeleteRoleByIdAsync(CancellationToken cancellationToken, int roleId);
    }
}
