using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.OData.Builder;
using AccuV.UI.Dto;
using NexiusFusion.DataAccess.Entities;

namespace AccuV.UI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.Filters.Add(new AuthorizeAttribute());

            config.MapHttpAttributeRoutes();

            RegisterDfrOdata(config);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }

        private static void RegisterDfrOdata(HttpConfiguration config)
        {
            var builder = new ODataConventionModelBuilder();
            builder.EntitySet<Report>("Reports");
            builder.EntitySet<ReportEmployee>("ReportEmployee");
            builder.EntitySet<ReportHazard>("ReportHazard");
            builder.EntitySet<Project>("Projects");
            builder.EntitySet<Site>("Sites");
            builder.EntitySet<StructureType>("StructureTypes");
            builder.EntitySet<Weather>("Weather");
            builder.EntitySet<WorkType>("WorkTypes");
            builder.EntitySet<Employee>("Employees");
            builder.EntitySet<Hazard>("Hazards");
            builder.EntitySet<EmployeeType>("EmployeeTypes");
            builder.EntitySet<PerDiem>("PerDiems");

            config.Routes.MapODataRoute("odata", "odata", builder.GetEdmModel());
        }

        private static void RegisterAccuVOdata(HttpConfiguration config)
        {
            var builder = new ODataConventionModelBuilder();
            builder.EntitySet<ActivityDto>("Activities");

            config.Routes.MapODataRoute("accuVData", "api/data", builder.GetEdmModel());
        }

    }
}
