using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSC.Application.Commands.Role
{
    public class DeleteRoleByIdCommand : IRequest<Result<long>>
    {
        public int RoleId { get; set; }
    }
}
