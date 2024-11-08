using AutoMapper;
using FluentResults;
using MediatR;
using SMSC.Application.Commands.Role;
using SMSC.Application.Commands.Service;
using SMSC.Application.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SMSC.Application.Handlers.Service
{
    public class DeleteServiceByIdCommandHandler : IRequestHandler<DeleteServiceByIdCommand, Result<long>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteServiceByIdCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<long>> Handle(DeleteServiceByIdCommand request, CancellationToken cancellationToken)
        {

            try
            {
                var resultService = await _unitOfWork.ServiceRepository.DeleteServiceByIdAsync(cancellationToken, request.ServiceId);
                return Result.Ok(resultService);
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
            
            
        }
    }
}
