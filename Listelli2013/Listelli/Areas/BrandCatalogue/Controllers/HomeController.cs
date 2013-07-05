using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Listelli.Controllers;
using Listelli.Models;
using SiteExtensions;

namespace Listelli.Areas.BrandCatalogue.Controllers
{
    public class HomeController : DefaultController
    {
        //
        // GET: /BrandCatalogue/Home/

        public ActionResult Index()
        {
            using (var context = new SiteContainer())
            {
                var model = new BrandCatalogueModel(CurrentLang, context,null, null);
                this.SetSeoContent(model);
                ViewBag.CurrentMenuItem = "gallery";
                return View(model);
            }
        }


        public ActionResult BrandGroupDetails(string id)
        {
            using (var context = new SiteContainer())
            {
                var model = new BrandCatalogueModel(CurrentLang, context, id, null);
                this.SetSeoContent(model);
                ViewBag.CurrentMenuItem = "brand-details";
                return View(model);
            }
        }

        public ActionResult BrandDetails(string brandGroup, string id)
        {
            using (var context = new SiteContainer())
            {
                var model = new BrandCatalogueModel(CurrentLang, context, brandGroup, id);
                this.SetSeoContent(model);
                ViewBag.CurrentMenuItem = "brand-details";
                return View(model);
            }
        }

    }
}
