using System;
using System.Web;
using System.Web.Mvc;

namespace iBank.UI.Models
{
    public class AuthenticateAttribute:AuthorizeAttribute
    {
        //protected override bool AuthorizeCore(HttpContextBase httpContext)
        //{
        //    if (httpContext.Request.Cookies[SiteSettings.TokenId] != null)
        //    {
        //        var cookie = httpContext.Request.Cookies[SiteSettings.TokenId];
        //        if (cookie.Expires > DateTime.Now)
        //        {

        //            HttpContext.Current.Response.SetCookie(cookie);
        //            return false;
        //        }
        //    }
        //    else
        //    {
                
        //    }
        //    return true;
        //}

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectResult("/login", false);
        }






        private void SetValue()
        {

        }

    }
}