using System;
using System.Web;
using System.Web.Mvc;
using iBank.SecurityServices;

namespace iBank.UI.Models
{
    public class AuthenticateAttribute:AuthorizeAttribute
    {
        public bool AllowAnonymus { get; set; }
        public bool AuthenticateAction { get; set; }

        public AuthenticateAttribute()
        {
        }

        public AuthenticateAttribute(bool allowAnonymus)
        {
            AllowAnonymus = allowAnonymus;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            string ip = httpContext.Request.UserHostAddress;
            var cookie = httpContext.Request.Cookies[SiteSettings.TokenId];
            if (cookie == null)
            {
                cookie = new HttpCookie(SiteSettings.TokenId);
                var authToken = DependencyResolver.Current.GetService<IAuthenticationService>().GetAuthentificationToken(ip);
                cookie["authToken"] = authToken.AuthentificationTokenValue;
                cookie.Expires = authToken.ExpireTime;
                cookie.Path = "/";
                //System.Web.HttpContext.Current.Response.SetCookie(cookie);
                httpContext.Response.Cookies.Add(cookie);
                
                // если мы зашли прямо на страницу авторизации
                if(AllowAnonymus)
                    return true;

                return false;
            }

            if (cookie["sessionToken"] == null)
            {
                if (AuthenticateAction)
                    return true;

                return false;
            }

            //string sessionToken = cookie["sessionToken"];

            return true;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectResult("/login", false);
        }
    }
}