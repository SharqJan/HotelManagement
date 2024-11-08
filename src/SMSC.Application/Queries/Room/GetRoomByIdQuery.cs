using FluentResults;
using MediatR;
using SMSC.Application.DTO;


namespace SMSC.Application.Queries.Room
{
    public class GetRoomByIdQuery : IRequest<Result<RoomDTO>>
    {
        public int RoomId { get; set; }
    }
}

