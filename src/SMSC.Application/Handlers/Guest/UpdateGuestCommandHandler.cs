using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentResults;
using MediatR;
using SMSC.Application.Commands.Guest;
using SMSC.Application.Commands.User;
using SMSC.Application.Interfaces;

namespace SMSC.Application.Handlers.Guest
{
    class UpdateGuestCommandHandler : IRequestHandler<UpdateGuestCommand, Result<long>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateGuestCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;


            _mapper = mapper;
        }


        public async Task<Result<long>> Handle(UpdateGuestCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var guest = _mapper.Map<Core.Entities.Guest>(request.GuestDTO);

                long resultGuestId = await _unitOfWork.GuestRepository.UpdateGuestAsync(cancellationToken, guest);

                return resultGuestId;
            }

            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
           

        }
    }
}
