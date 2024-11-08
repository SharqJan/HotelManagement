using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentResults;
using MediatR;
using SMSC.Application.DTO;
using SMSC.Application.Interfaces;
using SMSC.Application.Queries.CheckIn;

namespace SMSC.Application.Handlers.CheckOut
{
    public class GetRoomListByGuestIdQueryHandler : IRequestHandler<GetRoomListByGuestIdQuery, Result<IEnumerable<CheckOutDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetRoomListByGuestIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<CheckOutDto>>> Handle(GetRoomListByGuestIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var roomList = await _unitOfWork.CheckInCheckOutRepository.GetRoomsByGuestId(cancellationToken, request.GuestId,request.RoomNo);
                return Result.Ok(roomList);
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }

        }
    }
}