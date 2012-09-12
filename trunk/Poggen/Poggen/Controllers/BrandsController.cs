using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Poggen.Models;
using SiteExtensions;

namespace Poggen.Controllers
{
    public class BrandsController : Controller
    {
        //
        // GET: /Brands/

        public ActionResult Index(string category)
        {
            using (var context = new SiteContainer())
            {
                var model = new CatalogueViewModel(context, category, null, (int)CategoryType.Brand);
                ViewBag.CategoryType = (int)CategoryType.Brand;
                this.SetSeoContent(model);
                ViewBag.CategoryId = model.Category.Id;
                ViewBag.CategoryName = model.Category.Name;
                return View(model);
            }
        }

    }
}
