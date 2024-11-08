using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SMSC.Application.Commands.Reservation;
using SMSC.Application.DTO;
using SMSC.Application.Queries.Reservation;
using SMSC.Application.Queries.Room;
using SMSC.Application.Responses;
using SMSC.Core.Logger.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace SMSC.Admin.Controllers
{
    [Authorize(Policy = "RequireEmail")]
    public class ReservationController : Controller
    {
        private readonly ILog _logger;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IMediator _mediator;

        public ReservationController(ILog logger, IStringLocalizer<SharedResources> localizer, IMediator mediator)
        {
            _logger = logger;
            _localizer = localizer;
            _mediator = mediator;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> AddReservation(CancellationToken token, ReservationDto reservationDto)


        {


      

            var addReservationCommand = new AddReservationCommand
            {
                ReservationDto = reservationDto
            };

            var reservationResult = await _mediator.Send(addReservationCommand, token);
            var response = ResponseMapper.MapResponse(reservationResult, reservationResult.IsSuccess ? reservationResult.Value : null);
            return Json(response);
        }





        [HttpPost]
        public async Task<JsonResult> UpdateReservation(CancellationToken token, ReservationDto reservationDTO)


        {

            

           
            var updateReservationCommand = new UpdateReservationCommand
            {
                ReservationDto = reservationDTO
            };

            var reservationResult = await _mediator.Send(updateReservationCommand, token);
            var response = ResponseMapper.MapResponse(reservationResult, reservationResult.IsSuccess ? reservationResult.Value : null);
            return Json(response);
        }

        [HttpPost]
        public async Task<JsonResult> UpdateReservationStatus(CancellationToken token, ReservationDto reservationDTO)


        {




            var updateReservationStatusCommand = new UpdateReservationStatusCommand
            {
                ReservationDto = reservationDTO
            };

            var reservationResult = await _mediator.Send(updateReservationStatusCommand, token);
            var response = ResponseMapper.MapResponse(reservationResult, reservationResult.IsSuccess ? reservationResult.Value : null);
            return Json(response);
        }

        [HttpGet]
        public async Task<JsonResult> GetReservationById(CancellationToken cancellationToken, int GuestId)

        {
            var getReservationByIdQuery = new GetReservationByIdQuery
            {
                GuestId = GuestId
            };

            var resultGuestDto = await _mediator.Send(getReservationByIdQuery, cancellationToken);
            var response = ResponseMapper.MapResponse(resultGuestDto, resultGuestDto.IsSuccess ? resultGuestDto.Value : null);
            return Json(response);
        }



        public async Task<JsonResult> GetReservationList(CancellationToken cancellationToken)
        {

            var getReservationListQuery = new GetReservationListQuery();

            var resultReservationDto = await _mediator.Send(getReservationListQuery, cancellationToken);
            var response = ResponseMapper.MapResponse(resultReservationDto, resultReservationDto.IsSuccess ? resultReservationDto.Value : null);
            return Json(response);
        }




        [HttpGet]
        public async Task<JsonResult> GetRoomList(CancellationToken cancellationToken, RoomDTO roomDTO)
        {

            var getRoomListQuery = new GetRoomListQuery
            {
                RoomDTO = roomDTO
            };

            var resultRoomDto = await _mediator.Send(getRoomListQuery, cancellationToken);
            var response = ResponseMapper.MapResponse(resultRoomDto, resultRoomDto.IsSuccess ? resultRoomDto.Value : null);
            return Json(response);
        }


        


    }

}

