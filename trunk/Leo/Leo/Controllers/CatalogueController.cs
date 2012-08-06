using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Leo.Helpers;
using Leo.Models;
using SiteExtensions;

namespace Leo.Controllers
{
    public class CatalogueController : Controller
    {
        //
        // GET: /Catalogue/

        public ActionResult Index(string id)
        {
            using (var context = new SiteContainer())
            {
                var model = new CatalogueViewModel(context, id);
                this.SetSeoContent(model);

                if (WebSession.Categories.ContainsKey(model.Category.Id))
                    model.ApplyFilterForProducts(WebSession.Categories[model.Category.Id]);

                return View(model);
            }
        }

        [HttpPost]
        public ActionResult Index(string id, FormCollection form)
        {
            using (var context = new SiteContainer())
            {
                var model = new CatalogueViewModel(context, id);
                this.SetSeoContent(model);

                List<ProductAttribute> checkedAttributes = (from attribute in model.Attributes where form["attr_" + attribute.Id] == "true,false" select attribute).ToList();
                WebSession.Categories[model.Category.Id] = checkedAttributes;

                if (WebSession.Categories.ContainsKey(model.Category.Id))
                    model.ApplyFilterForProducts(WebSession.Categories[model.Category.Id]);

                return View(model);
            }
        }

      


        [OutputCache(VaryByParam = "*", NoStore = true, Duration = 1)]
        public ActionResult ShowFilter(string id)
        {
            using (var context = new SiteContainer())
            {
                var model = new CatalogueViewModel(context, id);
                ViewBag.CategoryName = model.Category.Name;
                ViewBag.CategoryId = model.Category.Id;
                return PartialView("_SearchCriteriaSelector",model.Attributes);
            }
            
        }

    }
}
