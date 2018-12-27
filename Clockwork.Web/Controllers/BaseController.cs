using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Clockwork.Web.Controllers
{
    public class BaseController : Controller
    {
        public static string BaseApiUrl = $"{ConfigurationManager.AppSettings["apiLink"]}";
    }
}