using FluentResults;
using MediatR;
using SMSC.Application.DTO;
using System;
using System.Collections.Generic;

namespace SMSC.Application.Queries.Service
{
    public class GetServiceListQuery : IRequest<Result<IEnumerable<ServiceDTO>>>
    {
        public ServiceDTO ServiceDTO { get; set; }
    }
}