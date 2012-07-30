using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shop.Helpers;
using Shop.Models;

namespace Shop.Controllers
{
    public class ArticlesController : Controller
    {
        //
        // GET: /Articles/

        public ActionResult Index()
        {
            using (var context = new ShopContainer())
            {
                var articles = context.Article.Where(a => a.Published).ToList();
                SiteViewModel model = new SiteViewModel(context, null, false) { Articles = articles };
                this.SetSeoContent(model);
                ViewBag.MainMenu = model.MainMenu;
                ViewBag.isHomePage = model.IsHomePage;
                return View(model);
            }
        }

        public ActionResult Details(string id)
        {
            using (var context = new ShopContainer())
            {
                var article = context.Article.FirstOrDefault(a => a.Name == id);
                if (article == null)
                {
                    throw new HttpNotFoundException();
                }
                SiteViewModel model = new SiteViewModel(context, null, false) { Article = article };
                this.SetSeoContent(model);
                ViewBag.MainMenu = model.MainMenu;
                ViewBag.isHomePage = model.IsHomePage;
                return View(model);
            }
        }

    }
}
