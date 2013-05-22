using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Listelli.Controllers;
using Listelli.Models;
using SiteExtensions;

namespace Listelli.Areas.FactoryCatalogue.Controllers
{
    //[Authorize]
    public class CategoryController : DefaultController
    {
        //
        // GET: /FactoryCatalogue/Category/

        public ActionResult Index()
        {
            using (var context = new SiteContainer())
            {
      
                var model = new FactoryCatalogueModel(CurrentLang, context, null,null);

                if (!User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("LogOn", "Customer");
                }


                this.SetSeoContent(model);
                ViewBag.CurrentMenuItem = model.Content.Name;
                return View(model);
            }
        }

        [Authorize]
        public ActionResult Details(string id)
        {
            using (var context = new SiteContainer())
            {
                var model = new FactoryCatalogueModel(CurrentLang, context, id,null);
                this.SetSeoContent(model);
                ViewBag.CurrentMenuItem = "factory-details";
                if (model.Category.CategoryBrands.Any())
                {
                    var minSortOrder = model.Category.CategoryBrands.Min(c => (int?)c.SortOrder) ?? 0;
                    return RedirectToAction("Details", "Brand", new {area = "FactoryCatalogue", categoryId=model.Category.Name,id=model.Category.CategoryBrands.First(c=>c.SortOrder==minSortOrder).Name});
                }
                return View(model);
            }
        }
    }
}
