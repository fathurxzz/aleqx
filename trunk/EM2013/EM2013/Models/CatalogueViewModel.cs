using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Web;

namespace EM2013.Models
{
    public class CatalogueViewModel : SiteViewModel
    {
        public Category Category { get; set; }
        public Product Product { get; set; }
        public List<Product> Products { get; set; }
        public int TotalProductsCount { get; set; }
        public string TitleToCategory { get; set; }

        public CatalogueViewModel(SiteContext context, string category, string product, int? page)
            : base(context, category == "" ? "" : null)
        {
            UpdateMainMenu(category, product);
            
            if(!string.IsNullOrEmpty(category))
            {
                Category = context.Category.Include("Products").First(c => c.Name == category);
                PageTitle += " » " + Category.Title;
                SeoDescription = Category.SeoDescription;
                SeoKeywords = Category.SeoKeywords;
                Title = Category.Title;
                TitleToCategory = Category.TitleToCategory;

                TotalProductsCount = Category.Products.Count();
                Products = ApplyPaging(Category.Products, page).ToList();
            }

            if(!string.IsNullOrEmpty(product))
            {
                Product = context.Product.Include("Category").Include("ProductItems").First(p => p.Name == product);
                SeoDescription = Product.SeoDescription;
                SeoKeywords = Product.SeoKeywords;
                PageTitle += " » " + Product.Title;
                Title = Product.Title;
            }
        }

        private void UpdateMainMenu(string category, string product)
        {
            foreach (var item in Menu.Where(item => item.ContentName == category))
            {
                if (string.IsNullOrEmpty(product))
                    item.Current = true;
                else
                    item.Selected = true;
            }
        }

        IEnumerable<Product> ApplyPaging(IEnumerable<Product> products, int? page)
        {
            if (products == null)
                return null;
            int currentPage = page ?? 0;
            int pageSize = Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]);
            if (page < 0)
                return products;
            return products.Skip(currentPage * pageSize).Take(pageSize);
        }
    }
}