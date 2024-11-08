using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentResults;
using MediatR;
using SMSC.Application.DTO;
using SMSC.Application.Interfaces;
using SMSC.Application.Queries.CheckIn;
using SMSC.Application.Queries.Guest;
using SMSC.Application.Queries.Reservation;
using SMSC.Application.Queries.User;

namespace SMSC.Application.Handlers.CheckIn
{
    public class GetCheckReservationListQueryHandler : IRequestHandler<GetCheckReservationListQuery, Result<IEnumerable<ReservationDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCheckReservationListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<ReservationDto>>> Handle(GetCheckReservationListQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var reservationList = await _unitOfWork.CheckInCheckOutRepository.CheckReservationListAsync(cancellationToken,request.ReservationDto);
                return Result.Ok(reservationList);
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }

        }
    }
}