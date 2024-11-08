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
    class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Result<long>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<Result<long>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {

            try
            {
                var user = _mapper.Map<Core.Entities.Users>(request.UserDTO);

                long resultUserId = await _unitOfWork.UserRepository.UpdateUserAsync(cancellationToken, user);

                return resultUserId;
            }

            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }

            

        }
    }
}
