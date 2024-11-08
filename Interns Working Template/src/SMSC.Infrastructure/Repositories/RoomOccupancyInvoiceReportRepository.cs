using SMSC.Application.DTO;
using SMSC.Application.Interfaces;
using SMSC.Core.Entities;
using SMSC.Core.Interfaces;
using SMSC.Core.Logger.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;


namespace SMSC.Infrastructure.Repositories
{
    public class RoomOccupancyInvoiceReportRepository : IRoomOccupancyInvoiceReportRepository
    {
        private readonly IRepository _dbRepository;
        private readonly LogService _logger;

        public RoomOccupancyInvoiceReportRepository(IRepository dbRepository)
        {
            _dbRepository = dbRepository;
            _logger = new LogService();
        }


        public async Task<IEnumerable<RoomOccupancyInvoiceReportDto>> GetInvoice(CancellationToken token, RoomOccupancyInvoiceReportDto roomOccupancyInvoiceReportDto)
        {

            try
            {
                var parameters = new List<ParametersCollection>
                {
                    new() { ParameterName = "@ReportType",  ParameterValue = roomOccupancyInvoiceReportDto.ReportType, ParameterType = DbType.String , ParameterDirection = ParameterDirection.Input},
                    new() { ParameterName = "@StartDate",  ParameterValue = roomOccupancyInvoiceReportDto.StartDate, ParameterType = DbType.DateTime , ParameterDirection = ParameterDirection.Input},
                    new() { ParameterName = "@EndDate",  ParameterValue = roomOccupancyInvoiceReportDto.EndDate, ParameterType = DbType.DateTime , ParameterDirection = ParameterDirection.Input},
                    new() { ParameterName = "@MinPercent",  ParameterValue = roomOccupancyInvoiceReportDto.MinPercent, ParameterType = DbType.Decimal , ParameterDirection = ParameterDirection.Input},
                    new() { ParameterName = "@MaxPercent",  ParameterValue = roomOccupancyInvoiceReportDto.MaxPercent, ParameterType = DbType.Decimal , ParameterDirection = ParameterDirection.Input},

                };
                var roomOccupancyReportList = await _dbRepository.ExecuteSpListAsync<RoomOccupancyInvoiceReportDto>(token, "GetRoomOccupancyReport", parameters);
                return roomOccupancyReportList;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error Executing Procedure CheckReservation");
                throw;
            }
        }




    }
}




