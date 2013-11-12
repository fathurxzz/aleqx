using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using CashMachine.Models;

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

        public static Card Card
        {
            get
            {
                if (Session["card"] != null)
                    return (Card)Session["card"];
                return null;
            }
            set
            {
                Session["card"] = value;
            }
        }

        public static int? CardId
        {
            get
            {
                if (Session["cardid"] != null)
                    return (int?)Session["cardid"];
                return null;
            }
            set
            {
                Session["cardid"] = value;
            }
        }
    }
}