using FluentResults;
using MediatR;
using SMSC.Application.DTO;
using System.Collections.Generic;

namespace SMSC.Application.Queries.Role
{
    public class GetRoleListQuery : IRequest<Result<IEnumerable<RoleDTO>>>
    {
    }
}