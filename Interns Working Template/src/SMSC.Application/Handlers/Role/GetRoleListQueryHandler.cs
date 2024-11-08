using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentResults;
using MediatR;
using SMSC.Application.DTO;
using SMSC.Application.Interfaces;
using SMSC.Application.Queries.Role;

namespace SMSC.Application.Handlers.Role
{
    public class GetRoleListQueryHandler : IRequestHandler<GetRoleListQuery, Result<IEnumerable<RoleDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetRoleListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<RoleDTO>>> Handle(GetRoleListQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var roleList = await _unitOfWork.RoleRepository.GetRoleListAsync<RoleDTO>(cancellationToken);
                return Result.Ok(roleList);
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }
    }
}