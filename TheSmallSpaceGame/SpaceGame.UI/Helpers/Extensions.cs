using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;

namespace SpaceGame.UI.Helpers
{
    public static class Extensions
    {
        public static string FormatResourcesAmount(this long value)
        {
            return FormatResourcesAmount(value.ToString(CultureInfo.InvariantCulture));
        }

        public static string FormatResourcesAmount(this string value)
        {
            for (int i = value.Length; i > 0; i--)
            {
                if ((value.Length - i + 1) % 4 == 0)
                {
                    value = value.Insert(i, ".");
                }
            }
            return value;
        }

        public static string FormatUpgradeTime(this TimeSpan value)
        {
            var cnt = 0;
            var sb = new StringBuilder();

            if (value.Days/7 != 0)
            {
                sb.AppendFormat(" {0} weeks", value.Days/7);
                cnt++;
            }
            if(cnt==3)
                return sb.ToString();

            if (value.Days != 0)
            {
                sb.AppendFormat(" {0} days", value.Days);
                cnt++;
            }
            if (cnt == 3)
                return sb.ToString();

            if (value.Hours != 0)
            {
                sb.AppendFormat(" {0} hours", value.Hours);
                cnt++;
            }
            if (cnt == 3)
                return sb.ToString();

            if (value.Minutes != 0)
            {
                sb.AppendFormat(" {0} minutes", value.Minutes);
                cnt++;
            }
            if (cnt == 3)
                return sb.ToString();

            if (value.Seconds != 0)
            {
                sb.AppendFormat(" {0} seconds", value.Seconds);
            }
            
            return sb.ToString();
        }
    }
}