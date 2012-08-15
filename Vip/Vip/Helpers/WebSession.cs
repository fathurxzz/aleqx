using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using Vip.Models;

namespace Vip.Helpers
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


        public static List<Layout> Layouts
        {
            get
            {
                if (Session["layouts"] == null)
                    Session["layouts"] = new List<Layout>();
                return (List<Layout>) Session["layouts"];
            }
        }

    }
}