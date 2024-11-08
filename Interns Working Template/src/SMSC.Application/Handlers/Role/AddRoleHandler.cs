using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentResults;
using MediatR;
using SMSC.Application.Commands.Role;
using SMSC.Application.Interfaces;

namespace SMSC.Application.Handlers.Role
{
    class AddRoleHandler : IRequestHandler<AddRoleCommand, Result<long>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddRoleHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<Result<long>> Handle(AddRoleCommand request, CancellationToken cancellationToken)
        {

            try
            {
                var role = _mapper.Map<Core.Entities.Roles>(request.RoleDTO);

                var resultRoleId = await _unitOfWork.RoleRepository.AddRoleAsync(cancellationToken, role);

                return resultRoleId;
            }

            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }

           

        }
    }
}