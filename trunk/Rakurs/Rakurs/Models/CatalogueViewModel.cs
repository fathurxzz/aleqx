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
        public Category CurrentCategory { get; set; }
        public Menu SubcategoriesMenu { get; set; }
        public int? CurrentFilterId { get; set; }
        public IEnumerable<Product> Products { get; set; }

        public CatalogueViewModel(StructureContainer context, string categoryName, string subCategoryName, int? filter = null)
            : base(context, null)
        {
            var title = "";
            var category = Context.Category
                .Include("Children")
                //.Include("ProductAttributes")
                .First(c => c.Name == categoryName);
            category.ProductAttributes.Load();
            Category = category;

            var currentCategory = category;
            CurrentCategory = category;
            title += " - " +(SiteSettings.Language==Language.Ru? category.Title:category.TitleEng);

            if (!string.IsNullOrEmpty(subCategoryName))
            {
                var subCategory = category.Children.First(c => c.Name == subCategoryName);
                subCategory.ProductAttributes.Load();
                SubCategory = subCategory;
                currentCategory = subCategory;
                CurrentCategory = subCategory;
                title += (SiteSettings.Language==Language.Ru? subCategory.Title:subCategory.TitleEng);
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
                        Title = SiteSettings.Language==Language.Ru? c.Title:c.TitleEng
                    });
            }

            foreach (var m in MainMenu.Where(m => m.IsGalleryMenuItem))
            {
                m.Selected = true;
            }

            CurrentFilterId = filter;


            //var productAttributes = Context.ProductAttribute.Where(p => !filter.HasValue || p.Id == filter);

            var products = Context.Product.Include("ProductAttributes").Where(p => p.CategoryId == currentCategory.Id).ToList();
            Products = ApplyFilter(products, filter);


            Title += " - " + title;
            SeoDescription = currentCategory.SeoDescription;
            SeoKeywords = currentCategory.SeoKeywords;
        }

        private IEnumerable<Product> ApplyFilter(IEnumerable<Product> products, int? attributeId)
        {
            if (!attributeId.HasValue)
                return products;
            else
            {
                var productAttribute = Context.ProductAttribute.First(pa => pa.Id == attributeId);

                return products.Where(p => p.ProductAttributes.Contains(productAttribute));

                //var result = new List<Product>();
                //foreach (var product in products)
                //{

                //}
            }
        }
    }
}