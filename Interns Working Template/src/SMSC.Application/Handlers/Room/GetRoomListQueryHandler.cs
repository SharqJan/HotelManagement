using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentResults;
using MediatR;
using SMSC.Application.DTO;
using SMSC.Application.Interfaces;
using SMSC.Application.Queries.Room;

namespace SMSC.Application.Handlers.Room
{
    public class GetRoomListQueryHandler : IRequestHandler<GetRoomListQuery, Result<IEnumerable<RoomDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetRoomListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<RoomDTO>>> Handle(GetRoomListQuery request, CancellationToken cancellationToken)
        {

            try
            {
                var roomList = await _unitOfWork.RoomRepository.GetRoomListAsync<RoomDTO>(cancellationToken);
                return Result.Ok(roomList);
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }

           
        }
    }
}