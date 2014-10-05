using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using Shop.DataAccess.Entities;
using Shop.WebSite.Models;

namespace Shop.WebSite.Helpers
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

        public static Order Order
        {
            get
            {
                return (Order)Session["order"];
            }
            set
            {
                Session["order"] = value;
            }
        }

        public static Dictionary<int, OrderItem> OrderItems
        {
            get
            {
                if (Session["orderItems"] == null)
                    Session["orderItems"] = new Dictionary<int, OrderItem>();
                return (Dictionary<int, OrderItem>)Session["orderItems"];
            }
        }

        public static IEnumerable<Language> Languages { get; set; }

        public static IEnumerable<ShopSetting> ShopSettings { get; set; }
    }
}