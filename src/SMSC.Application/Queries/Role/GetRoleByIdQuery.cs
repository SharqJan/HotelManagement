using FluentResults;
using MediatR;
using SMSC.Application.DTO;


namespace SMSC.Application.Queries.Role
{
    public class GetRoleByIdQuery : IRequest<Result<RoleDTO>>
    {
        public int RoleId { get; set; }
    }
}

