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
    public class GuestRepository : IGuestRepository
    {
        private readonly IRepository _dbRepository;
        private readonly LogService _logger;

        public GuestRepository(IRepository dbRepository)
        {
            _dbRepository = dbRepository;
            _logger = new LogService();
        }

        public async Task<GuestDTO> GetGuestByIdAsync(CancellationToken cancellationToken, int GuestId)
        {
            try
            {
                var parameters = new List<ParametersCollection> {
                    new() {ParameterName  = "@GuestId", ParameterValue = GuestId, ParameterType = DbType.Int64, ParameterDirection = ParameterDirection.Input}
                };

                // *** 
                // use this function for getting single Row  _dbRepository.ExecuteSpSingleAsync()

                var guest = await _dbRepository.ExecuteSpSingleAsync<GuestDTO>(cancellationToken, "GetGuestById", parameters);
                return guest;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error Executing GetGuestById");
                return default;
            }
        }

        public async Task<IEnumerable<GuestDTO>> GetGuestListAsync<GuestDTO>(CancellationToken token)
        {

            try
            {
                // *** 
                // use this function for getting list  _dbRepository.ExecuteSpListAsync()
                var guestList = await _dbRepository.ExecuteSpListAsync<GuestDTO>(token, "GetGuestList", null);
                return guestList;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error Executing Procedure GetGuestList");
                throw;
            }
        }
        public async Task<long> AddGuestAsync(CancellationToken cancellationToken, Guest guest)
        {
            int length = guest.IdCard.Length; // ***
            try
            {
                var parameters = new List<ParametersCollection>
                {
                    new() { ParameterName = "@ParentId",  ParameterValue = guest.ParentId, ParameterType = DbType.Int32 , ParameterDirection = ParameterDirection.Input},
                    new() { ParameterName = "@FirstName",  ParameterValue = guest.FirstName, ParameterType = DbType.String , ParameterDirection = ParameterDirection.Input},
                    new() { ParameterName = "@LastName",  ParameterValue = guest.LastName, ParameterType = DbType.String , ParameterDirection = ParameterDirection.Input},
                    new() { ParameterName = "@Email",  ParameterValue = guest.Email, ParameterType = DbType.String , ParameterDirection = ParameterDirection.Input},
                    new() { ParameterName = "@PhoneNo",  ParameterValue = guest.PhoneNo, ParameterType = DbType.String , ParameterDirection = ParameterDirection.Input},
                    new() { ParameterName = "@Address",  ParameterValue = guest.Address, ParameterType = DbType.String , ParameterDirection = ParameterDirection.Input},
                    new() { ParameterName = "@IdCardType",  ParameterValue = guest.IdCardType, ParameterType = DbType.String , ParameterDirection = ParameterDirection.Input},
                    new() { ParameterName = "@IdCard",  ParameterValue = guest.IdCard.Length > 0 ? guest.IdCard : null, ParameterType = DbType.Binary , ParameterDirection = ParameterDirection.Input},
                    new() { ParameterName = "@IsActive",  ParameterValue = guest.IsActive, ParameterType = DbType.Boolean , ParameterDirection = ParameterDirection.Input},
                    new() { ParameterName = "@AssignParent",  ParameterValue = guest.AssignParent, ParameterType = DbType.Boolean , ParameterDirection = ParameterDirection.Input},


                };

                // *** 
                // use this function for getting a single value as return value from proc  _dbRepository.ExecuteSpReturnValueAsync()
                var resultGuestId = await _dbRepository.ExecuteSpReturnValueAsync(cancellationToken, "AddGuest", parameters);
                return resultGuestId;


            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error Executing AddGuest");
                return default;
            }
        }




        public async Task<long> UpdateGuestAsync(CancellationToken cancellationToken, Guest guest)
        {
            try
            {
                var parameters = new List<ParametersCollection>
                {
                    new() { ParameterName = "@GuestId",  ParameterValue = guest.GuestId, ParameterType = DbType.Int32 , ParameterDirection = ParameterDirection.Input},
                    new() { ParameterName = "@ParentId",  ParameterValue = guest.ParentId, ParameterType = DbType.Int32 , ParameterDirection = ParameterDirection.Input},
                    new() { ParameterName = "@FirstName",  ParameterValue = guest.FirstName, ParameterType = DbType.String , ParameterDirection = ParameterDirection.Input},
                    new() { ParameterName = "@LastName",  ParameterValue = guest.LastName, ParameterType = DbType.String , ParameterDirection = ParameterDirection.Input},
                    new() { ParameterName = "@Email",  ParameterValue = guest.Email, ParameterType = DbType.String , ParameterDirection = ParameterDirection.Input},
                    new() { ParameterName = "@PhoneNo",  ParameterValue = guest.PhoneNo, ParameterType = DbType.String , ParameterDirection = ParameterDirection.Input},
                    new() { ParameterName = "@Address",  ParameterValue = guest.Address, ParameterType = DbType.String , ParameterDirection = ParameterDirection.Input},
                    new() { ParameterName = "@IdCardType",  ParameterValue = guest.IdCardType, ParameterType = DbType.String , ParameterDirection = ParameterDirection.Input},
                    new() { ParameterName = "@IdCard",  ParameterValue = guest.IdCard.Length > 0 ? guest.IdCard : null, ParameterType = DbType.Binary , ParameterDirection = ParameterDirection.Input},
                    new() { ParameterName = "@AssignParent",  ParameterValue = guest.AssignParent, ParameterType = DbType.Boolean , ParameterDirection = ParameterDirection.Input},
                    new() { ParameterName = "@IsActive",  ParameterValue = guest.IsActive, ParameterType = DbType.Boolean , ParameterDirection = ParameterDirection.Input},
                    new() { ParameterName = "@RoomId",  ParameterValue = guest.RoomId, ParameterType = DbType.Int32 , ParameterDirection = ParameterDirection.Input},
                    new() { ParameterName = "@ServiceIds",  ParameterValue = guest.ServiceIds, ParameterType = DbType.String , ParameterDirection = ParameterDirection.Input},



                };

                var resultGuestId = await _dbRepository.ExecuteSpReturnValueAsync(cancellationToken, "UpdateGuest", parameters);
                return resultGuestId;


            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error Executing UpdateGuest");
                return default;
            }
        }

        public async Task<long> DeleteGuestByIdAsync(CancellationToken cancellationToken, int guestId)
        {
            try
            {
                var parameters = new List<ParametersCollection>
                {
                    new() { ParameterName = "@GuestId",  ParameterValue = guestId, ParameterType = DbType.Int32 , ParameterDirection = ParameterDirection.Input},
                };

                var resultGuestId = await _dbRepository.ExecuteSpReturnValueAsync(cancellationToken, "DeleteGuestById", parameters);
                return resultGuestId;


            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error Executing DeleteGuestById");
                return default;
            }
        }


    }
}
