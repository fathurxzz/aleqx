﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;

namespace eShop.Controllers
{
    public class ResourcesHelper
    {
        public static CultureInfo GetSelectedCulture()
        {
            CultureInfo info = null;
            if (HttpContext.Current.Session["lang"] != null)
            {
                string lang = HttpContext.Current.Session["lang"].ToString();
                info = CultureInfo.GetCultureInfo(lang);
            }
            else
                info = CultureInfo.GetCultureInfo("uk-UA");
            return info;
        }

        public static string GetResourceString(string resourceName)
        {
            return HttpContext.GetGlobalResourceObject("Resources", resourceName, GetSelectedCulture()).ToString();
        }

    }
}
