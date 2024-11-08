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
    public class UserRepository : IUserRepository
    {
        private readonly IRepository _dbRepository;
        private readonly LogService _logger;

        public UserRepository(IRepository dbRepository)
        {
            _dbRepository = dbRepository;
            _logger = new LogService();
        }

        public async Task<UserDTO> GetUserByIdAsync(CancellationToken cancellationToken, int UserId)
        {
            try
            {
                var parameters = new List<ParametersCollection> {
                    new() {ParameterName  = "@UserId", ParameterValue = UserId, ParameterType = DbType.Int64, ParameterDirection = ParameterDirection.Input}
                };

                // *** 
                // use this function for getting single Row  _dbRepository.ExecuteSpSingleAsync()

                var userRole = await _dbRepository.ExecuteSpSingleAsync<UserDTO>(cancellationToken, "GetUserById", parameters);
                return userRole;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error Executing GetUserById");
                return default;
            }
        }

        public async Task<IEnumerable<UserDTO>> GetUserListAsync<UserDTO>(CancellationToken token)
        {

            try
            {
                // *** 
                // use this function for getting list  _dbRepository.ExecuteSpListAsync()
                var userList = await _dbRepository.ExecuteSpListAsync<UserDTO>(token, "GetUserList", null);
                return userList;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error Executing Procedure GetUserList");
                throw;
            }
        }
        public async Task<long> AddUserAsync(CancellationToken cancellationToken, Users user)
        {
            try
            {
                var parameters = new List<ParametersCollection>
                {
                    new() { ParameterName = "@FirstName",  ParameterValue = user.FirstName, ParameterType = DbType.String , ParameterDirection = ParameterDirection.Input},
                    new() { ParameterName = "@LastName",  ParameterValue = user.LastName, ParameterType = DbType.String , ParameterDirection = ParameterDirection.Input},
                    new() { ParameterName = "@Email",  ParameterValue = user.Email, ParameterType = DbType.String , ParameterDirection = ParameterDirection.Input},
                    new() { ParameterName = "@Password",  ParameterValue = user.Password, ParameterType = DbType.String , ParameterDirection = ParameterDirection.Input},
                    new() { ParameterName = "@PhoneNo",  ParameterValue = user.PhoneNo, ParameterType = DbType.String , ParameterDirection = ParameterDirection.Input},
                    new() { ParameterName = "@ProfileImage",  ParameterValue = user.ProfileImage.Length > 0 ? user.ProfileImage : null, ParameterType = DbType.Binary , ParameterDirection = ParameterDirection.Input},
                    new() { ParameterName = "@CreatedBy",  ParameterValue = user.CreatedBy, ParameterType = DbType.Int32 , ParameterDirection = ParameterDirection.Input},
                    new() { ParameterName = "@RoleId",  ParameterValue = user.RoleId, ParameterType = DbType.Int64 , ParameterDirection = ParameterDirection.Input},
                    new() { ParameterName = "@IsActive",  ParameterValue = user.IsActive, ParameterType = DbType.Boolean , ParameterDirection = ParameterDirection.Input},


                };

                // *** 
                var resultUserId = await _dbRepository.ExecuteSpReturnValueAsync(cancellationToken, "AddUser", parameters);
                return resultUserId;


            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error Executing AddUser");
                return default;
            }
        }




        public async Task<long> UpdateUserAsync(CancellationToken cancellationToken, Users user)
        {
            try
            {
                var parameters = new List<ParametersCollection>
                {
                    new() { ParameterName = "@UserId",  ParameterValue = user.UserId, ParameterType = DbType.Int32 , ParameterDirection = ParameterDirection.Input},
                    new() { ParameterName = "@FirstName",  ParameterValue = user.FirstName, ParameterType = DbType.String , ParameterDirection = ParameterDirection.Input},
                    new() { ParameterName = "@LastName",  ParameterValue = user.LastName, ParameterType = DbType.String , ParameterDirection = ParameterDirection.Input},
                    new() { ParameterName = "@Email",  ParameterValue = user.Email, ParameterType = DbType.String , ParameterDirection = ParameterDirection.Input},
                    new() { ParameterName = "@Password",  ParameterValue = user.Password, ParameterType = DbType.String , ParameterDirection = ParameterDirection.Input},
                    new() { ParameterName = "@PhoneNo",  ParameterValue = user.PhoneNo, ParameterType = DbType.String , ParameterDirection = ParameterDirection.Input},
                    new() { ParameterName = "@ProfileImage",  ParameterValue = user.ProfileImage.Length > 0 ? user.ProfileImage : null, ParameterType = DbType.Binary , ParameterDirection = ParameterDirection.Input},
                    new() { ParameterName = "@RoleId",  ParameterValue = user.RoleId, ParameterType = DbType.Int64 , ParameterDirection = ParameterDirection.Input},
                    new() { ParameterName = "@IsActive",  ParameterValue = user.IsActive, ParameterType = DbType.Boolean , ParameterDirection = ParameterDirection.Input},


                };

                var resultUserId = await _dbRepository.ExecuteSpReturnValueAsync(cancellationToken, "UpdateUser", parameters);
                return resultUserId;


            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error Executing UpdateUser");
                return default;
            }
        }

        public async Task<long> DeleteUserByIdAsync(CancellationToken cancellationToken, int userId)
        {
            try
            {
                var parameters = new List<ParametersCollection>
                {
                    new() { ParameterName = "@UserId",  ParameterValue = userId, ParameterType = DbType.Int32 , ParameterDirection = ParameterDirection.Input},
                };

                var resultRoleId = await _dbRepository.ExecuteSpReturnValueAsync(cancellationToken, "DeleteUserById", parameters);
                return resultRoleId;


            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error Executing DeleteUserById");
                return default;
            }
        }

       
    }
}
