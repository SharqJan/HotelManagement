using AutoMapper;
using FluentResults;
using MediatR;
using SMSC.Application.Commands.Role;
using SMSC.Application.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SMSC.Application.Handlers.Role
{
    public class DeleteRoleByIdCommandHandler : IRequestHandler<DeleteRoleByIdCommand, Result<long>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteRoleByIdCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<long>> Handle(DeleteRoleByIdCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var resultRole = await _unitOfWork.RoleRepository.DeleteRoleByIdAsync(cancellationToken, request.RoleId);
                return Result.Ok(resultRole);
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
           
        }
    }
}
