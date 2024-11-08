using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentResults;
using MediatR;
using SMSC.Application.Commands.Role;
using SMSC.Application.Commands.Service;
using SMSC.Application.DTO;
using SMSC.Application.Interfaces;

namespace SMSC.Application.Handlers.Role
{
    class UpdateServiceCommandHandler : IRequestHandler<UpdateServiceCommand, Result<long>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateServiceCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<Result<long>> Handle(UpdateServiceCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var service = _mapper.Map<Core.Entities.Service>(request.ServiceDTO);

                long resultServiceId = await _unitOfWork.ServiceRepository.UpdateServiceAsync(cancellationToken, service);

                return resultServiceId;
            }

            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
            

        }
    }
}
