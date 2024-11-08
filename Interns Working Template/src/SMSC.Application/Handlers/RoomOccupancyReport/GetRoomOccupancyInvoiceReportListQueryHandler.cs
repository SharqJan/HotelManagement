using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SMSC.Application.Interfaces;
using SMSC.Application.DTO;
using FluentResults;
using SMSC.Application.Queries.RoomOccupancyReport;

namespace SMSC.Application.Handlers.InvoiceReport
{
    public class GetRoomOccupancyInvoiceReportListQueryHandler : IRequestHandler<GetRoomOccupancyInvoiceReportListQuery, Result<IEnumerable<RoomOccupancyInvoiceReportDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetRoomOccupancyInvoiceReportListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<RoomOccupancyInvoiceReportDto>>> Handle(GetRoomOccupancyInvoiceReportListQuery request, CancellationToken cancellationToken)
        {

            try
            {
                var resultInvoice = await _unitOfWork.RoomOccupancyInvoiceReportRepository.GetInvoice(cancellationToken, request.RoomOccupancyInvoiceReportDto);
                return Result.Ok(resultInvoice);
            }

            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }

        }
    }
}