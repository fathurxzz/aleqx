using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shop.DataAccess.Entities;
using Shop.DataAccess.Repositories;

namespace Shop.WebSite.Models
{
    public class CatalogueModel : SiteModel
    {
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<ProductAttribute> ProductAttributes { get; set; }
        public Product Product { get; set; }
        public Article Article { get; set; }


        public CatalogueModel(IShopRepository repository, string categoryName=null, string subCategoryName=null, string productName=null, string articleName = null)
            : base(repository,null)
        {
            Products = AllProducts;
            ProductAttributes = new List<ProductAttribute>();
            
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
            }

            if (productName != null)
            {
                this.Product = repository.GetProduct(productName);
            }

            if (articleName != null)
            {
                this.Article = repository.GetArticle(articleName);
            }

        }
    }
}