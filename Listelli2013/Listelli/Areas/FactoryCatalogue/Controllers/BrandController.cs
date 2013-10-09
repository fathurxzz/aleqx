using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Listelli.Controllers;
using Listelli.Models;

namespace Listelli.Areas.FactoryCatalogue.Controllers
{
    [Authorize]
    public class BrandController : DefaultController
    {

        //
        // GET: /FactoryCatalogue/Brand/Details/5

        public ActionResult Details(string categoryId, string id)
        {
            using (var context = new SiteContainer())
            {
                var model = new FactoryCatalogueModel(CurrentLang, context, categoryId, id);
                ViewBag.CurrentBramdName = model.Brand.Name;
                ViewBag.CurrentMenuItem = "factory-details";
                return View(model);
            }
        }

       
    }
}
