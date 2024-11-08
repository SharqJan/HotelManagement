using AutoMapper;
using FluentResults;
using MediatR;
using SMSC.Application.Commands.Room;
using SMSC.Application.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SMSC.Application.Handlers.Room
{
    public class DeleteRoomByIdCommandHandler : IRequestHandler<DeleteRoomByIdCommand, Result<long>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteRoomByIdCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<long>> Handle(DeleteRoomByIdCommand request, CancellationToken cancellationToken)
        {

            try
            {
                var resultRoom = await _unitOfWork.RoomRepository.DeleteRoomByIdAsync(cancellationToken, request.RoomId);
                return Result.Ok(resultRoom);
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
            
        }
    }
}
