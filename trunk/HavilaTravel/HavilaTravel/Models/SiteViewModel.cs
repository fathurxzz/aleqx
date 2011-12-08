using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HavilaTravel.Helpers;

namespace HavilaTravel.Models
{
    public class SiteViewModel
    {
        public List<Menu> MenuList { get; set; }
        public Bellboy Bellboy { get; set; }
        public List<Content> HeaderLeftMenuItems { get; set; }
        public List<Banner> MainBanners { get; set; }
        public Banner LeftBanner { get; set; }
        public Banner RightBanner { get; set; }
        public Content Content { get; set; }
        public string PageTitle { get; set; }
        public string SeoDescription { get; set; }
        public string SeoKeywords { get; set; }
        public int CurrentContentId { get; set; }
        public bool IsRoot { get; set; }
        public List<Content> SearchResult { get; set; }
        public string SearchQuery { get; set; }



        public static SiteViewModel FetchData(string id, ContentStorage context)
        {
            var model = new SiteViewModel();

            var menuList = Menu.GetMenuList(id, context);

            model.MenuList = menuList;
            model.Bellboy = context.Bellboy.GetRandomItem();
            model.HeaderLeftMenuItems = context.Content.Where(m => m.ContentType == 10).ToList();

            var banners = context.Banner.ToList();
            model.MainBanners = banners.Where(b => b.BannerType == 1).ToList();
            model.LeftBanner = banners.Where(b => b.BannerType == 2).GetRandomItem();
            model.RightBanner = banners.Where(b => b.BannerType == 3).GetRandomItem();
            return model;
        }
    }
}