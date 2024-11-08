using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentResults;
using MediatR;
using SMSC.Application.DTO;
using SMSC.Application.Interfaces;
using SMSC.Application.Queries.Guest;
using SMSC.Application.Queries.Reservation;
using SMSC.Application.Queries.User;

namespace SMSC.Application.Handlers.Reservation
{
    public class GetReservationListQueryHandler : IRequestHandler<GetReservationListQuery, Result<IEnumerable<ReservationDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetReservationListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<ReservationDto>>> Handle(GetReservationListQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var reservationList = await _unitOfWork.ReservationRepository.GetReservationListAsync<ReservationDto>(cancellationToken);
                return Result.Ok(reservationList);
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }

        }
    }
}