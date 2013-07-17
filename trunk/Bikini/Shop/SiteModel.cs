using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shop.Models;
using SiteExtensions;

namespace Shop
{
    public class SiteModel:ISiteModel
    {
        public string Title { get; set; }
        public string SeoDescription { get; set; }
        public string SeoKeywords { get; set; }
        public Menu Menu { get; set; }
        public bool IsHomePage { get; set; }
        public Content Content { get; set; }

        public SiteModel(ShopContainer context, string contentId)
        {
            Title = "Bikini";
            Content = context.Content.FirstOrDefault(c => c.Name == contentId) ?? context.Content.First(c => c.MainPage);
            IsHomePage = Content.MainPage;
            SeoDescription = Content.SeoDescription;
            SeoKeywords = Content.SeoKeywords;
        }
    }
}