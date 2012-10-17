using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SiteExtensions;
using Vip.Helpers;
using Vip.Models;

namespace Vip.Controllers
{
    [Authorize]
    public class CatalogueController : Controller
    {
        public ActionResult Index(string category, string filter, string brand)
        {
            using (var context = new CatalogueContainer())
            {
                var model = new CatalogueViewModel(context, category, filter, brand);
                //if (!model.Category.MainPage)
                //    ViewBag.CategoryName = model.Category.Name;
                //model.SetFilters();
                //model.ApplyFilers();
                //model.ApplyPaging();
                //ViewBag.Page = page ?? 0;
                ViewBag.MainMenu = model.Menu;
                ViewBag.Categories = model.Categories;
                ViewBag.Projects = model.Projects;
                ViewBag.CataloguePage = true;
                this.SetSeoContent(model);
                return View(model);
            }
        }
    }
}
