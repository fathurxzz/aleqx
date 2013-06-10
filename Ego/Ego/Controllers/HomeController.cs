using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ego.Models;
using SiteExtensions;

namespace Ego.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string id)
        {
            using (var context = new SiteContainer())
            {
                var model = new SiteModel(context, id);
                ViewBag.IsHomePage = model.IsHomePage;
                this.SetSeoContent(model);
                return View(model);
            }
        }
    }
}
