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
    public class ReservationRepository : IReservationRepository
    {
        private readonly IRepository _dbRepository;
        private readonly LogService _logger;

        public ReservationRepository(IRepository dbRepository)
        {
            _dbRepository = dbRepository;
            _logger = new LogService();
        }

        public async Task<ReservationDto> GetReservationByIdAsync(CancellationToken cancellationToken, int GuestId)
        {
            try
            {
                var parameters = new List<ParametersCollection> {
                    new() {ParameterName  = "@GuestId", ParameterValue = GuestId, ParameterType = DbType.Int64, ParameterDirection = ParameterDirection.Input}
                };

                // *** 
                // use this function for getting single Row  _dbRepository.ExecuteSpSingleAsync()

                var reservation = await _dbRepository.ExecuteSpSingleAsync<ReservationDto>(cancellationToken, "GetReservationById", parameters);
                return reservation;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error Executing GetReservationById");
                return default;
            }
        }

        public async Task<IEnumerable<ReservationDto>> GetReservationListAsync<ReservationDto>(CancellationToken token)
        {

            try
            {
                // *** 
                // use this function for getting list  _dbRepository.ExecuteSpListAsync()
                var reservationList = await _dbRepository.ExecuteSpListAsync<ReservationDto>(token, "GetReservationList", null);
                return reservationList;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error Executing Procedure GetReservationList");
                throw;
            }
        }
        public async Task<long> AddReservationAsync(CancellationToken cancellationToken, ReservationDto reservation)
        {
            try
            {
                var parameters = new List<ParametersCollection>
                {
                    new() { ParameterName = "@FirstName",  ParameterValue = reservation.FirstName, ParameterType = DbType.String , ParameterDirection = ParameterDirection.Input},
                    new() { ParameterName = "@LastName",  ParameterValue = reservation.LastName, ParameterType = DbType.String , ParameterDirection = ParameterDirection.Input},
                    new() { ParameterName = "@Email",  ParameterValue = reservation.Email, ParameterType = DbType.String , ParameterDirection = ParameterDirection.Input},
                    new() { ParameterName = "@PhoneNo",  ParameterValue = reservation.PhoneNo, ParameterType = DbType.String , ParameterDirection = ParameterDirection.Input},
                    new() { ParameterName = "@Address",  ParameterValue = reservation.Address, ParameterType = DbType.String , ParameterDirection = ParameterDirection.Input},
                    new() { ParameterName = "ReservationDateTime",  ParameterValue = reservation.ReservationDateTime, ParameterType = DbType.DateTime2 , ParameterDirection = ParameterDirection.Input},
                    new() { ParameterName = "@AdvanceAmount",  ParameterValue = reservation.AdvanceAmount, ParameterType = DbType.Decimal , ParameterDirection = ParameterDirection.Input},
                    


                };

                // *** 
                // use this function for getting a single value as return value from proc  _dbRepository.ExecuteSpReturnValueAsync()
                var resultGuestId = await _dbRepository.ExecuteSpReturnValueAsync(cancellationToken, "AddReservation", parameters);
                return resultGuestId;


            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error Executing AddReservation");
                return default;
            }
        }




        public async Task<long> UpdateReservationAsync(CancellationToken cancellationToken, ReservationDto reservation)
        {
            try
            {
                var parameters = new List<ParametersCollection>
                {
                    new() { ParameterName = "@GuestId",  ParameterValue = reservation.GuestId, ParameterType = DbType.Int32 , ParameterDirection = ParameterDirection.Input},
                    new() { ParameterName = "ReservationDateTime",  ParameterValue = reservation.ReservationDateTime, ParameterType = DbType.DateTime2 , ParameterDirection = ParameterDirection.Input},
                    new() { ParameterName = "@AdvanceAmount",  ParameterValue = reservation.AdvanceAmount, ParameterType = DbType.Decimal , ParameterDirection = ParameterDirection.Input},



                };

                var resultReservationId = await _dbRepository.ExecuteSpReturnValueAsync(cancellationToken, "UpdateReservation", parameters);
                return resultReservationId;


            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error Executing UpdateReservation");
                return default;
            }
        }

        public async Task<long> UpdateReservationStatusAsync(CancellationToken cancellationToken, ReservationDto reservation)
        {
            try
            {
                var parameters = new List<ParametersCollection>
                {
                    new() { ParameterName = "@GuestId",  ParameterValue = reservation.GuestId, ParameterType = DbType.Int32 , ParameterDirection = ParameterDirection.Input},
                    new() { ParameterName = "@ReservationStatus",  ParameterValue = reservation.ReservationStatus, ParameterType = DbType.String , ParameterDirection = ParameterDirection.Input},



                };

                var resultReservationId = await _dbRepository.ExecuteSpReturnValueAsync(cancellationToken, "UpdateReservationStatus", parameters);
                return resultReservationId;


            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error Executing UpdateReservation");
                return default;
            }
        }


    }
}
