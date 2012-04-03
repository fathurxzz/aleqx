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


                WebSession.Categories.Clear();
                WebSession.Elements.Clear();

                List<Category> checkedCategories = (from category in model.Categories where form["hcat_" + category.Id] == "1" select category).ToList();
                List<Element> checkedElements = (from element in model.Elements where form["el_" + element.Id] == "true,false" select element).ToList();


                //foreach (var category in model.Categories.Where(category => form["hcat_" + category.Id] == "1"))
                //{
                //    WebSession.Categories.Add(category);
                //}

                //foreach (var element in model.Elements.Where(element => form["el_" + element.Id] == "true,false"))
                //{
                //    WebSession.Elements.Add(element);
                //}

                /*Session["categories"] = checkedCategories;
                Session["elements"] = checkedElements;*/

                WebSession.Categories.AddRange(checkedCategories);
                WebSession.Elements.AddRange(checkedElements);

                //model.ApplyFilter(checkedCategories, checkedElements);


                //ViewBag.Categories = checkedCategories;
                //ViewBag.Elements = checkedElements;

                return View(model);
            }
        }
    }
}
