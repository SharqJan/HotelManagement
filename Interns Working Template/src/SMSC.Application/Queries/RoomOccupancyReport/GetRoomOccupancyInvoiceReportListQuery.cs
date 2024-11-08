using FluentResults;
using MediatR;
using SMSC.Application.DTO;
using System.Collections.Generic;


namespace SMSC.Application.Queries.RoomOccupancyReport
{
    public class GetRoomOccupancyInvoiceReportListQuery : IRequest<Result<IEnumerable<RoomOccupancyInvoiceReportDto>>>
    {
        public RoomOccupancyInvoiceReportDto RoomOccupancyInvoiceReportDto { get; set; }
    }
}

