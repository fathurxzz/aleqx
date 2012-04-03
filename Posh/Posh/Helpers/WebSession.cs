using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using Posh.Models;

namespace Posh.Helpers
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

        public static List<Category> Categories
        {
            get
            {
                if (Session["categories"] == null)
                    Session["categories"] = new List<Category>();
                return (List<Category>)Session["categories"];
            }
        }

        public static List<Element> Elements
        {
            get
            {
                if (Session["elements"] == null)
                    Session["elements"] = new List<Element>();
                return (List<Element>)Session["elements"];
            }
        }
    }
}