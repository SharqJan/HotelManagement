using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SMSC.Application.Commands.Role;
using SMSC.Application.Commands.Room;
using SMSC.Application.DTO;
using SMSC.Application.Interfaces;

namespace SMSC.Application.Handlers.Room
{
    class UpdateRoomCommandHandler : IRequestHandler<UpdateRoomCommand, string>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateRoomCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<string> Handle(UpdateRoomCommand request, CancellationToken cancellationToken)
        {
           
            var room = _mapper.Map<Core.Entities.Room>(request.RoomDTO);

            long resultRoomId = await _unitOfWork.RoomRepository.UpdateRoomAsync(cancellationToken, room);

            return resultRoomId.ToString();

        }
    }
}
