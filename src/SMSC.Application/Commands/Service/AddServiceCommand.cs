using FluentResults;
using MediatR;
using SMSC.Application.DTO;

namespace SMSC.Application.Commands.Service
{
    public class AddServiceCommand : IRequest<Result<long>>
    {
        public ServiceDTO ServiceDTO{ get; set; }

    }
}
