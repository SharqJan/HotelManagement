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
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IRepository _dbRepository;
        private readonly LogService _logger;

        public EmployeeRepository(IRepository dbRepository)
        {
            _dbRepository = dbRepository;
            _logger = new LogService();
        }

        public async Task<Employee> GetEmployeeByIdAsync(CancellationToken cancellationToken, int employeeId)
        {
            try
            {
                var parameters = new List<ParametersCollection> {
                    new() {ParameterName  = "@EmployeeId", ParameterValue = employeeId, ParameterType = DbType.Int64, ParameterDirection = ParameterDirection.Input}
                };

                // *** 
                // use this function for getting single Row  _dbRepository.ExecuteSpSingleAsync()

                var resultEmployee = await _dbRepository.ExecuteSpSingleAsync<Employee>(cancellationToken, "GetEmployeeById", parameters);
                return resultEmployee;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error Executing GetEmployeeById");
                return default;
            }
        }

        public async Task<long> AddEmployeeAsync(CancellationToken cancellationToken, Employee employee)
        {
            try
            {
                var parameters = new List<ParametersCollection>
                {
                    new() { ParameterName = "@Name",  ParameterValue = employee.Name, ParameterType = DbType.String , ParameterDirection = ParameterDirection.Input},
                    new() { ParameterName = "@Description",  ParameterValue = employee.Description, ParameterType = DbType.String , ParameterDirection = ParameterDirection.Input},
                    new() { ParameterName = "@Email",  ParameterValue = employee.Email, ParameterType = DbType.String , ParameterDirection = ParameterDirection.Input},
                    new() { ParameterName = "@PhoneNumber",  ParameterValue = employee.PhoneNumber, ParameterType = DbType.String , ParameterDirection = ParameterDirection.Input},
                    new() { ParameterName = "@Address",  ParameterValue = employee.Address, ParameterType = DbType.String , ParameterDirection = ParameterDirection.Input}
                };

                // *** 
                // use this function for getting a single value as return value from proc  _dbRepository.ExecuteSpReturnValueAsync()
                var resultEmployeeId = await _dbRepository.ExecuteSpReturnValueAsync(cancellationToken, "AddEmployee", parameters);
                return resultEmployeeId;


            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error Executing AddEmployee");
                return default;
            }
        }

  

        public async Task<long> UpdateEmployeeAsync(CancellationToken cancellationToken, Employee employee)
        {
            try
            {
                var parameters = new List<ParametersCollection>
                {
                    new() { ParameterName = "@EmployeeId",  ParameterValue = employee.EmployeeId, ParameterType = DbType.Int32 , ParameterDirection = ParameterDirection.Input},
                    new() { ParameterName = "@Name",  ParameterValue = employee.Name, ParameterType = DbType.String , ParameterDirection = ParameterDirection.Input},
                    new() { ParameterName = "@Description",  ParameterValue = employee.Description, ParameterType = DbType.String , ParameterDirection = ParameterDirection.Input},
                    new() { ParameterName = "@Email",  ParameterValue = employee.Email, ParameterType = DbType.String , ParameterDirection = ParameterDirection.Input},
                    new() { ParameterName = "@PhoneNumber",  ParameterValue = employee.PhoneNumber, ParameterType = DbType.String , ParameterDirection = ParameterDirection.Input},
                    new() { ParameterName = "@Address",  ParameterValue = employee.Address, ParameterType = DbType.String , ParameterDirection = ParameterDirection.Input}

                };

                var resultEmployeeId = await _dbRepository.ExecuteSpReturnValueAsync(cancellationToken, "UpdateEmployee", parameters);
                return resultEmployeeId;


            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error Executing AddEmployeeAsync");
                return default;
            }
        }

       public async  Task<long> DeleteEmployeeByIdAsync(CancellationToken cancellationToken, int employeeId)
        {
            try
            {
                var parameters = new List<ParametersCollection>
                {
                    new() { ParameterName = "@EmployeeId",  ParameterValue = employeeId, ParameterType = DbType.Int32 , ParameterDirection = ParameterDirection.Input},
                };

                var resultEmployeeId = await _dbRepository.ExecuteSpReturnValueAsync(cancellationToken, "DeleteEmployeeById", parameters);
                return resultEmployeeId;


            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error Executing DeleteEmployeeById");
                return default;
            }
        }

        public async Task<IEnumerable<Employee>> GetEmployeeListAsync<Employee>(CancellationToken token)
        {

            try
            {
                // *** 
                // use this function for getting list  _dbRepository.ExecuteSpListAsync()
                var employeeList = await _dbRepository.ExecuteSpListAsync<Employee>(token, "GetEmployeeList", null);
                return employeeList;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error Executing Procedure GetEmployeeList");
                throw;
            }
        }
    }
}
