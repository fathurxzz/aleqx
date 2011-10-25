using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HavilaTravel.Models;

namespace HavilaTravel.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string id)
        {
            //ViewBag.Message = "Welcome to ASP.NET MVC!";

            using (var context = new ContentStorage())
            {
                var mainMenuItems = context.Content.Where(m => m.ContentType == 1 &&m.ContentLevel==1).Select(m => m).ToList();
                var headerLeftMenuItems = context.Content.Where(m => m.ContentType == 10).ToList();
                ViewBag.HeaderLeftMenuItems = headerLeftMenuItems;

                ViewBag.MainMenuItems = mainMenuItems;

                ViewBag.CurrentContentId = id;

                var content = context.Content.Include("Children").Where(c => c.Name == id || c.ContentType == 0).First();

                

                ViewBag.SeoDescription = content.SeoDescription;
                ViewBag.SeoKeywords = content.SeoKeywords;
                ViewBag.SubMenuItems = content.Children.ToList();

                ViewBag.Title = content.Title;

                return View(content);
            }
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
