using Clockwork.API.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Clockwork.API.Controllers
{
    [Route("api/[controller]")]
    public class TimeZoneController : Controller
    {
        // GET api/currenttime
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var timeZoneList = TimeZoneInfo.GetSystemTimeZones()
                    .Select(item => new TimeZoneModel()
                    {
                        Id = item.Id,
                        DisplayName = $"{item.Id} ({item.DisplayName})"
                    })
                    .ToList();

                return Ok(timeZoneList);
            }
            catch (Exception e)
            {
               //Logger here
                return BadRequest(e);
            }
        }
    }
}
