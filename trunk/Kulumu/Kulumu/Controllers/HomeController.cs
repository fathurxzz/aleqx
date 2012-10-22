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
                SiteModel model = new SiteModel(context,id);
                this.SetSeoContent(model);

                ViewBag.isHomePage = model.IsHomePage;
                return View(model);
            }
        }

        public ActionResult Articles()
        {
            using (var context = new SiteContainer())
            {
                SiteModel model = new SiteModel(context, "articles", true);
                return View(model);
            }
        }

        public ActionResult Gallery(int id)
        {
            using (var context = new SiteContainer())
            {
                SiteModel model = new SiteModel(context, "gallery");
                model.Products = model.Products.Where(p => p.CategoryId == id).ToList();
                model.Category = context.Category.First(c => c.Id == id);
                return View(model);
            }
        }
    }
}
