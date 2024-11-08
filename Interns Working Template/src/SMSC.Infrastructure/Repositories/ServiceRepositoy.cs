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
    public class ServiceRepository : IServiceRepository
    {
        private readonly IRepository _dbRepository;
        private readonly LogService _logger;

        public ServiceRepository(IRepository dbRepository)
        {
            _dbRepository = dbRepository;
            _logger = new LogService();
        }

        public async Task<Service> GetServiceByIdAsync(CancellationToken cancellationToken, int ServiceId)
        {
            try
            {
                var parameters = new List<ParametersCollection> {
                    new() {ParameterName  = "@ServiceId", ParameterValue = ServiceId, ParameterType = DbType.Int64, ParameterDirection = ParameterDirection.Input}
                };

                // *** 
                // use this function for getting single Row  _dbRepository.ExecuteSpSingleAsync()

                var resultService = await _dbRepository.ExecuteSpSingleAsync<Service>(cancellationToken, "GetServiceByID", parameters);
                return resultService;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error Executing GetServiceByID");
                return default;
            }
        }

        public async Task<long> AddServiceAsync(CancellationToken cancellationToken, Service service)
        {
            try
            {
                var parameters = new List<ParametersCollection>
                {
                    new() { ParameterName = "@ServiceName",  ParameterValue = service.ServiceName, ParameterType = DbType.String , ParameterDirection = ParameterDirection.Input},
                     new() { ParameterName = "@ServiceCharges",  ParameterValue = service.ServiceCharges, ParameterType = DbType.Decimal , ParameterDirection = ParameterDirection.Input},
                    new() { ParameterName = "@IsActive",  ParameterValue = service.IsActive, ParameterType = DbType.Boolean , ParameterDirection = ParameterDirection.Input},
                   


                };

                // *** 
                // use this function for getting a single value as return value from proc  _dbRepository.ExecuteSpReturnValueAsync()
                var resultServiceId = await _dbRepository.ExecuteSpReturnValueAsync(cancellationToken, "AddService", parameters);
                return resultServiceId;


            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error Executing AddService");
                return default;
            }
        }



        public async Task<long> UpdateServiceAsync(CancellationToken cancellationToken, Service service)
        {
            try
            {
                var parameters = new List<ParametersCollection>
                {
                    new() { ParameterName = "@ServiceId",  ParameterValue = service.ServiceId, ParameterType = DbType.Int32 , ParameterDirection = ParameterDirection.Input},
                    new() { ParameterName = "@ServiceName",  ParameterValue = service.ServiceName, ParameterType = DbType.String , ParameterDirection = ParameterDirection.Input},
                    new() { ParameterName = "@ServiceCharges",  ParameterValue = service.ServiceCharges, ParameterType = DbType.Decimal , ParameterDirection = ParameterDirection.Input},
                    new() { ParameterName = "@IsActive",  ParameterValue = service.IsActive, ParameterType = DbType.Boolean , ParameterDirection = ParameterDirection.Input},
                    

                };

                var resultServiceId = await _dbRepository.ExecuteSpReturnValueAsync(cancellationToken, "UpdateService", parameters);
                return resultServiceId;


            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error Executing UpdateService");
                return default;
            }
        }

        public async Task<long> DeleteServiceByIdAsync(CancellationToken cancellationToken, int serviceId)
        {
            try
            {
                var parameters = new List<ParametersCollection>
                {
                    new() { ParameterName = "@ServiceId",  ParameterValue = serviceId, ParameterType = DbType.Int32 , ParameterDirection = ParameterDirection.Input},
                };

                var resultServiceId = await _dbRepository.ExecuteSpReturnValueAsync(cancellationToken, "DeleteServiceById", parameters);
                return resultServiceId;


            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error Executing DeleteRoleById");
                return default;
            }
        }

        public async Task<IEnumerable<Service>> GetServiceListAsync<Service>(CancellationToken token)
        {

            try
            {
                // *** 
                // use this function for getting list  _dbRepository.ExecuteSpListAsync()
                var serviceList = await _dbRepository.ExecuteSpListAsync<Service>(token, "GetServiceList", null);
                return serviceList;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error Executing Procedure GetServiceList");
                throw;
            }
        }
    }
}
