
using FluentResults;
using MediatR;
using SMSC.Application.DTO;

namespace SMSC.Application.Commands.Room
{
    public class AddRoomCommand : IRequest<Result<long>>
    {
        public RoomDTO RoomDTO { get; set; }

    }
}
