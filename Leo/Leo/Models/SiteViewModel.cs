using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SiteExtensions;

namespace Leo.Models
{
    public class SiteViewModel : ISiteModel
    {
        public string Title { get; set; }
        public string SeoDescription { get; set; }
        public string SeoKeywords { get; set; }
        public Menu Menu { get; set; }
        
        public SiteViewModel(SiteContainer context, string categoryName)
        {
            var categories = context.Category;
            Menu = InitializeMainMenu(categories, categoryName);


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