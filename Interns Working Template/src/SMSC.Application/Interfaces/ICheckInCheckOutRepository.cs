using SMSC.Application.DTO;
using SMSC.Core.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SMSC.Application.Interfaces
{
    public interface ICheckInCheckOutRepository
    {
   

        Task<IEnumerable<ReservationDto>>CheckReservationListAsync(CancellationToken token, ReservationDto reservation);
        
        Task<IEnumerable<CheckOutDto>> GetRoomsByGuestId(CancellationToken cancellationToken, int? GuestId,int? RoomNo);
        Task<InvoiceDto> GetInvoiceByRoomIdAsync(CancellationToken cancellationToken, int RoomId);

        Task<IEnumerable<OccupiedRoomsGuestDto>> GetOccupiedRoomsGuestListAsync<OccupiedRoomsGuestDto>(CancellationToken token);

        

    }
}


