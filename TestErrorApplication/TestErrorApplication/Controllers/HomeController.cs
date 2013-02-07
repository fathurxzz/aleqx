using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SiteExtensions;

namespace TestErrorApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            throw new HttpNotFoundException("aaa");

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
