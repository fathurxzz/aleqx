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

        public ActionResult ArticleDetails(string id)
        {
            using (var context = new SiteContainer())
            {
                var model = new SiteModel(context, "articles", true);
                this.SetSeoContent(model);
                model.Article = context.Article.First(a => a.Name == id);
                ViewBag.CurrentMenuItemName = model.Content.Name;
                return View(model);
            }
        }

        public ActionResult ProductDetails(int id)
        {
            using (var context = new SiteContainer())
            {
                var model = new GalleryModel(context, null, id);
                this.SetSeoContent(model);
                return View(model);
            }
        }

        public ActionResult ProductDetailsPopUp(int id)
        {
            using (var context = new SiteContainer())
            {
                var model = new GalleryModel(context, null, id);
                this.SetSeoContent(model);
                return PartialView(model);
            }
        }

        public ActionResult WorkDetails(int id)
        {
            using (var context = new SiteContainer())
            {
                var model = new WorksModel(context, id);
                this.SetSeoContent(model);
                return View(model);
            }
        }

        public ActionResult Gallery(string id)
        {
            using (var context = new SiteContainer())
            {
                var model = new GalleryModel(context, id);
                ViewBag.CurrentMenuItemName = model.Content.Name;
                ViewBag.CurrentCategoryId = model.Category.Id;
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

        public ActionResult OurWorks()
        {
            using (var context = new SiteContainer())
            {
                var model = new SiteModel(context, "ourworks", false, true);
                this.SetSeoContent(model);
                ViewBag.CurrentMenuItemName = model.Content.Name;
                return View(model);
            }
        }
    }
}
