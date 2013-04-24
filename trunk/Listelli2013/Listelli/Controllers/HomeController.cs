using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Listelli.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Gallery()
        {
            ViewBag.CurrentMenuItem = "gallery";
            return View();
        }

        public ActionResult BrandDetails()
        {
            ViewBag.CurrentMenuItem = "brand-details";
            return View();
        }
    }
}
