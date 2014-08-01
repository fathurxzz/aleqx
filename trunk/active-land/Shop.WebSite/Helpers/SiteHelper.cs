﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop.WebSite.Helpers
{
    public class SiteHelper
    {
        public static string Transliterate(string source)
        {
            string[] russian = "aбвгдеёжзийклмнопрстуфхцчшщъыьэюя"
                .ToCharArray().Select(c => new String(c, 1)).ToArray();
            string[] english = "a/b/v/g/d/e/e/zh/z/i/y/k/l/m/n/o/p/r/s/t/u/f/h/ts/ch/sh/shch//y/'/e/iu/ia"
                .Split('/');
            string result = source;
            for (int i = 0; i < russian.Length; i++)
            {
                result = result.Replace(russian[i], english[i]);
            }
            return result;
        }


        public static string UpdatePageWebName(string source)
        {
            return source.ToLower().Replace(" ", "-").Replace("'", "").Trim();
        }

        public static string UpdatePageWebName(string source, string title)
        {
            return Transliterate(title.ToLower().Replace(" ", "-").Replace("'", "").Trim());
        }


    }
}