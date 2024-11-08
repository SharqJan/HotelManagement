using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SMSC.Application.Interfaces;
using SMSC.Application.DTO;
using FluentResults;
using SMSC.Application.Queries.CheckOut;

namespace SMSC.Application.Handlers.Role
{
    public class GetInvoiceByRoomIdQueryHandler : IRequestHandler<GetInvoiceByRoomIdQuery, Result<InvoiceDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetInvoiceByRoomIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<InvoiceDto>> Handle(GetInvoiceByRoomIdQuery request, CancellationToken cancellationToken)
        {

            try
            {
                var resultInvoice = await _unitOfWork.CheckInCheckOutRepository.GetInvoiceByRoomIdAsync(cancellationToken, request.RoomId);
                return Result.Ok(resultInvoice);
            }

            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }

        }
    }
}