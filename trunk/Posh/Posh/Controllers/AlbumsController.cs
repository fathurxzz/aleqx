using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Posh.Helpers;
using Posh.Models;

namespace Posh.Controllers
{
    public class AlbumsController : Controller
    {
        //
        // GET: /Albums/

        public ActionResult Index(string id)
        {
            using (var context = new ModelContainer())
            {
                var model = new CatalogueModel(context, "catalogue",id);
                this.SetSeoContent(model);

                ViewBag.MainMenu = model.MainMenu;
                ViewBag.isHomePage = model.IsHomePage;

                ViewBag.Categories = model.Categories;
                ViewBag.Elements = model.Elements;

                if (model.Album != null)
                    return View("Details",model);

                return View(model);
            }
        }


    }
}
