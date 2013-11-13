using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace CashMachine.Helpers
{
    public static class WebSession
    {
        private static HttpSessionState Session
        {
            get
            {
                return HttpContext.Current.Session;
            }
        }

        public static string CardNumber
        {
            get
            {
                return "1111222233334444";
                return Session["cardnumber"] != null ? Session["cardnumber"].ToString() : null;
            }
            set
            {
                Session["cardnumber"] = value;
            }
        }
    }
}