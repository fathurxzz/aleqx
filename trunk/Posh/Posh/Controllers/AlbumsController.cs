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


                model.Albums = ApplyFilter(model);


                if (model.Album != null)
                    return View("Details",model);

                return View(model);
            }
        }

        public List<Album> ApplyFilter(CatalogueModel model)
        {
           


            if (WebSession.Categories.Count > 0)
            {
                foreach (var album in model.Albums)
                {
                    foreach (var product in album.Products)
                    {
                        if (product.Categories.Intersect(WebSession.Categories).Count() == 0)
                        {
                            product.Hidden = true;
                        }
                    }
                }
            }

            if (WebSession.Elements.Count > 0)
            {
                foreach (var album in model.Albums)
                {
                    foreach (var product in album.Products)
                    {
                        if (product.Elements.Intersect(WebSession.Elements).Count() == 0)
                        {
                            product.Hidden = true;
                        }
                    }
                }
            }


            var resultAlbums = new List<Album>();

            foreach (var album in model.Albums)
            {
                if (album.Products.Where(p => !p.Hidden).Count() > 0)
                    resultAlbums.Add(album);
            }

            return resultAlbums;

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

                WebSession.Categories.AddRange(checkedCategories);
                WebSession.Elements.AddRange(checkedElements);


                model.Albums = ApplyFilter(model);

                

               


                //return RedirectToAction("Index");
                return View(model);
            }
        }
    }
}
