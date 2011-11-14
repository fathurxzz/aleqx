using System;
using System.Collections.Generic;
using System.Data.Objects.DataClasses;
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
            using (var context = new ContentStorage())
            {
                var menuList = Menu.GetMenuList(id, context);
                ViewBag.MenuList = menuList;

                var headerLeftMenuItems = context.Content.Where(m => m.ContentType == 10).ToList();
                ViewBag.HeaderLeftMenuItems = headerLeftMenuItems;

                var content = context.Content
                    .Include("Parent").Include("Children").Include("Accordions")
                    .Where(c => c.Name == id)
                    .FirstOrDefault();

                if(content==null)
                content = context.Content
                    .Include("Parent").Include("Children").Include("Accordions")
                    .Where(c =>c.ContentType == 0)
                    .First();

                foreach (var accordion in content.Accordions)
                {
                    accordion.AccordionImages.Load();
                }

                ViewBag.IsRoot = content.Id == 8;

                var banners = context.Banner.ToList();
                ViewBag.Banners = banners;

                ViewBag.PageTitle = content.PageTitle;
                ViewBag.SeoDescription = content.SeoDescription;
                ViewBag.SeoKeywords = content.SeoKeywords;
                ViewBag.CurrentContentId = content.Id;


                if(content.ContentModel==1)
                {
                    var selectCountryMenu = context.Content.Include("Parent").Where(c => c.ContentModel > 0).ToList();
                    ViewBag.SelectCountryMenu = selectCountryMenu;
                }


                return View(content);
            }
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
