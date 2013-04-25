using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Listelli.Models;

namespace Listelli.Controllers
{
    public class HomeController : DefaultController
    {
        public ActionResult Index()
        {
            //using (var context = new SiteContainer())
            //{
            //    var lang =  context.Language.First();
                
            //}

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
