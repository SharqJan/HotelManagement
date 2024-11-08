using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentResults;
using MediatR;
using SMSC.Application.Commands.User;
using SMSC.Application.Interfaces;

namespace SMSC.Application.Handlers.User
{
    class AddUserHandler : IRequestHandler<AddUserCommand, Result<long>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddUserHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<Result<long>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {

            try
            {
                var user = _mapper.Map<Core.Entities.Users>(request.UserDTO);

                var resultUserId = await _unitOfWork.UserRepository.AddUserAsync(cancellationToken, user);

                return resultUserId;

            }

            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
           
        }
    }
}