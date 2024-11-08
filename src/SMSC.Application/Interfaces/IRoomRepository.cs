using SMSC.Application.DTO;
using SMSC.Core.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SMSC.Application.Interfaces
{
    public interface IRoomRepository
    {
        Task<IEnumerable<RoomDTO>> GetRoomListAsync<RoomDTO>(CancellationToken token);
        Task<long> AddRoomAsync(CancellationToken cancellationToken, Room room);
        Task<RoomDTO> GetRoomByIdAsync(CancellationToken cancellationToken, int roomId);
        Task<long> UpdateRoomAsync(CancellationToken cancellationToken, Room room);
        Task<long> DeleteRoomByIdAsync(CancellationToken cancellationToken, int roomId);
        //Task<long> DeleteAllEmployeesAsync(CancellationToken cancellationToken);
    }
}
