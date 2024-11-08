
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentResults;
using MediatR;
using SMSC.Application.DTO;
using SMSC.Application.Interfaces;
using SMSC.Application.Queries.CheckOut;

namespace SMSC.Application.Handlers.CheckOut
{
    public class GetOccupiedRoomsGuestListQueryHandler : IRequestHandler<GetOccupiedRoomsGuestListQuery, Result<IEnumerable<OccupiedRoomsGuestDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetOccupiedRoomsGuestListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<OccupiedRoomsGuestDto>>> Handle(GetOccupiedRoomsGuestListQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var occupiedRoomsGuestList = await _unitOfWork.CheckInCheckOutRepository.GetOccupiedRoomsGuestListAsync<OccupiedRoomsGuestDto>(cancellationToken);
                return Result.Ok(occupiedRoomsGuestList);
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }

        }
    }
}