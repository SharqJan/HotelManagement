using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SMSC.Application.Interfaces;
using SMSC.Application.DTO;
using SMSC.Application.Queries.Service;
using FluentResults;

namespace SMSC.Application.Handlers.Service
{
    public class GetServiceByIdQueryHandler : IRequestHandler<GetServiceByIdQuery, Result<ServiceDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetServiceByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<ServiceDTO>> Handle(GetServiceByIdQuery request, CancellationToken cancellationToken)
        {

            try
            {
                var serviceId = request.ServiceId;
                var resultService = await _unitOfWork.ServiceRepository.GetServiceByIdAsync(cancellationToken, serviceId);
                ServiceDTO serviceDTO = _mapper.Map<ServiceDTO>(resultService);
                return Result.Ok(serviceDTO);
            }

            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
           
        }
    }
}
