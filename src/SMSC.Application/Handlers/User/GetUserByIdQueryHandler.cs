using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SMSC.Application.Interfaces;
using SMSC.Application.DTO;
using SMSC.Application.Queries.User;
using FluentResults;

namespace SMSC.Application.Handlers.Role
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Result<UserDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetUserByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<UserDTO>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var UserId = request.UserId;
                var resultUser = await _unitOfWork.UserRepository.GetUserByIdAsync(cancellationToken, UserId);
                return Result.Ok(resultUser);
            }

            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
            
        }
    }
}