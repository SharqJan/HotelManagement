using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSC.Application.Commands.Room
{
    public class DeleteRoomByIdCommand : IRequest<Result<long>>
    {
        public int RoomId { get; set; }
    }
}
