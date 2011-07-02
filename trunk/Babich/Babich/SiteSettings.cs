using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Babich
{
    public enum Language
    {
        En,
        Ua
    }

    public static class SiteSettings
    {
        public static string GetCurrentLanguage
        {
            get
            {
                if (HttpContext.Current.Session["lang"] == null)
                {
                    HttpContext.Current.Session["lang"] = "uk-UA";
                }
                return HttpContext.Current.Session["lang"].ToString();
            }
        }

        public static void SetCurrentLanguage(string lang)
        {
            HttpContext.Current.Session["lang"] = lang;
        }

        public static Language Language
        {
            get
            {
                return GetCurrentLanguage == "uk-UA" ? Language.Ua : Language.En;
            }
        }
    }
}