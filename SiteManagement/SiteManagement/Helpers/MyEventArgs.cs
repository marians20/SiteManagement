using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteManagement.Helpers
{
    public class MyEventArgs : EventArgs
    {
        public string Message { get; set; }

        public MyEventArgs(string message = "")
        {
            Message = message;
        }
    }
}
