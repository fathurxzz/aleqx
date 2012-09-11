using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Poggen.Models;
using SiteExtensions;

namespace Poggen.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string id)
        {
            using (var context = new SiteContainer())
            {
                var model = new SiteViewModel(context, id??"");
                this.SetSeoContent(model);
                ViewBag.isHomePage = model.IsHomePage;
                if (model.Content != null)
                    ViewBag.ContentName = model.Content.Name;
                return View();
            }
        }
    }
}
