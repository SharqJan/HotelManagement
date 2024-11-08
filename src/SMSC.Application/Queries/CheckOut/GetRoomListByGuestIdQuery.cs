using FluentResults;
using MediatR;
using SMSC.Application.DTO;
using System.Collections.Generic;


namespace SMSC.Application.Queries.CheckIn
{
    public class GetRoomListByGuestIdQuery : IRequest<Result<IEnumerable<CheckOutDto>>>
    {
        public int? GuestId { get; set; }
        public int? RoomNo { get; set; }
    }
}
