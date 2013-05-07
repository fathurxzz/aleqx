using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shitova.Models;
using SiteExtensions;

namespace Shitova.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string id)
        {
            using (var context = new SiteContainer())
            {

                var model = new SiteModel(context, id, id == "look");
                this.SetSeoContent(model);
                ViewBag.isHomePage = model.IsHomePage;
                ViewBag.PageTitle = model.PageTitle;
                return View(model);
            }
        }


    }
}
