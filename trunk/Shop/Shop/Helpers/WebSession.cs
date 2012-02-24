using System.Collections.Generic;
using System.Web;
using System.Web.SessionState;
using Shop.Models;


namespace Shop.Helpers
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

        public static Dictionary<int, OrderItem> OrderItems
        {
            get
            {
                if (Session["orderItems"] == null)
                    Session["orderItems"] = new Dictionary<int, OrderItem>();
                return (Dictionary<int, OrderItem>)Session["orderItems"];
            }
        }
    }
}