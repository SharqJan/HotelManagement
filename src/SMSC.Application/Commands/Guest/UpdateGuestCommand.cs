using FluentResults;
using MediatR;
using SMSC.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSC.Application.Commands.Guest
{
    public class UpdateGuestCommand : IRequest<Result<long>>
    {
        public GuestDTO GuestDTO { get; set; }


    }
}
