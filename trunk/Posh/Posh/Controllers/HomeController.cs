using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Posh.Models;

namespace Posh.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (var context = new ModelContainer())
            {
                ViewBag.Categories = context.Category.ToList();
                ViewBag.Elements = context.Element.ToList();
            }

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
