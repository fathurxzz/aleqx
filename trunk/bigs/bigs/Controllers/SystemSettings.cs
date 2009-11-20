using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.SessionState;
using System.Web.Security;


namespace bigs.Controllers
{
    public static class SystemSettings
    {
        private static HttpSessionState Session
        {
            get { return HttpContext.Current.Session; }
        }

        public static string CurrentLanguage
        {
            get
            {
                if (Session["lang"] == null)
                    Session["lang"] = "ru-RU";
                return (string)Session["lang"];
            }
            set { System.Web.HttpContext.Current.Session["lang"] = value; }
        }

        public static string CurrentLanguageShort
        {
            get
            {
                return CurrentLanguage.Substring(0, 2);
            }
        }

    }
}
