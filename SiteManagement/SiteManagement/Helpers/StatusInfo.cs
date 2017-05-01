using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteManagement.Helpers
{
    public class StatusInfo
    {
        public Status Status { get; set; }
        public string Message { get; set; }

        public StatusInfo(Status status, string message)
        {
            Status = status;
            Message = message;
        }
    }
}
