using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentResults;
using MediatR;
using SMSC.Application.Commands.Reservation;
using SMSC.Application.Interfaces;

namespace SMSC.Application.Handlers.Reservation
{
    class UpdateReservationStatusCommandHandler : IRequestHandler<UpdateReservationStatusCommand, Result<long>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateReservationStatusCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;


            _mapper = mapper;
        }


        public async Task<Result<long>> Handle(UpdateReservationStatusCommand request, CancellationToken cancellationToken)
        {
            try
            {
                //var guest = _mapper.Map<Core.Entities.Guest>(request.GuestDTO);

                long resultGuestId = await _unitOfWork.ReservationRepository.UpdateReservationStatusAsync(cancellationToken, request.ReservationDto);

                return resultGuestId;
            }

            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }


        }
    }
}
