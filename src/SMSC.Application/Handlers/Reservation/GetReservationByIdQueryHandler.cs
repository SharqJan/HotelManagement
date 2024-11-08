using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SMSC.Application.Interfaces;
using SMSC.Application.DTO;
using FluentResults;
using SMSC.Application.Queries.Reservation;

namespace SMSC.Application.Handlers.Reservation
{
    public class GetReservationByIdQueryHandler : IRequestHandler<GetReservationByIdQuery, Result<ReservationDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetReservationByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<ReservationDto>> Handle(GetReservationByIdQuery request, CancellationToken cancellationToken)
        {


            try
            {
                var guestId = request.GuestId;
                var resultGuest = await _unitOfWork.ReservationRepository.GetReservationByIdAsync(cancellationToken, guestId);
                return Result.Ok(resultGuest);
            }

            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }

        }
    }
}