using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Exchange.Models.Helpers
{
    public static partial class Helper
    {
        private static CultureInfo GetSelectedCulture()
        {
            CultureInfo info = null;
            if (HttpContext.Current.Session["lang"] != null)
            {
                string lang = HttpContext.Current.Session["lang"].ToString();
                info = CultureInfo.GetCultureInfo(lang);
            }
            else
                info = CultureInfo.GetCultureInfo("ru-RU");
            return info;
        }

        public static string GetResourceString(string resourceName)
        {
            object obj = HttpContext.GetGlobalResourceObject("Resources", resourceName, GetSelectedCulture());
            return obj != null ? obj.ToString() : resourceName;
            //return HttpContext.GetGlobalResourceObject("Resources", resourceName, GetSelectedCulture()).ToString();
        }
    }
}