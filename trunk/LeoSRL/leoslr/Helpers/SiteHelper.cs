using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Leo.Helpers
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
            return source.ToLower().Replace(" ", "-").Replace("'", "").Replace("\"","").Trim();
        }

        public static string UpdatePageWebName(string source, string title)
        {
            return Transliterate(title.ToLower().Replace(" ", "-").Replace("'", "").Replace("\"", "").Trim());
        }

        public static string GetMonthName(int month, string cultureInfoName)
        {
            return months.ContainsKey(cultureInfoName)
                ? months[cultureInfoName][month]
                : month.ToString();
        }

        private static Dictionary<string, string[]> months = new Dictionary<string, string[]>
        {
            {"ru", new [] {"","январь","февраль","март","апрель","май","июнь","июль","август","сентябрь","октябрь","ноябрь","декабрь"}},
            {"ua", new [] {"","січень","лютий","березень","квітень","травень","червень","липень","серпень","вересень","жовтень","листопад","грудень"}},
            {"en", new [] {"","january","february","march","april","may","june","july","august","september","october","november","december"}},
        };
    }
}