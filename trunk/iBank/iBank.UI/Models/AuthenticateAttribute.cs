using System;
using System.Web;
using System.Web.Mvc;
using iBank.SecurityServices;

namespace iBank.UI.Models
{
    public class AuthenticateAttribute : AuthorizeAttribute
    {
        private readonly bool _allowAnonymus;
        public bool OnlyAuthenticationToken { get; set; }

        

        public AuthenticateAttribute(bool allowAnonymus)
        {
            _allowAnonymus = allowAnonymus;
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
                if (_allowAnonymus)
                    return true;

                return false;
            }

            if (cookie.Expires > DateTime.Now)
            {
                return false;
            }


            // если мы зашли прямо на страницу авторизации
            if (_allowAnonymus)
                return true;

            if (cookie["sessionToken"] == null)
            {
                if (OnlyAuthenticationToken)
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