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
    public class CheckInCheckOutRepository : ICheckInCheckOutRepository
    {
        private readonly IRepository _dbRepository;
        private readonly LogService _logger;

        public CheckInCheckOutRepository(IRepository dbRepository)
        {
            _dbRepository = dbRepository;
            _logger = new LogService();
        }



        public async Task<InvoiceDto> GetInvoiceByRoomIdAsync(CancellationToken cancellationToken, int RoomId)
        {
            try
            {
                var parameters = new List<ParametersCollection> {
                    new() {ParameterName  = "@RoomId", ParameterValue = RoomId, ParameterType = DbType.Int64, ParameterDirection = ParameterDirection.Input}
                };



                var invoice = await _dbRepository.ExecuteSpSingleAsync<InvoiceDto>(cancellationToken, "GenerativeInvoice", parameters);
                return invoice;
            }
            catch (Exception ex)
            {

                _logger.Error(ex, "Error Executing GenerativeInvoice");
                return default;
            }
        }



        public async Task<IEnumerable<ReservationDto>> CheckReservationListAsync(CancellationToken token, ReservationDto reservation)
        {

            try
            {
                var parameters = new List<ParametersCollection>
                {
                    new() { ParameterName = "@FirstName",  ParameterValue = reservation.FirstName, ParameterType = DbType.String , ParameterDirection = ParameterDirection.Input},
                    new() { ParameterName = "@LastName",  ParameterValue = reservation.LastName, ParameterType = DbType.String , ParameterDirection = ParameterDirection.Input},
                    new() { ParameterName = "@Email",  ParameterValue = reservation.Email, ParameterType = DbType.String , ParameterDirection = ParameterDirection.Input},
                    new() { ParameterName = "@PhoneNo",  ParameterValue = reservation.PhoneNo, ParameterType = DbType.String , ParameterDirection = ParameterDirection.Input},



                };
                var reservationList = await _dbRepository.ExecuteSpListAsync<ReservationDto>(token, "CheckReservation", parameters);
                return reservationList;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error Executing Procedure CheckReservation");
                throw;
            }

        }


        public async Task<IEnumerable<CheckOutDto>> GetRoomsByGuestId(CancellationToken token, int? GuestId, int? RoomNo)
        {

            try
            {
                var parameters = new List<ParametersCollection>
                {
                    new() { ParameterName = "@GuestId",  ParameterValue = GuestId, ParameterType = DbType.Int32 , ParameterDirection = ParameterDirection.Input},
                    new() { ParameterName = "@RoomNo",  ParameterValue = RoomNo, ParameterType = DbType.Int32 , ParameterDirection = ParameterDirection.Input},




                };
                var roomList = await _dbRepository.ExecuteSpListAsync<CheckOutDto>(token, "GetRoomsByGuestIdRoomNo", parameters);
                return roomList;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error Executing Procedure CheckReservation");
                throw;
            }
        }


        public async Task<IEnumerable<OccupiedRoomsGuestDto>> GetOccupiedRoomsGuestListAsync<OccupiedRoomsGuestDto>(CancellationToken token)
        {

            try
            {
                var OccupiedRoomsGuestList = await _dbRepository.ExecuteSpListAsync<OccupiedRoomsGuestDto>(token, "GetOccupiedRoomsGuestList", null);
                return OccupiedRoomsGuestList;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error Executing Procedure GetGuestList");
                throw;
            }
        }













    }
}






