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
using SMSC.Application.Queries.Guest;
using FluentResults;

namespace SMSC.Application.Handlers.Guest
{
    public class GetGuestByIdQueryHandler : IRequestHandler<GetGuestByIdQuery, Result<GuestDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetGuestByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<GuestDTO>> Handle(GetGuestByIdQuery request, CancellationToken cancellationToken)
        {


            try
            {
                var guestId = request.GuestId;
                var resultGuest = await _unitOfWork.GuestRepository.GetGuestByIdAsync(cancellationToken, guestId);
               // RoleDTO roleDTO = _mapper.Map<RoleDTO>(resultRole);
                return Result.Ok(resultGuest);
            }

            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
           
        }
    }
}