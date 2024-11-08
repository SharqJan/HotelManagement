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
    public class InvoiceReportRepository : IInvoiceReportRepository
    {
        private readonly IRepository _dbRepository;
        private readonly LogService _logger;

        public InvoiceReportRepository(IRepository dbRepository)
        {
            _dbRepository = dbRepository;
            _logger = new LogService();
        }


        public async Task<IEnumerable<InvoiceReportDto>> GetDailyInvoice(CancellationToken token, InvoiceReportDto invoiceReportDto)
        {

            try
            {
                var parameters = new List<ParametersCollection>
                {
                    new() { ParameterName = "@ReportType",  ParameterValue = invoiceReportDto.ReportType, ParameterType = DbType.String , ParameterDirection = ParameterDirection.Input},
                    new() { ParameterName = "@StartDate",  ParameterValue = invoiceReportDto.StartDate, ParameterType = DbType.DateTime , ParameterDirection = ParameterDirection.Input},
                    new() { ParameterName = "@EndDate",  ParameterValue = invoiceReportDto.EndDate, ParameterType = DbType.DateTime , ParameterDirection = ParameterDirection.Input},
                    new() { ParameterName = "@MinRevenue",  ParameterValue = invoiceReportDto.MinRevenue, ParameterType = DbType.Int64 , ParameterDirection = ParameterDirection.Input},
                     new() { ParameterName ="@MaxRevenue",  ParameterValue = invoiceReportDto.MaxRevenue, ParameterType = DbType.Int64 , ParameterDirection = ParameterDirection.Input},

                };
                var revenueReportList = await _dbRepository.ExecuteSpListAsync<InvoiceReportDto>(token, "GetRevenueReport", parameters);
                return revenueReportList;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error Executing Procedure CheckReservation");
                throw;
            }
        }














    }
}


