using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace eShop.Controllers
{
    public static class SystemSettings
    {
        private static HttpSessionState Session
        {
            get { return HttpContext.Current.Session; }
        }

        public static int ParentCategoryId
        {
            get
            {
                int result = int.MinValue;
                if (Session["ParentCategoryId"] != null)
                    result = Convert.ToInt32(Session["ParentCategoryId"]);
                return result;
            }
            set { Session["ParentCategoryId"] = value; }
        }

        public static int CategoryId
        {
            get
            {
                int result = int.MinValue;
                if (Session["CategoryId"] != null)
                    result = Convert.ToInt32(Session["CategoryId"]);
                return result;
            }
            set { Session["CategoryId"] = value; }
        }
    }
}
