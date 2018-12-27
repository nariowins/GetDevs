using Clockwork.API.Models;
using Clockwork.Model;
using Clockwork.Web.Helper;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Mvc;
using Clockwork.Model.ViewModel;

namespace Clockwork.Web.Controllers
{
    public class TimezoneController : BaseController
    {
        private static readonly HttpClient HttpClient = HttpClientHelper.GetHttpClient();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAll([DataSourceRequest]DataSourceRequest request)
        {
            try
            {
                var response = HttpClient.GetAsync(BaseApiUrl + "api/CurrentTime/GetAll").Result;
                if (!response.IsSuccessStatusCode) return Json(null, JsonRequestBehavior.AllowGet);

                //Storing the response details recieved from web api   
                var result = response.Content.ReadAsStringAsync().Result;

                //Deserializing the response recieved from web api and storing into the Employee list  
                var timeZoneList = JsonConvert.DeserializeObject<List<CurrentTimeQuery>>(result);

                return Json(timeZoneList.ToDataSourceResult(request));
            }
            catch (Exception e)
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
                return Json(e, JsonRequestBehavior.AllowGet);
            }
        }
        
        public ActionResult Get(string timezoneId)
        {
            try
            {
                var url = $"{BaseApiUrl}api/CurrentTime/Get?timezoneId={timezoneId}";
                var response = HttpClient.GetAsync(url).Result;

                if (!response.IsSuccessStatusCode) return Json(null, JsonRequestBehavior.AllowGet);
                var result = response.Content.ReadAsStringAsync().Result;
                var currentTimeModel = JsonConvert.DeserializeObject<CurrentTimeViewModel>(result);

                return !response.IsSuccessStatusCode ? null : Json(currentTimeModel, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
                return Json(e, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetTimezoneList()
        {
            try
            {
                var response = HttpClient.GetAsync(BaseApiUrl + "api/TimeZone").Result;
                if (!response.IsSuccessStatusCode) return Json(null, JsonRequestBehavior.AllowGet);

                var result = response.Content.ReadAsStringAsync().Result;
                var timeZoneList = JsonConvert.DeserializeObject<List<TimeZoneModel>>(result);

                return !response.IsSuccessStatusCode ? null : Json(timeZoneList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
                return Json(e, JsonRequestBehavior.AllowGet);
            }
        }
    }
}