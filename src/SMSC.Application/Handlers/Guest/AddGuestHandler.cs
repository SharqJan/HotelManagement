using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentResults;
using MediatR;
using SMSC.Application.Commands.Guest;
using SMSC.Application.Interfaces;



namespace SMSC.Application.Handlers.Guest
{
    class AddGuestHandler : IRequestHandler<AddGuestCommand, Result<long>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddGuestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<Result<long>> Handle(AddGuestCommand request, CancellationToken cancellationToken)
        {


            try
            {
                var guest = _mapper.Map<Core.Entities.Guest>(request.GuestDTO);

                var resultGuestId = await _unitOfWork.GuestRepository.AddGuestAsync(cancellationToken, guest);

                return resultGuestId;
            }

            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }

           
        }
    }
}