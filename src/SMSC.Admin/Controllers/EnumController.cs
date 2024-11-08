using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMSC.Admin.Controllers
{
   
    public class EnumController : Controller
    {

        public IActionResult Index()
        {
            return View();  
        }
        public IActionResult GetStatuses()
        {
            var statuses = Enum.GetValues(typeof(RoomStatus))
                                .Cast<RoomStatus>()
                                .Select(s => new
                                {
                                    StatusId = (int)s,
                                    RoomStatus = s.ToString()
                                })
                                .ToList();

            return Ok(statuses); 
        }
    }

    public enum RoomStatus
    {
        Available = 1 ,
        Occupied = 2,
        Reserved = 3 ,
        Maintenance = 4 ,
        Dirty = 5 ,
        Clean = 6,
    }
}
