using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentResults;
using MediatR;
using SMSC.Application.DTO;
using SMSC.Application.Interfaces;
using SMSC.Application.Queries.User;

namespace SMSC.Application.Handlers.User
{
    public class GetUserListQueryHandler : IRequestHandler<GetUserListQuery, Result<IEnumerable<UserDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetUserListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<UserDTO>>> Handle(GetUserListQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var userList = await _unitOfWork.UserRepository.GetUserListAsync<UserDTO>(cancellationToken);
                return Result.Ok(userList);
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
            
        }
    }
}