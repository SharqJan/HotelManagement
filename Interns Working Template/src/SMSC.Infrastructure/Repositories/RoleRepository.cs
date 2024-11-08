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
    public class RoleRepository : IRoleRepository
    {
        private readonly IRepository _dbRepository;
        private readonly LogService _logger;

        public RoleRepository(IRepository dbRepository)
        {
            _dbRepository = dbRepository;
            _logger = new LogService();
        }

        public async Task<Roles> GetRoleByIdAsync(CancellationToken cancellationToken, int RoleId)
        {
            try
            {
                var parameters = new List<ParametersCollection> {
                    new() {ParameterName  = "@RoleId", ParameterValue = RoleId, ParameterType = DbType.Int64, ParameterDirection = ParameterDirection.Input}
                };

                // *** 
                // use this function for getting single Row  _dbRepository.ExecuteSpSingleAsync()

                var resultRole = await _dbRepository.ExecuteSpSingleAsync<Roles>(cancellationToken, "GetRoleByID", parameters);
                return resultRole;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error Executing GetRoleById");
                return default;
            }
        }

        public async Task<long> AddRoleAsync(CancellationToken cancellationToken, Roles role)
        {
            try
            {
                var parameters = new List<ParametersCollection>
                {
                    new() { ParameterName = "@RoleName",  ParameterValue = role.RoleName, ParameterType = DbType.String , ParameterDirection = ParameterDirection.Input},
                    new() { ParameterName = "@IsActive",  ParameterValue = role.IsActive, ParameterType = DbType.Boolean , ParameterDirection = ParameterDirection.Input},
                    new() { ParameterName = "@CreatedBy",  ParameterValue = role.CreatedBy, ParameterType = DbType.Int32 , ParameterDirection = ParameterDirection.Input},



                };

                // *** 
                var resultRoleId = await _dbRepository.ExecuteSpReturnValueAsync(cancellationToken, "AddRole", parameters);
                return resultRoleId;


            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error Executing AddRole");
                return default;
            }
        }



        public async Task<long> UpdateRoleAsync(CancellationToken cancellationToken, Roles role)
        {
            try
            {
                var parameters = new List<ParametersCollection>
                {
                    new() { ParameterName = "@RoleId",  ParameterValue = role.RoleId, ParameterType = DbType.Int32 , ParameterDirection = ParameterDirection.Input},
                    new() { ParameterName = "@RoleName",  ParameterValue = role.RoleName, ParameterType = DbType.String , ParameterDirection = ParameterDirection.Input},
                    new() { ParameterName = "@IsActive",  ParameterValue = role.IsActive, ParameterType = DbType.Boolean , ParameterDirection = ParameterDirection.Input},
                   

                };

                var resultRoleId = await _dbRepository.ExecuteSpReturnValueAsync(cancellationToken, "UpdateRole", parameters);
                return resultRoleId;


            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error Executing UpdateRole");
                return default;
            }
        }

        public async Task<long> DeleteRoleByIdAsync(CancellationToken cancellationToken, int roleId)
        {
            try
            {
                var parameters = new List<ParametersCollection>
                {
                    new() { ParameterName = "@RoleId",  ParameterValue = roleId, ParameterType = DbType.Int32 , ParameterDirection = ParameterDirection.Input},
                };

                var resultRoleId = await _dbRepository.ExecuteSpReturnValueAsync(cancellationToken, "DeleteRoleById", parameters);
                return resultRoleId;


            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error Executing DeleteRoleById");
                return default;
            }
        }

        public async Task<IEnumerable<Roles>> GetRoleListAsync<Roles>(CancellationToken token)
        {

            try
            {
                var roleList = await _dbRepository.ExecuteSpListAsync<Roles>(token, "GetRoleList", null);
                return roleList;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error Executing Procedure GetRoleList");
                throw;
            }
        }
    }
}
