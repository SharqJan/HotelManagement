using SMSC.Application.DTO;
using SMSC.Application.Interfaces;
using SMSC.Core.Entities;
using SMSC.Core.Interfaces;
using SMSC.Core.Logger.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace SMSC.Infrastructure.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly IRepository _dbRepository;
        private readonly LogService _logger;

        public RoomRepository(IRepository dbRepository)
        {
            _dbRepository = dbRepository;
            _logger = new LogService();
        }

        public async Task<RoomDTO> GetRoomByIdAsync(CancellationToken cancellationToken, int RoomId)
        {
            try
            {
                var parameters = new List<ParametersCollection> {
                    new() {ParameterName  = "@RoomId", ParameterValue = RoomId, ParameterType = DbType.Int64, ParameterDirection = ParameterDirection.Input}
                };

                // *** 
                // use this function for getting single Row  _dbRepository.ExecuteSpSingleAsync()

                var resultRoom = await _dbRepository.ExecuteSpSingleAsync<RoomDTO>(cancellationToken, "GetRoomById", parameters);
                return resultRoom;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error Executing GetRoomByID");
                return default;
            }
        }

        public async Task<long> AddRoomAsync(CancellationToken cancellationToken, Room room)
        {
            try
            {
                var parameters = new List<ParametersCollection>
                {
                    new() { ParameterName = "@FloorNo",  ParameterValue = room.FloorNo, ParameterType = DbType.Int32 , ParameterDirection = ParameterDirection.Input},
                     new() { ParameterName = "@RoomNo",  ParameterValue = room.RoomNo, ParameterType = DbType.Int32 , ParameterDirection = ParameterDirection.Input},
                     new() { ParameterName = "@RoomStatus",  ParameterValue = room.RoomStatus, ParameterType = DbType.String , ParameterDirection = ParameterDirection.Input},
                    new() { ParameterName = "@RoomDefaultPrice",  ParameterValue = room.RoomDefaultPrice, ParameterType = DbType.Decimal , ParameterDirection = ParameterDirection.Input},
                    new() { ParameterName = "@RoomAdditionalTax",  ParameterValue = room.RoomAdditionalTax, ParameterType = DbType.Decimal , ParameterDirection = ParameterDirection.Input},
                    new() { ParameterName = "@RoomImage1",  ParameterValue = room.RoomImages.Count > 0 ? room.RoomImages[0] : null, ParameterType = DbType.Binary , ParameterDirection = ParameterDirection.Input},
                    new() { ParameterName = "@RoomImage2",  ParameterValue = room.RoomImages.Count > 1  ? room.RoomImages[1] : null, ParameterType = DbType.Binary , ParameterDirection = ParameterDirection.Input},
                    new() { ParameterName = "@RoomImage3",  ParameterValue = room.RoomImages.Count > 2 ? room.RoomImages[2] : null, ParameterType = DbType.Binary , ParameterDirection = ParameterDirection.Input},
                    new() { ParameterName = "@RoomImage4",  ParameterValue = room.RoomImages.Count > 3 ? room.RoomImages[3] : null, ParameterType = DbType.Binary , ParameterDirection = ParameterDirection.Input},
                    new() { ParameterName = "@RoomImage5",  ParameterValue = room.RoomImages.Count > 4 ? room.RoomImages[4] : null, ParameterType = DbType.Binary , ParameterDirection = ParameterDirection.Input},
                    new() { ParameterName = "@IsRoomAvailable",  ParameterValue = room.IsRoomAvailable, ParameterType = DbType.Boolean , ParameterDirection = ParameterDirection.Input},




                };

                // *** 
                // use this function for getting a single value as return value from proc  _dbRepository.ExecuteSpReturnValueAsync()
                var resultRoomId = await _dbRepository.ExecuteSpReturnValueAsync(cancellationToken, "AddRoom", parameters);
                return resultRoomId;


            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error Executing AddRoom");
                return default;
            }
        }



        public async Task<long> UpdateRoomAsync(CancellationToken cancellationToken, Room room)
        {
            try
            {
                var parameters = new List<ParametersCollection>
                {
                    new() { ParameterName = "@RoomId",  ParameterValue = room.RoomId, ParameterType = DbType.Int32 , ParameterDirection = ParameterDirection.Input},
                    new() { ParameterName = "@FloorNo",  ParameterValue = room.FloorNo, ParameterType = DbType.Int32 , ParameterDirection = ParameterDirection.Input},
                     new() { ParameterName = "@RoomNo",  ParameterValue = room.RoomNo, ParameterType = DbType.Int32 , ParameterDirection = ParameterDirection.Input},
                    new() { ParameterName = "@RoomDefaultPrice",  ParameterValue = room.RoomDefaultPrice, ParameterType = DbType.Decimal , ParameterDirection = ParameterDirection.Input},
                    new() { ParameterName = "@RoomAdditionalTax",  ParameterValue = room.RoomAdditionalTax, ParameterType = DbType.Decimal , ParameterDirection = ParameterDirection.Input},
                    new() { ParameterName = "@RoomStatus",  ParameterValue = room.RoomStatus, ParameterType = DbType.String , ParameterDirection = ParameterDirection.Input},
                    new() { ParameterName = "@RoomImage1",  ParameterValue = room.RoomImages.Count > 0 ? room.RoomImages[0] : null, ParameterType = DbType.Binary , ParameterDirection = ParameterDirection.Input},
                    new() { ParameterName = "@RoomImage2",  ParameterValue = room.RoomImages.Count > 1  ? room.RoomImages[1] : null, ParameterType = DbType.Binary , ParameterDirection = ParameterDirection.Input},
                    new() { ParameterName = "@RoomImage3",  ParameterValue = room.RoomImages.Count > 2 ? room.RoomImages[2] : null, ParameterType = DbType.Binary , ParameterDirection = ParameterDirection.Input},
                    new() { ParameterName = "@RoomImage4",  ParameterValue = room.RoomImages.Count > 3 ? room.RoomImages[3] : null, ParameterType = DbType.Binary , ParameterDirection = ParameterDirection.Input},
                    new() { ParameterName = "@RoomImage5",  ParameterValue = room.RoomImages.Count > 4 ? room.RoomImages[4] : null, ParameterType = DbType.Binary , ParameterDirection = ParameterDirection.Input},
                    new() { ParameterName = "@IsRoomAvailable",  ParameterValue = room.IsRoomAvailable, ParameterType = DbType.Boolean , ParameterDirection = ParameterDirection.Input},


                };

                var resultRoomId = await _dbRepository.ExecuteSpReturnValueAsync(cancellationToken, "UpdateRoom", parameters);
                return resultRoomId;


            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error Executing UpdateRoom");
                return default;
            }
        }

        public async Task<long> DeleteRoomByIdAsync(CancellationToken cancellationToken, int roomId)
        {
            try
            {
                var parameters = new List<ParametersCollection>
                {
                    new() { ParameterName = "@RoomId",  ParameterValue = roomId, ParameterType = DbType.Int32 , ParameterDirection = ParameterDirection.Input},
                };

                var resultRoomId = await _dbRepository.ExecuteSpReturnValueAsync(cancellationToken, "DeleteRoomById", parameters);
                return resultRoomId;


            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error Executing DeleteRoomById");
                return default;
            }
        }

        public async Task<IEnumerable<RoomDTO>> GetRoomListAsync<RoomDTO>(CancellationToken token)
        {

            try
            {
                // *** 
                // use this function for getting list  _dbRepository.ExecuteSpListAsync()
                var roomList = await _dbRepository.ExecuteSpListAsync<RoomDTO>(token, "GetRoomList", null);
                return roomList;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error Executing Procedure GetRoomList");
                throw;
            }
        }
    }
}
