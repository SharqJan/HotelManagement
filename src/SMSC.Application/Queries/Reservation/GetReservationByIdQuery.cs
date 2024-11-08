using FluentResults;
using MediatR;
using SMSC.Application.DTO;


namespace SMSC.Application.Queries.Reservation
{
    public class GetReservationByIdQuery : IRequest<Result<ReservationDto>>
    {
        public int GuestId { get; set; }
    }
}
