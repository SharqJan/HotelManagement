using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSC.Application.Commands.Service
{
    public class DeleteServiceByIdCommand : IRequest<Result<long>>
    {
        public int ServiceId { get; set; }
    }
}
