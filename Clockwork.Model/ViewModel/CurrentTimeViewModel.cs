using System;

namespace Clockwork.Model.ViewModel
{
   public class CurrentTimeViewModel
    {
        public int CurrentTimeQueryId { get; set; }
        public string Time { get; set; }
        public string ClientIp { get; set; }
        public string UTCTime { get; set; }
    }
}
