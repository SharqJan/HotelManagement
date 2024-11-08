using AutoMapper;
using FluentResults;
using MediatR;
using SMSC.Application.Commands.User;
using SMSC.Application.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SMSC.Application.Handlers.User
{
    public class DeleteUserByIdCommandHandler : IRequestHandler<DeleteUserByIdCommand, Result<long>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteUserByIdCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<long>> Handle(DeleteUserByIdCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var resultUser = await _unitOfWork.UserRepository.DeleteUserByIdAsync(cancellationToken, request.UserId);
                return Result.Ok(resultUser);
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
           
        }
    }
}
