using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SMSC.Application.DTO;
using SMSC.Application.Queries.Room;
using SMSC.Application.Responses;
using SMSC.Core.Logger.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace SMSC.Admin.Controllers
{
    [Authorize(Policy = "RequireEmail")]
    public class CheckInventoryController : Controller
    {
        private readonly ILog _logger;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IMediator _mediator;

        public CheckInventoryController(ILog logger, IStringLocalizer<SharedResources> localizer, IMediator mediator)
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

