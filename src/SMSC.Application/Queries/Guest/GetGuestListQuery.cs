using FluentResults;
using MediatR;
using SMSC.Application.DTO;
using System.Collections.Generic;


namespace SMSC.Application.Queries.Guest
{
    public class GetGuestListQuery : IRequest<Result<IEnumerable<GuestDTO>>>
    {

    }
}