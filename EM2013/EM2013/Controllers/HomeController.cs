using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EM2013.Models;
using SiteExtensions;

namespace EM2013.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string category, string product)
        {
            using (var context = new SiteContext())
            {
                var model = new CatalogueViewModel(context, category ?? "", product);
                this.SetSeoContent(model);
                ViewBag.isHomePage = model.IsHomePage;
                if (model.Content != null)
                {
                    return View("Content", model);
                }
                return View(model);
            }
        }

        public ActionResult SecretLink()
        {
            using (var context = new SiteContext())
            {
                var model = new SiteViewModel(context, "secretlink", null);
                ViewBag.isHomePage = false;
                return View("Content",model);
            }
        }
    }
}
