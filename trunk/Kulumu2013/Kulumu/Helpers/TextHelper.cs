using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kulumu.Helpers
{
    public static class TextHelper
    {
        public static string ToCustomDate(this DateTime str)
        {
            string[] _monthes = { "", "января", "февраля", "марта", "апреля", "мая", "июня", "июля", "августа", "сентября", "октября", "ноября", "декабря" };
            return string.Format("{0} {1} {2}", str.Day, _monthes[str.Month], str.Year);
        }
    }
}