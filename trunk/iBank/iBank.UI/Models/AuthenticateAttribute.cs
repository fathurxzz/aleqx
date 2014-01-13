using System.Web;
using System.Web.Mvc;

namespace iBank.UI.Models
{
    public class AuthenticateAttribute:AuthorizeAttribute
    {
//        private readonly bool _allowAnonymus;

        public AuthenticateAttribute()
        {
            
        }

        public AuthenticateAttribute(bool allowAnonymus)
        {
  //          _allowAnonymus = allowAnonymus;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
    //        if (_allowAnonymus)
      //          return true;

            return base.AuthorizeCore(httpContext);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectResult("/login", false);
        }
    }
}