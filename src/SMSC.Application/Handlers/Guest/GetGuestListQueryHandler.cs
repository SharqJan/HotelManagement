using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentResults;
using MediatR;
using SMSC.Application.DTO;
using SMSC.Application.Interfaces;
using SMSC.Application.Queries.Guest;

namespace SMSC.Application.Handlers.Guest
{
    public class GetGuestListQueryHandler : IRequestHandler<GetGuestListQuery, Result<IEnumerable<GuestDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetGuestListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<GuestDTO>>> Handle(GetGuestListQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var guestList = await _unitOfWork.GuestRepository.GetGuestListAsync<GuestDTO>(cancellationToken);
                return Result.Ok(guestList);
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
            
        }
    }
}