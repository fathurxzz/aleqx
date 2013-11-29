using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpaceGame.UI.Helpers
{
    public static class Extensions
    {
        public static string FormatResourcesAmount(this long source)
        {
            return FormatResourcesAmount(source.ToString());
        }

        public static string FormatResourcesAmount(this string source)
        {
            for (int i = source.Length; i > 0; i--)
            {
                if ((source.Length - i + 1) % 4 == 0)
                {
                    source = source.Insert(i, ".");
                }
            }
            return source;
        }
    }
}