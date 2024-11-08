using FluentResults;
using MediatR;
using SMSC.Application.DTO;


namespace SMSC.Application.Queries.Service
{
    public class GetServiceByIdQuery : IRequest<Result<ServiceDTO>>
    {
        public int ServiceId { get; set; }
    }
}

