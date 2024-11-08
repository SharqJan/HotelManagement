
using FluentResults;
using MediatR;
using SMSC.Application.DTO;

namespace SMSC.Application.Commands.Reservation
{
    public class AddReservationCommand : IRequest<Result<long>>
    {
        public ReservationDto ReservationDto { get; set; }

    }
}
