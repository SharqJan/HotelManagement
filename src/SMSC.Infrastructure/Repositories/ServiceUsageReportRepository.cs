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
    public class ServiceUsageReportRepository : IServiceUsageReportRepository
    {
        private readonly IRepository _dbRepository;
        private readonly LogService _logger;

        public ServiceUsageReportRepository(IRepository dbRepository)
        {
            _dbRepository = dbRepository;
            _logger = new LogService();
        }


        public async Task<IEnumerable<ServiceUsageReportDto>> GetRevenueByServiceType(CancellationToken token, ServiceUsageReportDto serviceUsageReportDto)
        {

            try
            {
                var parameters = new List<ParametersCollection>
                {
                    new() { ParameterName = "@ServiceType",  ParameterValue = serviceUsageReportDto.ServiceName, ParameterType = DbType.String , ParameterDirection = ParameterDirection.Input},
                    new() { ParameterName = "@StartDate",  ParameterValue = serviceUsageReportDto.StartDate, ParameterType = DbType.DateTime , ParameterDirection = ParameterDirection.Input},
                    new() { ParameterName = "@EndDate",  ParameterValue = serviceUsageReportDto.EndDate, ParameterType = DbType.DateTime , ParameterDirection = ParameterDirection.Input},
   

                };
                var revenueByServiceList = await _dbRepository.ExecuteSpListAsync<ServiceUsageReportDto>(token, "RevenueBreakdownByService", parameters);
                return revenueByServiceList;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error Executing Procedure RevenueBreakdownByService");
                throw;
            }
        }









    }
}
