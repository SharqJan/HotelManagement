using SMSC.Application.DTO;
using SMSC.Core.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SMSC.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserDTO>> GetUserListAsync<UserDTO>(CancellationToken token);
        Task<long> AddUserAsync(CancellationToken cancellationToken, Users user);
        Task<UserDTO> GetUserByIdAsync(CancellationToken cancellationToken, int userId);
        Task<long> UpdateUserAsync(CancellationToken cancellationToken, Users user);
        Task<long> DeleteUserByIdAsync(CancellationToken cancellationToken, int userId);
        //Task<long> DeleteAllEmployeesAsync(CancellationToken cancellationToken);
    }
}
