using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SMSC.Application.DTO;
using SMSC.Application.Queries.CheckIn;
using SMSC.Application.Queries.CheckOut;
using SMSC.Application.Queries.Room;
using SMSC.Application.Responses;
using SMSC.Core.Logger.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace SMSC.Admin.Controllers
{
    [Authorize(Policy = "RequireEmail")]
    public class CheckOutController : Controller
    {
        private readonly ILog _logger;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IMediator _mediator;

        public CheckOutController(ILog logger, IStringLocalizer<SharedResources> localizer, IMediator mediator)
        {
            _logger = logger;
            _localizer = localizer;
            _mediator = mediator;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetRoomsByGuestName(CancellationToken cancellationToken, int GuestId)
        {

            var getCheckOutListQuery = new GetRoomListByGuestIdQuery
            {
                GuestId = GuestId
            };

            var resultCheckOutDto = await _mediator.Send(getCheckOutListQuery, cancellationToken);
            var response = ResponseMapper.MapResponse(resultCheckOutDto, resultCheckOutDto.IsSuccess ? resultCheckOutDto.Value : null);
            return Json(response);
        }

        //GetOccupiedRoomsGuestList
        public async Task<JsonResult> GetOccupiedRoomsGuestList(CancellationToken cancellationToken)
        {

            
            var getOccupiedRoomsGuestListQuery = new GetOccupiedRoomsGuestListQuery();

            var resultOccupiedRoomsGuestDto = await _mediator.Send(getOccupiedRoomsGuestListQuery, cancellationToken);
            var response = ResponseMapper.MapResponse(resultOccupiedRoomsGuestDto, resultOccupiedRoomsGuestDto.IsSuccess ? resultOccupiedRoomsGuestDto.Value : null);
            return Json(response);
        }



        [HttpGet]
        public async Task<JsonResult> GetOccupiedRooms(CancellationToken cancellationToken, int? GuestId, int? RoomNo)
        {

            var getCheckOutListQuery = new GetRoomListByGuestIdQuery
            {
                GuestId = GuestId,
                RoomNo = RoomNo
            };

            var resultCheckOutDto = await _mediator.Send(getCheckOutListQuery, cancellationToken);
            var response = ResponseMapper.MapResponse(resultCheckOutDto, resultCheckOutDto.IsSuccess ? resultCheckOutDto.Value : null);
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


        [HttpGet]
        public async Task<JsonResult> GetInvoiceById(CancellationToken cancellationToken, int RoomId)
        {
            var getInvoiceByIdQuery = new GetInvoiceByRoomIdQuery
            {
                RoomId = RoomId
            };

            var resultInvoiceDto = await _mediator.Send(getInvoiceByIdQuery, cancellationToken);
            var response = ResponseMapper.MapResponse(resultInvoiceDto, resultInvoiceDto.IsSuccess ? resultInvoiceDto.Value : null);
            return Json(response);
        }


    }

}