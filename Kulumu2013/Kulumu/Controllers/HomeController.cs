using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kulumu.Models;
using SiteExtensions;

namespace Kulumu.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string id)
        {
            using (var context = new SiteContainer())
            {
                var model = new SiteModel(context, id);
                this.SetSeoContent(model);
                ViewBag.CurrentMenuItemName = model.Content.Name;
                ViewBag.isHomePage = model.IsHomePage;
                return View(model);
            }
        }

        public ActionResult Articles()
        {
            using (var context = new SiteContainer())
            {
                var model = new SiteModel(context, "articles",true);
                this.SetSeoContent(model);
                ViewBag.CurrentMenuItemName = model.Content.Name;
                return View(model);
            }
        }

        public ActionResult ProductDetails(int id)
        {
            using (var context = new SiteContainer())
            {

            }
        }

        public ActionResult Gallery(string id)
        {
            using (var context = new SiteContainer())
            {
                var model = new GalleryModel(context, id);
                
                //model.Categories = context.Category.Include("Products").ToList();

                //if (model.Categories.Any())
                //{
                //    model.Category = //!string.IsNullOrEmpty(id)
                //        //?
                //        model.Categories.First(c => c.Name == id);
                //                         //: model.Categories.First(c => !c.SpecialCategory);

                //    //model.Products = model.Products.Where(p => p.CategoryId == model.Category.Id).ToList();
                //    model.Title += " » " + model.Category.Title;
                //}

                



                //model.Products = model.Products.Where(p => p.CategoryId == id).ToList();
                //model.Category = context.Category.First(c => c.Id == id);
                this.SetSeoContent(model);
                return View(model);
            }
        }

        public ActionResult Tour(string id)
        {
            using (var context = new SiteContainer())
            {
                var model = new SiteModel(context, "tour");
                this.SetSeoContent(model);
                //ViewBag.SpecialCategoryName = context.Category.First(c => c.SpecialCategory).Name;
                ViewBag.CurrentMenuItemName = model.Content.Name;
                ViewBag.isHomePage = model.IsHomePage;
                ViewBag.FlashId = id;
                ViewBag.FlashName = id + ".html";
                return View(model);
            }
        }
    }
}
