using System;
using System.Collections.Generic;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HavilaTravel.Helpers;
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

                ViewBag.Bellboy = context.Bellboy.GetRandomItem();

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
                ViewBag.MainBanners = banners.Where(b => b.BannerType == 1).ToList();
                ViewBag.LeftBanner = banners.Where(b => b.BannerType == 2).ToList().GetRandomItem();
                ViewBag.RightBanner = banners.Where(b => b.BannerType == 3).ToList().GetRandomItem();



                ViewBag.PageTitle = content.PageTitle;
                ViewBag.SeoDescription = content.SeoDescription;
                ViewBag.SeoKeywords = content.SeoKeywords;
                ViewBag.CurrentContentId = content.Id;

                if(content.ContentModel==1)
                {
                    var selectCountryMenu = context.Content.Include("Parent").Where(c => c.ContentModel > 0&&c.ContentLevel>1).ToList();
                    ViewBag.SelectCountryMenu = selectCountryMenu;
                }

                return View(content);
            }
        }

        public ActionResult Search(string mSearch)
        {
            ViewBag.SearchQuery = mSearch;

            using (var context = new ContentStorage())
            {
                var contentItems = context.Content.Include("Accordions").Where(c => c.PlaceKind > 1 && c.Title.Contains(mSearch)).ToList();
                foreach (var content in contentItems)
                {
                    foreach (var accordion in content.Accordions)
                    {
                        accordion.AccordionImages.Load();
                    }
                }
                return View(contentItems);
            }
        }
    }
}
