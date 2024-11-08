
using FluentResults;
using MediatR;
using SMSC.Application.DTO;
using System.Collections.Generic;


namespace SMSC.Application.Queries.CheckOut
{
    public class GetOccupiedRoomsGuestListQuery : IRequest<Result<IEnumerable<OccupiedRoomsGuestDto>>>
    {

    }
}