using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HavilaTravel.Helpers;

namespace HavilaTravel.Models
{
    public class SiteViewModel
    {
        public List<Menu> MenuList { get; set; }
        public Bellboy Bellboy { get; set; }
        public List<MenuItem> HeaderLeftMenuItems { get; set; }
        public List<Banner> MainBanners { get; set; }
        public Banner LeftBanner { get; set; }
        public Banner RightBanner { get; set; }
        public Content Content { get; set; }
        public int CurrentContentId { get; set; }
        public bool IsRoot { get; set; }
        public List<Content> SearchResult { get; set; }
        public string SearchQuery { get; set; }
        public List<Article> Articles { get; set; }
        public Article Article { get; set; }
        public List<Customers> Customers { get; set; }
        public PlaceKind PlaceKind { get; set; }
        public List<ActualTours> ActualTours { get; set; }

        protected readonly ContentStorage Context;

        public SiteViewModel(string id, ContentStorage context, bool loadSibs, bool loadContent = true)
        {
            Context = context;
            Content content;
            MenuList = Menu.GetMenuList(id, Context, loadSibs, out content);

            Bellboy = Context.Bellboy.GetRandomItem();

            HeaderLeftMenuItems = MenuList.FirstOrDefault(menu => menu.ContentType == 10) ?? new List<MenuItem>();

            var banners = Context.Banner.ToList();
            MainBanners = banners.Where(b => b.BannerType == 1).ToList();
            LeftBanner = banners.Where(b => b.BannerType == 2).GetRandomItem();
            RightBanner = banners.Where(b => b.BannerType == 3).GetRandomItem();

            ActualTours = context.ActualTours.ToList();

            if (loadContent)
            {
                if (content == null)
                {
                    try
                    {
                        Content = Context.Content.Include("Parent").Include("Children").Include("Accordions").First(c => (string.IsNullOrEmpty(id) && c.ContentType == 100) || c.Name == id);
                    }
                    catch
                    {
                        throw new HttpNotFoundException();
                    }
                }
                else
                {
                    Content = content;
                }

                foreach (var accordion in Content.Accordions)
                {
                    accordion.AccordionImages.Load();
                }

                IsRoot = Content.ContentType == 100;
                CurrentContentId = (int)Content.Id;
                PlaceKind = (PlaceKind)Content.PlaceKind;
            }
        }
    }
}