using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rakurs.Helpers;
using Rakurs.Models;

namespace Rakurs.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string id)
        {
            using (var context = new StructureContainer())
            {
                var model = new SiteViewModel(context,  id ?? "");

                this.SetSeoContent(model);
                ViewBag.MainMenu = model.MainMenu;
                ViewBag.isHomePage = model.IsHomePage;
                return View(model);
            }
        }

        public ActionResult NotFound()
        {
            using (var context = new StructureContainer())
            {
                SiteViewModel model = new SiteViewModel(context, null);
                ViewBag.MainMenu = model.MainMenu;
                return View(model);
            }
        }

        [OutputCache(NoStore = true, VaryByParam = "*", Duration = 1)]
        public ActionResult Categories()
        {
            using (var context = new StructureContainer())
            {
                var categories = context.Category.Where(c => c.CategoryId == null).ToList();
                return PartialView("Categories", categories);
            }
        }

        public ActionResult SetLanguage(string id)
        {
            SiteSettings.SetCurrentLanguage(id);
            return RedirectToAction("Index", "Home", new { id = "" });
        }
    }
}
