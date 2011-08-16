using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exchange.Models.Helpers
{
    public static partial class Helper
    {
        public static string ToOsFileFormatDate(this DateTime date)
        {
            return date.ToShortDateString().Replace(".", "");
        }
    }
}