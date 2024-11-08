using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentResults;
using MediatR;
using SMSC.Application.DTO;
using SMSC.Application.Interfaces;
using SMSC.Application.Queries.Service;

namespace SMSC.Application.Handlers.Role
{
    public class GetServiceListQueryHandler : IRequestHandler<GetServiceListQuery, Result<IEnumerable<ServiceDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetServiceListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<ServiceDTO>>> Handle(GetServiceListQuery request, CancellationToken cancellationToken)
        {

            try
            {
                var serviceList = await _unitOfWork.ServiceRepository.GetServiceListAsync<ServiceDTO>(cancellationToken);
                return Result.Ok(serviceList);
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
           
        }
    }
}