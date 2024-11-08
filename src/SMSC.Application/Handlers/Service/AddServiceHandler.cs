using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentResults;
using MediatR;
using SMSC.Application.Commands.Service;
using SMSC.Application.Interfaces;

namespace SMSC.Application.Handlers.Service
{
    class AddServiceHandler : IRequestHandler<AddServiceCommand, Result<long>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddServiceHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<Result<long>> Handle(AddServiceCommand request, CancellationToken cancellationToken)
        {

            try
            {
               var service = _mapper.Map<Core.Entities.Service>(request.ServiceDTO);

               var resultServiceId = await _unitOfWork.ServiceRepository.AddServiceAsync(cancellationToken, service);

               return  resultServiceId;
            }

            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
            

        }
    }
}
