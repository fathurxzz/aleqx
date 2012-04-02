using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Posh.Helpers;
using Posh.Models;

namespace Posh.Controllers
{
    public class NewsController : Controller
    {
        public ActionResult Index()
        {
            using (var context = new ModelContainer())
            {
                var model = new NewsModel(context, "news");
                this.SetSeoContent(model);

                ViewBag.MainMenu = model.MainMenu;
                ViewBag.isHomePage = model.IsHomePage;

                return View(model);
            }
        }

    }
}
