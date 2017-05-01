using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteManagement.Helpers
{
    public class Events
    {
        #region Events
        public delegate void RaiseEvent(object sender, MyEventArgs e);
        public event RaiseEvent Info = delegate { };
        public event RaiseEvent Debug = delegate { };
        public event RaiseEvent Error = delegate { };
        protected void OnInfo(string message)
        {
            Info(this, new MyEventArgs(message));
        }

        protected virtual void OnDebug(string message)
        {
            Debug(this, new MyEventArgs(message));
        }

        protected virtual void OnError(string message)
        {
            Error(this, new MyEventArgs(message));
        }
        #endregion Events
    }
}
