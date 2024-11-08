using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SMSC.Application.Interfaces;
using SMSC.Application.Queries.Role;
using SMSC.Application.DTO;
using FluentResults;

namespace SMSC.Application.Handlers.Role
{
    public class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQuery, Result<RoleDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetRoleByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<RoleDTO>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {

            try
            {
                var roleId = request.RoleId;
                var resultRole = await _unitOfWork.RoleRepository.GetRoleByIdAsync(cancellationToken, roleId);
                RoleDTO roleDTO = _mapper.Map<RoleDTO>(resultRole);
                return Result.Ok(roleDTO);
            }

            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
           
        }
    }
}