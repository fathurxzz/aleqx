using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop.Models
{
    public class ShopModel : SiteModel
    {
        public Category Category { get; set; }

        public ShopModel(ShopContainer context, string categoryId)
            : base(context, null)
        {

            Category = context.Category.First(c => c.Name == categoryId);

            foreach (var item in CatalogueMenu.Where(item => item.ContentName == Category.Name))
            {
                item.Current = true;
            }

            if (!string.IsNullOrEmpty(Category.SeoDescription))
                SeoDescription = Category.SeoDescription;
            if (!string.IsNullOrEmpty(Category.SeoKeywords))
                SeoKeywords = Category.SeoKeywords;
        }
    }
}