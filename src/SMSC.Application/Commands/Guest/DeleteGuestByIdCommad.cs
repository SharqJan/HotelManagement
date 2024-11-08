using FluentResults;
using MediatR;

namespace SMSC.Application.Commands.Guest
{
    public class DeleteGuestByIdCommand : IRequest<Result<long>>
    {
        public int GuestId { get; set; }
    }
}
