using FluentResults;
using MediatR;
using SMSC.Application.DTO;


namespace SMSC.Application.Queries.Guest
{
    public class GetGuestByIdQuery : IRequest<Result<GuestDTO>>
    {
        public int GuestId { get; set; }
    }
}
