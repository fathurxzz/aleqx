using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop.Models
{
    public class ShopViewModel:SiteViewModel
    {
        public List<Product> Products { get; set; }
        
        
        public ShopViewModel(ShopContainer context, string categoryId, string productId)
            : base(context)
        {
            foreach (var category in Categories.Where(category => category.Name == categoryId))
            {
                category.Selected = true;
                Title +=" - "+ category.Title;
            }
        }
    }
}