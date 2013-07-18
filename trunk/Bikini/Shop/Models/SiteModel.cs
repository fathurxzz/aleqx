using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shop.Models;
using SiteExtensions;

namespace Shop.Models
{
    public class SiteModel:ISiteModel
    {
        public string Title { get; set; }
        public string SeoDescription { get; set; }
        public string SeoKeywords { get; set; }
        public Menu Menu { get; set; }
        public bool IsHomePage { get; set; }
        public Content Content { get; set; }
        public Menu CatalogueMenu { get; set; }

        public SiteModel(ShopContainer context, string contentId)
        {
            Title = "Bikini";

            Menu = new Menu();
            
            

            Content = context.Content.FirstOrDefault(c => c.Name == contentId) ?? context.Content.First(c => c.MainPage);

            var contents = context.Content.ToList();
            foreach (var c in contents.OrderBy(c => c.SortOrder))
            {
                Menu.Add(new MenuItem { ContentId = c.Id, ContentName = c.Name , Current = c.Name==Content.Name&&contentId!=null,SortOrder = c.SortOrder,Title = c.Title});
            }
            CatalogueMenu = new Menu();
            
            var categories = context.Category.ToList();
            
            foreach (var c in categories)
            {
                CatalogueMenu.Add(new MenuItem { ContentId = c.Id, ContentName = c.Name, SortOrder = c.SortOrder, Title = c.Title });
            }

            IsHomePage = Content.MainPage;
            SeoDescription = Content.SeoDescription;
            SeoKeywords = Content.SeoKeywords;
        }
    }
}