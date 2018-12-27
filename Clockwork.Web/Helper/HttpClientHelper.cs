using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

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