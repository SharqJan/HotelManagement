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
    class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, Result<long>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateRoleCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<Result<long>> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var role = _mapper.Map<Core.Entities.Roles>(request.RoleDTO);

                long resultRoleId = await _unitOfWork.RoleRepository.UpdateRoleAsync(cancellationToken, role);

                return resultRoleId;
            }

            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }


        }
    }
}
