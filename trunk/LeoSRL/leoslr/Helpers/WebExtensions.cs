using System.Collections.Generic;
using System.Web.Routing;

namespace Leo.Helpers
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