using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Listelli.Models;

namespace Listelli.Helpers
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

        private static string UserName
        {
            get
            {
                if (Session["username"] != null)
                    return Session["username"].ToString();
                return null;
            }
            set
            {
                Session["username"] = value;
            }
        }

        public static string GetUserName(string userName)
        {
            return UserName ?? (UserName = GetUserTitle(userName));
        }

        private static string GetUserTitle(string userName)
        {
            //using (var context = new SiteContainer())
            //{
                //var customer = context.Customer.First(c => c.Name == userName);
                //return customer.Title;
                return Membership.GetUser(userName).Email;
            //}
        }
    }
}