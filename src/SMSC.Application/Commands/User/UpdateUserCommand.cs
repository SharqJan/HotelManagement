using FluentResults;
using MediatR;
using SMSC.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSC.Application.Commands.User
{
    public class UpdateUserCommand : IRequest<Result<long>>
    {
        public UserDTO UserDTO { get; set; }


    }
}
