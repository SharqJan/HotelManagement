
using FluentResults;
using MediatR;
using SMSC.Application.DTO;

namespace SMSC.Application.Commands.Guest
{
    public class AddGuestCommand : IRequest<Result<long>>
    {
        public GuestDTO GuestDTO { get; set; }

    }
}
