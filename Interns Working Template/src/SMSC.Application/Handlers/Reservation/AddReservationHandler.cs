using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentResults;
using MediatR;
using SMSC.Application.Commands.Reservation;
using SMSC.Application.Interfaces;



namespace SMSC.Application.Handlers.Reservation
{
    class AddReservationHandler : IRequestHandler<AddReservationCommand, Result<long>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddReservationHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<Result<long>> Handle(AddReservationCommand request, CancellationToken cancellationToken)
        {


            try
            {

                var resultReservationId = await _unitOfWork.ReservationRepository.AddReservationAsync(cancellationToken, request.ReservationDto);

                return resultReservationId;
            }

            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }


        }
    }
}