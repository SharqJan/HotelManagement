using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SMSC.Application.Interfaces;
using SMSC.Application.DTO;
using FluentResults;
using SMSC.Application.Queries.Login;

namespace SMSC.Application.Handlers.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, Result<IEnumerable<LoginDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LoginQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<LoginDto>>> Handle(LoginQuery request, CancellationToken cancellationToken)
        {

            try
            {
                var resultLogin = await _unitOfWork.LoginRepository.Login(cancellationToken, request.LoginDto);
                return Result.Ok(resultLogin);
            }

            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }

        }
    }
}




