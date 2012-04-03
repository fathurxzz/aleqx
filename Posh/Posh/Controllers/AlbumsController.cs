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

        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            using (var context = new ModelContainer())
            {


                var model = new CatalogueModel(context, "catalogue", null);
                this.SetSeoContent(model);

                ViewBag.MainMenu = model.MainMenu;
                ViewBag.isHomePage = model.IsHomePage;

                ViewBag.Categories = model.Categories;


                List<int> checkedCategories = new List<int>();
                List<int> checkedElements = new List<int>();

                foreach (var category in model.Categories)
                {
                    if(form["hcat_"+category.Id]=="true,false")
                    {
                        checkedCategories.Add(category.Id);
                    }
                }


                ViewBag.Elements = model.Elements;

                return View(model);
            }
        }
    }
}
