using Clockwork.API.Models;
using Clockwork.Model;
using Clockwork.Model.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Clockwork.API.Controllers
{
    [Route("api/[controller]")]
    public class CurrentTimeController : Controller
    {
        // GET api/currenttime
        [HttpGet]
        [Route("Get")]
        public IActionResult Get(string timezoneId)
        {
            try
            {
                var selectedTimezone = TimeZoneInfo.FindSystemTimeZoneById(timezoneId ?? TimeZoneInfo.Local.Id);

                var utcTime = DateTime.UtcNow;
                var serverTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, selectedTimezone);
                var ip = HttpContext.Connection.RemoteIpAddress.ToString();

                var returnVal = new CurrentTimeQuery
                {
                    UTCTime = utcTime,
                    ClientIp = ip,
                    Time = serverTime
                };

                using (var db = new ClockworkContext())
                {
                    db.CurrentTimeQueries.Add(returnVal);
                    db.SaveChanges();
                }

                var vm = new CurrentTimeViewModel
                {
                    ClientIp = returnVal.ClientIp,
                    UTCTime = returnVal.UTCTime.ToString("f"),
                    Time = returnVal.Time.ToString("f")
                };

                return Ok(vm);
            }
            catch (Exception e)
            {
                //Logger here
                return BadRequest(e);
            }
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            try
            {
                using (var db = new ClockworkContext())
                {
                    var currentTimeList = db.CurrentTimeQueries.ToList();
                    return Ok(currentTimeList);
                }
            }
            catch (Exception e)
            {
                //Logger here
                return BadRequest(e);
            }
        }

        [HttpGet]
        [Route("Delete")]
        public IActionResult Delete()
        {
            try
            {
                using (var db = new ClockworkContext())
                {
                    var currentTimeList = db.CurrentTimeQueries.ToList();

                    db.CurrentTimeQueries.RemoveRange(currentTimeList);
                    db.SaveChanges();

                    return Ok(currentTimeList.Count);
                }
            }
            catch (Exception e)
            {
                //Logger here
                return BadRequest(e);
            }
        }
    }
}
