using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Posh.Helpers;
using Posh.Models;

namespace Posh.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string id)
        {
            using (var context = new ModelContainer())
            {
                SiteViewModel model = new SiteViewModel(context, id, id);
                this.SetSeoContent(model);
                ViewBag.MainMenu = model.MainMenu;
                ViewBag.isHomePage = model.IsHomePage;

                ViewBag.Categories = model.Categories;
                ViewBag.Elements = model.Elements;

                return View(model);
            }
        }
        
        [OutputCache(VaryByParam = "*", NoStore = true, Duration = 1)]
        public ActionResult ShowFilter()
        {
            using (var context = new ModelContainer())
            {
                SiteViewModel model = new SiteViewModel(context, null, null);
                ViewBag.Categories = model.Categories;
                ViewBag.Elements = model.Elements;
                return PartialView("_SearchCriteriaSelector");
            }
        }


    }
}
