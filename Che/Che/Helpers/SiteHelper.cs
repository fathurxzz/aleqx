using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dev.Mvc.Helpers
{
    public static class SiteHelper
    {
        public static string GetPathByContentType(int contentType)
        {

            switch (contentType)
            {
                case 1:
                    return "sites";
                case 2:
                    return "id";
                case 3:
                    return "ad";
            }
            throw new ArgumentOutOfRangeException();
        }

    }
}