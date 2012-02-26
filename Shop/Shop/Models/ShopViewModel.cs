using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop.Models
{
    public class ShopViewModel:SiteViewModel
    {
        public List<Product> Products { get; set; }
        public Product Product { get; set; }
        
        public ShopViewModel(ShopContainer context, string categoryId, string productId)
            : base(context)
        {
            foreach (var category in Categories.Where(category => category.Name == categoryId))
            {
                category.Selected = true;
                Title +=" - "+ category.Title;
                SeoDescription = category.SeoDescription;
                SeoKeywords = category.SeoKeywords;
            }

            Products = context.Product.Include("ProductImages").Where(p => p.Category.Name == categoryId).ToList();

            if(!string.IsNullOrEmpty(productId))
            {
                var product =context.Product.First(p => p.Name == productId);
                Product = product;
                SeoDescription = product.SeoDescription;
                SeoKeywords = product.SeoKeywords;
            }
            
        }
    }
}