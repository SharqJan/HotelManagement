
using FluentResults;
using MediatR;
using SMSC.Application.DTO;

namespace SMSC.Application.Commands.Role
{
    public class AddRoleCommand : IRequest<Result<long>>
    {
        public RoleDTO RoleDTO { get; set; }

    }
}
