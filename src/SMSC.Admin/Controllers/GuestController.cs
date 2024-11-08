using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SMSC.Application.Commands.Guest;
using SMSC.Application.DTO;
using SMSC.Application.Queries.Guest;
using SMSC.Application.Queries.Service;
using SMSC.Application.Responses;
using SMSC.Core.Logger.Interfaces;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace SMSC.Admin.Controllers
{
    [Authorize(Policy = "RequireEmail")]
    public class GuestController : Controller
    {
        private readonly ILog _logger;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IMediator _mediator;

        public GuestController(ILog logger, IStringLocalizer<SharedResources> localizer, IMediator mediator)
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
        public async Task<JsonResult> AddGuest(CancellationToken token, GuestDTO guestDTO)


        {


            if (Request.Form.Files.Count > 0)
            {
                IFormFile file = Request.Form.Files[0];
                if (file != null && file.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await file.CopyToAsync(memoryStream);
                        guestDTO.IdCard = memoryStream.ToArray();
                    }
                }
            }

            var addGuestCommand = new AddGuestCommand
            {
                GuestDTO = guestDTO
            };

            var guestResult = await _mediator.Send(addGuestCommand, token);
            var response = ResponseMapper.MapResponse(guestResult, guestResult.IsSuccess ? guestResult.Value : null);
            return Json(response);
        }





        [HttpPost]
        public async Task<JsonResult> UpdateGuest(CancellationToken token, GuestDTO guestDTO)


        {
            guestDTO.ServiceIds = "";
            guestDTO.ServiceIds = Request.Form["ServiceId"];


            if (Request.Form.Files.Count > 0 && !(guestDTO.AssignParent))
            {
                IFormFile file = Request.Form.Files[0];
                if (file != null && file.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await file.CopyToAsync(memoryStream);
                        guestDTO.IdCard = memoryStream.ToArray();
                    }
                }
            }

            if (guestDTO.AssignParent)
            {
                guestDTO.IdCardType = null;
                guestDTO.IdCard = null;
            }
            else
            {
                guestDTO.ParentId = 0;
            }

            var updateGuestCommand = new UpdateGuestCommand
            {
                GuestDTO = guestDTO
            };

            var guestResult = await _mediator.Send(updateGuestCommand, token);
            var response = ResponseMapper.MapResponse(guestResult, guestResult.IsSuccess ? guestResult.Value : null);
            return Json(response);
        }

        [HttpGet]
        public async Task<JsonResult> GetGuestById(CancellationToken cancellationToken, int GuestId)

        {
            var getGuestByIdQuery = new GetGuestByIdQuery
            {
                GuestId = GuestId
            };

            var resultGuestDto = await _mediator.Send(getGuestByIdQuery, cancellationToken);
            var response = ResponseMapper.MapResponse(resultGuestDto, resultGuestDto.IsSuccess ? resultGuestDto.Value : null);
            return Json(response);
        }



        public async Task<JsonResult> GetGuestList(CancellationToken cancellationToken)
        {

            var getGuestListQuery = new GetGuestListQuery();

            var resultGuestDto = await _mediator.Send(getGuestListQuery, cancellationToken);
            var response = ResponseMapper.MapResponse(resultGuestDto, resultGuestDto.IsSuccess ? resultGuestDto.Value : null);
            return Json(response);
        }




        public async Task<JsonResult> DeleteGuestById(CancellationToken cancellationToken, int guestId)
        {

            var deleteGuestByIdCommand = new DeleteGuestByIdCommand
            {
                GuestId = guestId
            };

            var resultGuestDto = await _mediator.Send(deleteGuestByIdCommand, cancellationToken);
            var response = ResponseMapper.MapResponse(resultGuestDto, resultGuestDto.IsSuccess ? resultGuestDto.Value : null);
            return Json(response);
        }



        [HttpGet]
        public async Task<JsonResult> GetServiceList(CancellationToken cancellationToken, ServiceDTO serviceDTO)
        {

            var getServiceListQuery = new GetServiceListQuery
            {
                ServiceDTO = serviceDTO
            };

            var resultServiceDto = await _mediator.Send(getServiceListQuery, cancellationToken);
            var response = ResponseMapper.MapResponse(resultServiceDto, resultServiceDto.IsSuccess ? resultServiceDto.Value : null);
            return Json(response);
        }



    }

}

