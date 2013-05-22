using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Listelli.Models;
using SiteExtensions;

namespace Listelli.Controllers
{
    public class HomeController : DefaultController
    {
        public ActionResult Index(string id)
        {
            using (var context = new SiteContainer())
            {
                var model = new SiteModel(CurrentLang, context, id);
                ViewBag.IsHomePage = model.IsHomePage;
                this.SetSeoContent(model);

                ViewBag.CurrentMenuItem = model.Content.Name;

                return View(model);
            }
        }

        public ActionResult Gallery()
        {
            using (var context = new SiteContainer())
            {
                var model = new BrandCatalogueModel(CurrentLang, context, null);
                this.SetSeoContent(model);
                ViewBag.CurrentMenuItem = "gallery";
                return View(model);
            }
        }

        public ActionResult BrandDetails(string id)
        {
            using (var context = new SiteContainer())
            {
                var model = new BrandCatalogueModel(CurrentLang, context, id);
                this.SetSeoContent(model);
                ViewBag.CurrentMenuItem = "brand-details";
                return View(model);
            }
        }

        

        

    }
}
