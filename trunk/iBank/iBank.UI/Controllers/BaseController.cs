using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using iBank.SecurityServices;

namespace iBank.UI.Controllers
{
    public class BaseController : Controller
    {
        //
        // GET: /Base/

        private IAuthenticationService _serviceRepository;

        public BaseController(IAuthenticationService serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        protected override void Initialize(RequestContext requestContext)
        {
            //requestContext.HttpContext.Request.Cookies
            base.Initialize(requestContext);
        }
    }
}
