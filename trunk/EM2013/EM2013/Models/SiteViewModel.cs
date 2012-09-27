using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SiteExtensions;

namespace EM2013.Models
{
    public class SiteViewModel:ISiteModel 
    {
        public string Title { get; set; }
        public string SeoDescription { get; set; }
        public string SeoKeywords { get; set; }
        public Menu Menu { get; set; }
        public bool IsHomePage { get; set; }

        public string Description { get; set; }

        public Content Content { get; set; }


        public SiteViewModel(SiteContext context, string contentName, string categoryName)
        {
            var categories = context.Category;
            Menu = InitializeMainMenu(categories, categoryName);

            if (string.IsNullOrEmpty(categoryName))
            {
                IsHomePage = true;
                Content = context.Content.First(c => c.HomePage);
            }

        }

        private static Menu InitializeMainMenu(IEnumerable<Category> categories, string categoryName)
        {
            var menu = new Menu();
            var menuItems = categories.Select(c => new MenuItem
            {
                ContentId = c.Id,
                ContentName = c.Name,
                Title = c.Title,
                SortOrder = c.SortOrder,
                Current = c.Name == categoryName
            });
            menu.AddRange(menuItems);
            return menu;
        }
    }
}