using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop.Models
{
    public class ShopModel : SiteModel
    {
        public Category Category { get; set; }
        public Product Product { get; set; }

        public ShopModel(ShopContainer context, string categoryId, int? productId)
            : base(context, null)
        {

            Category = context.Category.Include("Products").First(c => c.Name == categoryId);

            foreach (var item in CatalogueMenu.Where(item => item.ContentName == Category.Name))
            {
                if (productId != null)
                {
                    item.Selected = true;
                }
                else
                {
                    item.Current = true;
                }
            }

            if (!string.IsNullOrEmpty(Category.SeoDescription))
                SeoDescription = Category.SeoDescription;
            if (!string.IsNullOrEmpty(Category.SeoKeywords))
                SeoKeywords = Category.SeoKeywords;


            if (productId.HasValue)
            {
                Product = context.Product.First(p => p.Id == productId);
            }

        }
    }
}