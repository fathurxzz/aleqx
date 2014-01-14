using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using iBank.Api.Exceptions;
using iBank.SecurityServices;
using iBank.SecurityServices.Entities;

namespace iBank.UI.Controllers
{
    public abstract class ControllerBase : Controller
    {
        //
        // GET: /Base/
        protected IAuthenticationService _AuthenticationService;

        public ControllerBase(IAuthenticationService authenticationService)
        {
            _AuthenticationService = authenticationService;
        }

        public User CurrentUser
        {
            get
            {
                try
                {
                    var cookie = Request.Cookies[SiteSettings.TokenId];
                    if (cookie == null)
                        throw new SecurityException("cannot resolve cookie", SecurityError.Unknow);
                    if (cookie["userName"] == null)
                        throw new SecurityException("cannot resolve cookie", SecurityError.Unknow);
                    return new User { Name = cookie["userName"] };
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        protected bool IsAuthenticated
        {
            get { return CurrentUser != null; }
        }
    }
}
