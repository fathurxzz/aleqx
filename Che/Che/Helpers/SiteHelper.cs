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
                    return "Sites";
                case 2:
                    return "Styles";
                case 3:
                    return "Adv";
            }
            throw new ArgumentOutOfRangeException();
        }

    }
}