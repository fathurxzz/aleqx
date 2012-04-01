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
                SiteViewModel model = new SiteViewModel(context, id,null);
                this.SetSeoContent(model);
                ViewBag.MainMenu = model.MainMenu;
                ViewBag.isHomePage = model.IsHomePage;

                ViewBag.Categories = model.Categories;
                ViewBag.Elements = model.Elements;

                return View(model);
            }
        }

        //public ActionResult Catalogue(string id)
        //{
        //    using (var context = new ModelContainer())
        //    {
        //        SiteViewModel model = new SiteViewModel(context, "catalogue", id, true, true);
        //        this.SetSeoContent(model);

        //        ViewBag.MainMenu = model.MainMenu;
        //        ViewBag.isHomePage = model.IsHomePage;

        //        ViewBag.Categories = model.Categories;
        //        ViewBag.Elements = model.Elements;

        //        return View(model);
        //    }
        //}

        public ActionResult Projects(string id)
        {
            return View();
        }

        public ActionResult Articles(string id)
        {
            return View();
        }

        public ActionResult News(string id)
        {
            return View();
        }

    }
}
