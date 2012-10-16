using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SiteExtensions;
using Vip.Models;

namespace Vip.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string id)
        {
            using (var context = new CatalogueContainer())
            {
                var model = new SiteViewModel(context, id);
                this.SetSeoContent(model);
                //if (model.Content != null && model.Layouts != null)
                //    ViewBag.Layouts = model.Layouts;
                ViewBag.Categories = model.Categories;
                ViewBag.Projects = model.Projects;

                ViewBag.isHomePage = model.IsHomePage;
                ViewBag.MainMenu = model.Menu;
                if (model.IsHomePage)
                    ViewBag.MainPage = true;
                return View(model);
            }
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult ErrorPage()
        {
            return View();
        }
    }
}
