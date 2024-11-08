using AutoMapper;
using FluentResults;
using MediatR;
using SMSC.Application.Commands.Guest;
using SMSC.Application.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SMSC.Application.Handlers.Guest
{
    public class DeleteGuestByIdCommandHandler : IRequestHandler<DeleteGuestByIdCommand, Result<long>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteGuestByIdCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<long>> Handle(DeleteGuestByIdCommand request, CancellationToken cancellationToken)
        {

            try
            {
                var resultGuest = await _unitOfWork.GuestRepository.DeleteGuestByIdAsync(cancellationToken, request.GuestId);
                return Result.Ok(resultGuest);
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }

            
        }
    }
}
