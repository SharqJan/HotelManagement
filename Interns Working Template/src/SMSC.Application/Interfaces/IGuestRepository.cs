using SMSC.Application.DTO;
using SMSC.Core.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SMSC.Application.Interfaces
{
    public interface IGuestRepository
    {
        Task<IEnumerable<GuestDTO>> GetGuestListAsync<GuestDTO>(CancellationToken token);
        Task<long> AddGuestAsync(CancellationToken cancellationToken, Guest guest);
        Task<GuestDTO> GetGuestByIdAsync(CancellationToken cancellationToken, int GuestId);
        Task<long> UpdateGuestAsync(CancellationToken cancellationToken, Guest guest);
        Task<long> DeleteGuestByIdAsync(CancellationToken cancellationToken, int GuestId);
    }
}
