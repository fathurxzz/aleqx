using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HavilaTravel.Models;

namespace HavilaTravel.Controllers
{
    public class ArticlesController : Controller
    {
        //
        // GET: /Articles/

        public ActionResult Index()
        {
            using (var context = new ContentStorage())
            {
                var menuList = Menu.GetMenuList("articles", context);
                ViewBag.MenuList = menuList;


                var headerLeftMenuItems = context.Content.Where(m => m.ContentType == 10).ToList();
                ViewBag.HeaderLeftMenuItems = headerLeftMenuItems;

                var banners = context.Banner.ToList();
                ViewBag.Banners = banners;




                var articles = context.Article.OrderBy(a => a.Date).ToList();
                return View(articles);
            }
        }

        public ActionResult Details(int id)
        {
            using (var context = new ContentStorage())
            {
                var article = context.Article.Where(a => a.Id == id).First();
                return View(article);
            }
        }

    }
}
