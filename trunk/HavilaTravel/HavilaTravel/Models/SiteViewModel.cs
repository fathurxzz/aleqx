using System.Collections.Generic;
using System.Linq;
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

        protected readonly ContentStorage Context;

        

        public SiteViewModel(string id, ContentStorage context, bool loadContent=true)
        {
            Context = context;
            var menuList = Menu.GetMenuList(id, Context);
            MenuList = menuList;
            Bellboy = Context.Bellboy.GetRandomItem();
            
            HeaderLeftMenuItems = Context.Content
                .Where(m => m.ContentType == 10)
                .Select(c=> new MenuItem
                                {
                                    Id = (int)c.Id,
                                    Title = c.Title,
                                    Name = c.Name
                                }).ToList();

            
            var banners = Context.Banner.ToList();
            MainBanners = banners.Where(b => b.BannerType == 1).ToList();
            LeftBanner = banners.Where(b => b.BannerType == 2).GetRandomItem();
            RightBanner = banners.Where(b => b.BannerType == 3).GetRandomItem();

            if (loadContent)
            {
                Content = Context.Content
                    .Include("Parent").Include("Children").Include("Accordions")
                    .Where(c => (string.IsNullOrEmpty(id) && c.ContentType == 0) || c.Name == id)
                    .First();
                
                foreach (var accordion in Content.Accordions)
                {
                    accordion.AccordionImages.Load();
                }

                IsRoot = Content.ContentType == 0;
                CurrentContentId = (int) Content.Id;
            }
        }
    }
}