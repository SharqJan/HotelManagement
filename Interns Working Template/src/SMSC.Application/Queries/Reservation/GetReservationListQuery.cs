using FluentResults;
using MediatR;
using SMSC.Application.DTO;
using System.Collections.Generic;


namespace SMSC.Application.Queries.Reservation
{
    public class GetReservationListQuery : IRequest<Result<IEnumerable<ReservationDto>>>
    {

    }
}