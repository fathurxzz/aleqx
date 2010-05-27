using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AvenueGreen.Helpers
{
    public class Helper
    {
        public static string GetUrl(string applicationPath,  string contentId)
        {
            string url = VirtualPathUtility.ToAbsolute(applicationPath);
            url = url + "/" + contentId;
            return url;
        }
    }
}
