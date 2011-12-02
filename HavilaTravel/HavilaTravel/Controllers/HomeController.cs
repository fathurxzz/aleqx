﻿using System;
using System.Collections.Generic;
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

        public void GetAddData(string id,  ContentStorage context)
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

        public ActionResult Index(string id)
        {
            using (var context = new ContentStorage())
            {

                GetAddData(id, context);

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

        public ActionResult Search(string query)
        {
            ViewBag.SearchQuery = query;


            if (string.IsNullOrEmpty(query))
                return View(new List<Content>());

            query = query.ToLower();

            using (var context = new ContentStorage())
            {

                var result = new List<Content>();

                GetAddData("countries", context);

                var contentItems = context.Content.Include("Parent").Include("Accordions").Where(c => c.PlaceKind > 1).ToList();
                foreach (var content in contentItems)
                {
                    content.Text = HttpUtility.HtmlDecode(content.Text);
                    content.Text = Regex.Replace(content.Text, @"<[^>]+>", String.Empty).Replace("\r", String.Empty).Replace("\n", String.Empty).Replace("\t", String.Empty);
                    int strLength = content.Text.Length;
                    if (strLength > 500) strLength = 500;
                    content.Text = content.Text.Substring(0, strLength);
                    foreach (var accordion in content.Accordions)
                    {
                        accordion.AccordionImages.Load();
                    }

                    if (content.Title.ToLower().Contains(query) || content.MenuTitle.ToLower().Contains(query) || content.Text.ToLower().Contains(query))
                        result.Add(content);

                }
                return View(result);
            }
        }
    }
}
