using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using Listelli.Models;

namespace Listelli
{
    public static class WebSession
    {
        static HttpSessionState Session
        {
            get
            {
                return HttpContext.Current.Session;
            }
        }

        public static Language CurrentLanguage
        {
            get
            {
                return Session["currentlanguage"] != null ? (Language)Session["currentlanguage"] : null;
            }

            set
            {
                Session["currentlanguage"] = value;
            }
        }

    }
}