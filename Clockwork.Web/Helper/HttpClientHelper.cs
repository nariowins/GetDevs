using System.Net.Http;

namespace Clockwork.Web.Helper
{
    public class HttpClientHelper
    {
        public static HttpClient GetHttpClient()
        {
            var myHttpClient = new HttpClient();
            return myHttpClient;
        }
    }
}