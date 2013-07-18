using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Index()
        {
            //Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return View();
        }

        public ActionResult NotFoundPage()
        {
            //Response.StatusCode = (int)HttpStatusCode.NotFound;
            return View();
        }

    }
}
