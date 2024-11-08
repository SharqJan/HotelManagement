using FluentResults;
using MediatR;
using SMSC.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSC.Application.Commands.Role
{
    public class UpdateRoleCommand : IRequest<Result<long>>
    {
        public RoleDTO RoleDTO { get; set; }
   

    }
}
