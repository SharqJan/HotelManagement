using SMSC.Application.DTO;
using SMSC.Core.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SMSC.Application.Interfaces
{
    public interface IReservationRepository
    {
        Task<IEnumerable<ReservationDto>> GetReservationListAsync<ReservationDto>(CancellationToken token);
        Task<long> AddReservationAsync(CancellationToken cancellationToken, ReservationDto reservation);
        Task<ReservationDto> GetReservationByIdAsync(CancellationToken cancellationToken, int GuestId);
        Task<long> UpdateReservationAsync(CancellationToken cancellationToken, ReservationDto reservation);
        Task<long> UpdateReservationStatusAsync(CancellationToken cancellationToken, ReservationDto reservation);

        // Task<long> DeleteGuestByIdAsync(CancellationToken cancellationToken, int GuestId);
        //Task<long> DeleteAllEmployeesAsync(CancellationToken cancellationToken);
    }
}
