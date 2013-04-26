using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace Listelli.Helpers
{
    public static class WebExtensions
    {
        public static RouteData GetCopy(this RouteData source)
        {
            RouteData result = new RouteData();
            foreach (KeyValuePair<string, object> value in source.Values)
            {
                result.Values.Add(value.Key, value.Value);
            }
            return result;
        }
    }
}