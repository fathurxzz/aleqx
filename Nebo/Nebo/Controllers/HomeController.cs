using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nebo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            for (int i = 0; i < 300; i++)
            {
                ViewBag.Message += " content";
            }





            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
