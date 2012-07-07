using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rakurs.Models
{
    public class CatalogueViewModel : SiteViewModel
    {
        public Category Category { get; set; }
        public Category SubCategory { get; set; }
        public Menu SubcategoriesMenu { get; set; }

        public CatalogueViewModel(StructureContainer context, string categoryName, string subCategoryName)
            : base(context, null)
        {
            var category = context.Category.Include("Children").First(c => c.Name == categoryName);
            Category = category;
            var currentCategory = category;

            if (!string.IsNullOrEmpty(subCategoryName))
            {
                var subCategory = category.Children.First(c => c.Name == subCategoryName);
                SubCategory = subCategory;
                currentCategory = subCategory;
            }

            SubcategoriesMenu = new Menu();
            foreach (var c in category.Children)
            {
                SubcategoriesMenu.Add(
                    new MenuItem
                    {
                        ContentId = c.Id,
                        ContentName = c.Name,
                        IsMainPage = false,
                        Selected = c.Name == subCategoryName,
                        SortOrder = c.SortOrder,
                        Title = c.Title
                    });
            }

            foreach (var m in MainMenu.Where(m => m.IsGalleryMenuItem))
            {
                m.Selected = true;
            }

            Title += " - " + currentCategory.Title;
            SeoDescription = currentCategory.SeoDescription;
            SeoKeywords = currentCategory.SeoKeywords;
        }
    }
}