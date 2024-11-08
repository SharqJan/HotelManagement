using FluentResults;
using MediatR;
using SMSC.Application.DTO;
using System.Collections.Generic;


namespace SMSC.Application.Queries.User
{
    public class GetUserListQuery : IRequest<Result<IEnumerable<UserDTO>>>
    {

    }
}