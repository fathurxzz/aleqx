using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shop.DataAccess.Entities;
using Shop.DataAccess.Repositories;

namespace Shop.WebSite.Models
{
    public class CatalogueModel:SiteModel
    {
        public IEnumerable<Product> Products { get; set; }

        public CatalogueModel(IShopRepository repository) : base(repository)
        {
            Products = AllProducts;
        }
    }
}