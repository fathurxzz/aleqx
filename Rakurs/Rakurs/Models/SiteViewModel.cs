using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Rakurs.Helpers;

namespace Rakurs.Models
{
    public class SiteViewModel
    {
        public Content Content { get; set; }
        public RakursSiteMenu MainMenu { get; set; }
        public bool IsHomePage { get; set; }
        public string Title { get; set; }
        public string SeoDescription { get; set; }
        public string SeoKeywords { get; set; }
        public IEnumerable<Product> GalleryFrameItems { get; set; }


        protected StructureContainer Context;

        public SiteViewModel(StructureContainer context, string contentName)
        {
            Title = "Ракурс";
            Context = context;

            var contentList = Context.Content.ToList();
            FetchMainMenuItems(contentList, contentName);

            Content content = null;
            if (!string.IsNullOrEmpty(contentName))
            {
                content = contentList.Where(c => c.Name == contentName).FirstOrDefault();
                if (content == null)
                {
                    throw new HttpNotFoundException();
                }
            }
            else if (contentName == "")
            {
                content = context.Content.FirstOrDefault(c => c.MainPage);
                IsHomePage = true;
            }

            if (IsHomePage)
            {
                GetGalleryFrameItems();
            }

            Content = content;
            if (content != null)
            {
                Title += " - " + content.Title;
                SeoDescription = content.SeoDescription;
                SeoKeywords = content.SeoKeywords;
            }

        }

        private void GetGalleryFrameItems()
        {
            var products = Context.Product.Include("Category").Where(p => p.ShowOnMainPage).ToList();
            foreach (var product in products)
            {
                var cat = Context.Category.Include("Parent").First(c => c.Id == product.Category.Id);
                product.Path = new List<PathItem>
                                       {
                                           new PathItem {Name = "", ParentName = cat.Parent.Name, Title = cat.Parent.Title},
                                           new PathItem {Name = product.Category.Name, ParentName = cat.Parent.Name, Title = product.Category.Title}
                                       };
            }
            GalleryFrameItems = products;
        }

        private void FetchMainMenuItems(IEnumerable<Content> contentList, string contentName)
        {
            MainMenu = new RakursSiteMenu();
            var menuItems = contentList.Select(c => new RakursMenuItem { ContentId = c.Id, ContentName = c.Name, Title = c.Title, SortOrder = c.SortOrder, IsMainPage = c.MainPage, Selected = c.Name == contentName, IsGalleryMenuItem = c.IsGallery });
            foreach (var menuItem in menuItems)
            {
                MainMenu.Add(menuItem);
            }
        }
    }
}