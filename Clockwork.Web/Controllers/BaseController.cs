using System.Configuration;
using System.Web.Mvc;

namespace Clockwork.Web.Controllers
{
    public class BaseController : Controller
    {
        public static string BaseApiUrl = $"{ConfigurationManager.AppSettings["apiLink"]}";
    }
}