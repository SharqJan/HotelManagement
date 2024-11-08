using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentResults;
using MediatR;
using SMSC.Application.Commands.Room;
using SMSC.Application.Interfaces;

namespace SMSC.Application.Handlers.Room
{
    class AddRoomHandler : IRequestHandler<AddRoomCommand, Result<long>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddRoomHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<Result<long>> Handle(AddRoomCommand request, CancellationToken cancellationToken)
        {

            try
            {
                var room = _mapper.Map<Core.Entities.Room>(request.RoomDTO);

                var resultRoomId = await _unitOfWork.RoomRepository.AddRoomAsync(cancellationToken, room);

                return resultRoomId;
            }

            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
           


        }
    }
}