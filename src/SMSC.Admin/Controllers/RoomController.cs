using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SMSC.Application.Commands.Room;
using SMSC.Application.DTO;
using SMSC.Application.Queries.Room;
using SMSC.Application.Responses;
using SMSC.Core.Logger.Interfaces;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace SMSC.Admin.Controllers
{
    [Authorize(Policy = "RequireEmail")]
    public class RoomController : Controller
    {
        private readonly ILog _logger;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IMediator _mediator;

        public RoomController(ILog logger, IStringLocalizer<SharedResources> localizer, IMediator mediator)
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
        public async Task<JsonResult> AddRoom(CancellationToken token, RoomDTO roomDto)
        {

            if (Request.Form.Files.Count > 0)
            {
                for (int i = 0; i < Request.Form.Files.Count; i++)
                {
                    IFormFile file = Request.Form.Files[i];

                    if (file != null && file.Length > 0)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await file.CopyToAsync(memoryStream);

                            roomDto.RoomImages.Add(memoryStream.ToArray());
                        }
                    }
                }
            }

           


            var addRoomCommand = new AddRoomCommand
            {
                RoomDTO = roomDto
            };

            var roomResult = await _mediator.Send(addRoomCommand, token);
            var response = ResponseMapper.MapResponse(roomResult, roomResult.IsSuccess ? roomResult.Value : null);
            return Json(response);
        }

        [HttpGet]
        public async Task<JsonResult> GetRoomById(CancellationToken cancellationToken, int RoomId)
        {
            var getRoomByIdQuery = new GetRoomByIdQuery
            {
                RoomId = RoomId
            };

            var resultRoomDto = await _mediator.Send(getRoomByIdQuery, cancellationToken);
            var response = ResponseMapper.MapResponse(resultRoomDto, resultRoomDto.IsSuccess ? resultRoomDto.Value : null);
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


        public async Task<JsonResult> UpdateRoom(CancellationToken cancellationToken, RoomDTO roomDto)
        {
            if (Request.Form.Files.Count > 0)
            {
                for (int i = 0; i < Request.Form.Files.Count; i++)
                {
                    IFormFile file = Request.Form.Files[i];

                    if (file != null && file.Length > 0)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await file.CopyToAsync(memoryStream);

                            roomDto.RoomImages.Add(memoryStream.ToArray());
                        }
                    }
                }
            }


            var updateRoomCommand = new UpdateRoomCommand
            {
                RoomDTO = roomDto
            };

            var roomResult = await _mediator.Send(updateRoomCommand, cancellationToken);
            return Json(roomResult);
        }

        public async Task<JsonResult> DeleteRoomById(CancellationToken cancellationToken, int roomId)
        {

            var deleteRoomByIdCommand = new DeleteRoomByIdCommand
            {
                RoomId = roomId
            };

            var resultRoomDto = await _mediator.Send(deleteRoomByIdCommand, cancellationToken);
            var response = ResponseMapper.MapResponse(resultRoomDto, resultRoomDto.IsSuccess ? resultRoomDto.Value : null);
            return Json(response);
        }
    }
}


