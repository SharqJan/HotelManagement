using FluentResults;
using MediatR;
using SMSC.Application.DTO;
using System;
using System.Collections.Generic;

namespace SMSC.Application.Queries.Room
{
    public class GetRoomListQuery : IRequest<Result<IEnumerable<RoomDTO>>>
    {
        public RoomDTO RoomDTO { get; set; }    
       
    }
}