using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Exchange.Models.Helpers;

namespace Exchange.Models.MVC
{
    
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        public new SiteRoles[] Roles;

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            if (filterContext.Result is HttpUnauthorizedResult)
                filterContext.Result = new RedirectResult("~/Error/Authorization");

        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
                throw new ArgumentNullException("httpContext");

            User u = WebSession.CurrentUser;
            int len = u.UserRoleIds.Count;
            for (int i = 0; i < len; i++)
            {
                if (Roles.Contains((SiteRoles)u.UserRoleIds[i]))
                    return true;
            }

            return false;
        }
    }
}