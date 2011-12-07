using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HavilaTravel.Helpers;
using HavilaTravel.Models;

namespace HavilaTravel.Controllers
{
    public class SubscribeController : Controller
    {
        //
        // GET: /Subscribe/

        public void GetAddData(string id, ContentStorage context)
        {
            var menuList = Menu.GetMenuList(id, context);
            ViewBag.MenuList = menuList;
            ViewBag.Bellboy = context.Bellboy.GetRandomItem();
            ViewBag.HeaderLeftMenuItems = context.Content.Where(m => m.ContentType == 10).ToList();

            var banners = context.Banner.ToList();
            ViewBag.MainBanners = banners.Where(b => b.BannerType == 1).ToList();
            ViewBag.LeftBanner = banners.Where(b => b.BannerType == 2).GetRandomItem();
            ViewBag.RightBanner = banners.Where(b => b.BannerType == 3).GetRandomItem();
        }



        public ActionResult Index()
        {
            using (var context = new ContentStorage())
            {
                
                string id = null;

                GetAddData(id, context);
                /*
                var content = context.Content
                    .Include("Parent").Include("Children").Include("Accordions")
                    .Where(c => c.Name == id)
                    .FirstOrDefault();

                if (content == null)
                    content = context.Content
                        .Include("Parent").Include("Children").Include("Accordions")
                        .Where(c => c.ContentType == 0)
                        .First();

                foreach (var accordion in content.Accordions)
                {
                    accordion.AccordionImages.Load();
                }

                ViewBag.IsRoot = content.Id == 8;

                ViewBag.PageTitle = content.PageTitle;
                ViewBag.SeoDescription = content.SeoDescription;
                ViewBag.SeoKeywords = content.SeoKeywords;
                ViewBag.CurrentContentId = content.Id;

                if (content.ContentModel == 1)
                {
                    var selectCountryMenu = context.Content.Include("Parent").Where(c => c.ContentModel > 0 && c.ContentLevel > 1).ToList();
                    ViewBag.SelectCountryMenu = selectCountryMenu;

                }
                */
                return View();
            }
        }



        [HttpPost]
        public ActionResult Subscribe(FormCollection form)
        {
            using (var context = new ContentStorage())
            {
                var subscriber = new Customers();
                TryUpdateModel(subscriber, new[] { "Name", "Email", "SubscribeType" });
                context.AddToCustomers(subscriber);
                context.SaveChanges();

                GetAddData(null, context);
            }
            
            return View("ThankYou");
        }
    }
}
