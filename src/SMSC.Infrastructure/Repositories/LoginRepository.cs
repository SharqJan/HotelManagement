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
    public class LoginRepository : ILoginRepository
    {
        private readonly IRepository _dbRepository;
        private readonly LogService _logger;

        public LoginRepository(IRepository dbRepository)
        {
            _dbRepository = dbRepository;
            _logger = new LogService();
        }



        public async Task<IEnumerable<LoginDto>> Login(CancellationToken token, LoginDto loginDto)
        {

            try
            {
                var parameters = new List<ParametersCollection>
                {
                    new() { ParameterName = "@Email",  ParameterValue = loginDto.Email, ParameterType = DbType.String , ParameterDirection = ParameterDirection.Input},
         

                };
                var loginResult = await _dbRepository.ExecuteSpListAsync<LoginDto>(token, "Login", parameters);
                return loginResult;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error Executing Procedure Login");
                throw;
            }
        }



    }
}
