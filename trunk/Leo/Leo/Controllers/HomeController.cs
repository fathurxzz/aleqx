using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Leo.Models;

namespace Leo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Leo";
            return View();
        }

        [OutputCache(NoStore = true, VaryByParam = "*", Duration = 1)]
        public ActionResult About()
        {
            using (var context = new SiteContainer())
            {
                var content = context.Content.First();
                return PartialView("_About", content);
            }
        }

        public ActionResult FeedBack()
        {
            return View();
        }
    }
}
