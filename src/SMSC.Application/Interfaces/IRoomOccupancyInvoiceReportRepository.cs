using SMSC.Application.DTO;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SMSC.Application.Interfaces
{
    public interface IRoomOccupancyInvoiceReportRepository
    {

        Task<IEnumerable<RoomOccupancyInvoiceReportDto>> GetInvoice(CancellationToken cancellationToken, RoomOccupancyInvoiceReportDto roomOccupancyInvoiceReportDto);
    }
}

