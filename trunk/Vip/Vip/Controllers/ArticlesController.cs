using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SiteExtensions;
using Vip.Models;

namespace Vip.Controllers
{
    [Authorize]
    public class ArticlesController : Controller
    {
        //
        // GET: /Articles/

        public ActionResult Index()
        {
            using (var context = new CatalogueContainer())
            {
                var model = new ArticlesViewModel(context);
                ViewBag.Categories = model.Categories;
                ViewBag.Projects = model.Projects;
                this.SetSeoContent(model);
                ViewBag.MainMenu = model.Menu;
                return View(model);
            }
        }

    }
}
