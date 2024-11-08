using FluentResults;
using MediatR;
using SMSC.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSC.Application.Commands.Service
{
    public class UpdateServiceCommand : IRequest<Result<long>>
    {
        public ServiceDTO ServiceDTO { get; set; }


    }
}
