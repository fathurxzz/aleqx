using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop.Models
{
    public class ShopViewModel:SiteViewModel
    {
        public List<Product> Products { get; set; }
        
        
        public ShopViewModel(ShopContainer context, int? categoryId)
            : base(context)
        {
        }

    }
}