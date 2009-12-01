using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using bigs.Models;
using System.Web.Caching;

namespace bigs
{
    public static class ApplicationData
    {
        public static string DestinationEmail
        {
            get { return GetApplicationData("DestinationEmail"); }
        }

        public static void UpdateDestinationEmail(string value)
        {
            UpdateApplicationData("DestinationEmail", value);
        }


        private static string GetApplicationData(string name)
        {
            if (HttpRuntime.Cache["ApplicationData_" + name] == null)
            {
                using (DataStorage context = new DataStorage())
                {
                    string result = (from data in context.ApplicationSettings
                                     where data.Name == name
                                     select data.Value).First();
                    HttpRuntime.Cache.Insert("ApplicationData_" + name, result, null, DateTime.Now.AddHours(3), Cache.NoSlidingExpiration);
                }
            }
            return HttpRuntime.Cache["ApplicationData_" + name].ToString();
        }

        private static void UpdateApplicationData(string name, string value)
        {
            HttpRuntime.Cache.Remove("ApplicationData_" + name);
            using (DataStorage context = new DataStorage())
            {
                var data = (from appData in context.ApplicationSettings
                            where appData.Name == name
                            select appData).First();
                data.Value = value;
                context.SaveChanges();
            }
        }
    }
}
