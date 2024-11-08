using FluentResults;
using MediatR;
using SMSC.Application.DTO;
using System.Collections.Generic;


namespace SMSC.Application.Queries.CheckIn
{
    public class GetCheckReservationListQuery : IRequest<Result<IEnumerable<ReservationDto>>>
    {
        public ReservationDto ReservationDto { get; set; }
    }
}