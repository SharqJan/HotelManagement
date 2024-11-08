using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SMSC.Application.Interfaces;
using SMSC.Application.DTO;
using SMSC.Application.Queries.Room;
using FluentResults;

namespace SMSC.Application.Handlers.Room
{
    public class GetRoomByIdQueryHandler : IRequestHandler<GetRoomByIdQuery, Result<RoomDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetRoomByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<RoomDTO>> Handle(GetRoomByIdQuery request, CancellationToken cancellationToken)
        {


            try
            {
                var roomId = request.RoomId;
                var resultRoom = await _unitOfWork.RoomRepository.GetRoomByIdAsync(cancellationToken, roomId);
                return Result.Ok(resultRoom);
            }

            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }

        }
        


        }
    }
