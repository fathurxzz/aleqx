using System.Collections.Generic;
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

        


        public static List<Layout> Layouts
        {
            get
            {
                if (Session["layouts"] == null)
                    Session["layouts"] = new List<Layout>();
                return (List<Layout>) Session["layouts"];
            }
        }

        public static List<ProductAttribute> Attributes
        {
            get
            {
                if (Session["attributes"] == null)
                    Session["attributes"] = new List<ProductAttribute>();
                return (List<ProductAttribute>)Session["attributes"];
            }
        }

        public static List<Brand> Brands
        {
            get
            {
                if (Session["brands"] == null)
                    Session["brands"] = new List<Brand>();
                return (List<Brand>)Session["brands"];
            }
        }

        public static List<Maker> Makers
        {
            get
            {
                if (Session["makers"] == null)
                    Session["makers"] = new List<Maker>();
                return (List<Maker>)Session["makers"];
            }
        }

    }
}