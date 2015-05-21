﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace CashMachine.UI.Helpers
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

        public static string GetCardNumber()
        {
            if (Session["cardnumber"] != null)
                return Session["cardnumber"].ToString();
            throw new ArgumentNullException("cannot get card number from session");
        }

        public static void SetCartNumber(string value)
        {
            Session["cardnumber"] = value;
        }

        //public static string CardNumber
        //{
        //    get
        //    {
        //        return Session["cardnumber"] != null ? Session["cardnumber"].ToString() : null;
        //    }
        //    set
        //    {
        //        Session["cardnumber"] = value;
        //    }
        //}
    }
}