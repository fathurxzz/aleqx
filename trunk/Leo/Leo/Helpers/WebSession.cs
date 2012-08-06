using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using Leo.Models;

namespace Leo.Helpers
{
    public static class WebSession
    {
        static HttpSessionState Session
        {
            get
            {
                return HttpContext.Current.Session;
            }
        }

        public static Dictionary<int, List<ProductAttribute>> Categories
        {
            get
            {
                if (Session["attributes"] == null)
                    Session["attributes"] = new Dictionary<int, List<ProductAttribute>>();
                return (Dictionary<int, List<ProductAttribute>>)Session["attributes"];
            }
        }

        /*public static List<ProductAttribute> GetAttributes(int categoryId)
        {
            return Categories[categoryId];
        }

        public static void SetAttributes(List<ProductAttribute> attributes, int categoryId)
        {
            Categories[categoryId] = attributes;
        }*/
    }
}