﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rakurs.Models
{
    public enum Language
    {
        En,
        Ru
    }
    public static class SiteSettings
    {
        public static string GetCurrentLanguage
        {
            get
            {
                if (HttpContext.Current.Session["lang"] == null)
                {
                    HttpContext.Current.Session["lang"] = "ru-RU";
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
                return GetCurrentLanguage == "ru-RU" ? Language.Ru : Language.En;
            }
        }
    }
}