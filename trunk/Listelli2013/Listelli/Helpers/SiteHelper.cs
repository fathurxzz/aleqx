using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Listelli.Helpers
{
    public static class SiteHelper
    {
        public static string UpdatePageWebName(string source)
        {
            return source.ToLower().Replace(" ", "-").Replace("'", "").Trim();
        }
    }
}