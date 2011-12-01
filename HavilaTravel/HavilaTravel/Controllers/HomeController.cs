using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using HavilaTravel.Helpers;
using HavilaTravel.Models;
using System.Web;

namespace HavilaTravel.Controllers
{
    public class HomeController : Controller
    {

        [HttpPost]
        [OutputCache(NoStore = true, VaryByParam = "*", Duration = 1)]
        public PartialViewResult GetBellboy()
        {
            using (var context = new ContentStorage())
            {
                var bellboy = context.Bellboy.GetRandomItem();
                return PartialView("Bellboy", bellboy);
            }
        }


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
                ViewBag.LeftBanner = banners.Where(b => b.BannerType == 2).GetRandomItem();
                ViewBag.RightBanner = banners.Where(b => b.BannerType == 3).GetRandomItem();



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
                    content.Text = HttpUtility.HtmlDecode(content.Text);
                    content.Text = Regex.Replace(content.Text, @"<[^>]+>", String.Empty).Replace("\r", String.Empty).Replace("\n", String.Empty).Replace("\t", String.Empty);
                    int strLength = content.Text.Length;
                    if (strLength > 500) strLength = 500;
                    content.Text = content.Text.Substring(1, strLength);
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
