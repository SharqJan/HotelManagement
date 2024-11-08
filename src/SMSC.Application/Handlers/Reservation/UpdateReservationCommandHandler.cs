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
    class UpdateReservationCommandHandler : IRequestHandler<UpdateReservationCommand, Result<long>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateReservationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;


            _mapper = mapper;
        }


        public async Task<Result<long>> Handle(UpdateReservationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                //var guest = _mapper.Map<Core.Entities.Guest>(request.GuestDTO);

                long resultGuestId = await _unitOfWork.ReservationRepository.UpdateReservationAsync(cancellationToken, request.ReservationDto);

                return resultGuestId;
            }

            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }


        }
    }
}
