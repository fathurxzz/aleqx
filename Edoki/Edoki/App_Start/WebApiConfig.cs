using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Edoki
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "EdokiApi",
                routeTemplate: "api/makeorder/{phone}",
                defaults: new { controller = "Service", action = "MakeOrder", phone = RouteParameter.Optional }
            );
            //config.Routes.MapHttpRoute(
            //    name: "ActionApi",
            //    routeTemplate: "api/{controller}/{action}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);
            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);
        }
    }
}
