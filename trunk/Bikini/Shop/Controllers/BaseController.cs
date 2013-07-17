using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop.Controllers
{
    public class BaseController : Controller
    {
        public static string HostName = string.Empty;

        protected static string ErrorPage = "~/Error";

        protected static string NotFoundPage = "~/NotFoundPage";

        protected static string LoginPage = "~/Login";

        public RedirectResult RedirectToErrorPage
        {
            get
            {
                return Redirect(ErrorPage);
            }
        }

        public RedirectResult RedirectToNotFoundPage
        {
            get
            {
                return Redirect(NotFoundPage);
            }
        }

        public RedirectResult RedirectToLoginPage
        {
            get
            {
                return Redirect(LoginPage);
            }
        }

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            if (requestContext.HttpContext.Request.Url != null)
            {
                HostName = requestContext.HttpContext.Request.Url.Authority;
            }
            base.Initialize(requestContext);
        }

        protected override void OnException(ExceptionContext filterContext)
        {

            //base.OnException(filterContext);

            if (filterContext.ExceptionHandled)
                return;



            if (filterContext.Exception is ObjectNotFoundException)
                filterContext.Result = Redirect(NotFoundPage);
            else
                filterContext.Result = Redirect(ErrorPage);

            filterContext.Controller.TempData["errorMessage"] = filterContext.Exception.Message;

            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.Clear();

        }
    }
}
