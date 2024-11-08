using MediatR;
using SMSC.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSC.Application.Commands.Room
{
    public class UpdateRoomCommand : IRequest<string>
    {
        public RoomDTO RoomDTO { get; set; }


    }
}
