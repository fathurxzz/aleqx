using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shop.DataAccess.Entities;
using Shop.DataAccess.Repositories;
using Shop.WebSite.Helpers;

namespace Shop.WebSite.Models
{
    public class CatalogueModel : SiteModel
    {
        public List<Product> Products { get; set; }
        public IEnumerable<ProductAttribute> ProductAttributes { get; set; }
        public Product Product { get; set; }
        public Article Article { get; set; }
        public string CurrentFilter { get; set; }
        public string[] FilterArray { get; set; }
        public int ProductTotalCount { get; set; }
        public Category CurrentCategory { get; set; }
        public int Page { get; set; }


        public CatalogueModel(IShopRepository repository, int? page, string categoryName = null, string subCategoryName = null, string productName = null, string articleName = null, string filter = null)
            : base(repository, null)
        {
            ProductAttributes = new List<ProductAttribute>();
            CurrentFilter = filter;
            FilterArray = filter != null ? filter.Split(new[] { "-" }, StringSplitOptions.RemoveEmptyEntries) : new string[0];

            Products = new List<Product>();

            if (FilterArray.Any())
            {

                foreach (var product in from product in AllProducts from pav in product.ProductAttributeValues.Where(pav => FilterArray.Contains(pav.Id.ToString())) select product)
                {
                    if (!Products.Contains(product))
                        Products.Add(product);
                }
            }
            else
            {
                foreach (var product in AllProducts)
                {
                    Products.Add(product);
                }
            }

            ProductTotalCount = Products.Count;

            IQueryable<Product> products = null;

            products = Products.OrderBy(p => p.Id).AsQueryable();

            products = ApplyPaging(products, page, SiteSettings.ProductsPageSize);

            Products = products.ToList();


            //if (!string.IsNullOrEmpty(categoryName))
            //{
            //    var category = Categories.First(c => c.Name == categoryName);
            //    ProductAttributes = category.ProductAttributes.Where(pa => pa.IsFilterable);
            //}

            var currentCategory = !string.IsNullOrEmpty(subCategoryName) ? subCategoryName : categoryName;

            var category = Categories.FirstOrDefault(c => c.Name == currentCategory);
            if (category != null)
            {
                ProductAttributes = repository.GetProductAttributes(category.Id).Where(pa => pa.IsFilterable);
                CurrentCategory = repository.GetCategory(category.Id);
            }

            if (productName != null)
            {
                this.Product = repository.GetProduct(productName);
                this.Product.IsInCart = WebSession.OrderItems.ContainsKey(Product.Id);
            }

            if (articleName != null)
            {
                this.Article = repository.GetArticle(articleName);
            }

        }

        IQueryable<Product> ApplyPaging(IQueryable<Product> products, int? page, int pageSize)
        {
            if (products == null)
                return null;
            int currentPage = page ?? 0;
            Page = currentPage;
            if (page < 0)
                return products;
            return products.Skip(currentPage * pageSize).Take(pageSize);
        }
    }
}