using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Leo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Leo";
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult FeedBack()
        {
            return View();
        }
    }
}
