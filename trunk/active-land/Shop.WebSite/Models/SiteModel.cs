using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Shop.DataAccess.Entities;
using Shop.DataAccess.Repositories;
using Shop.WebSite.Helpers;
using MenuItem = Shop.WebSite.Helpers.MenuItem;

namespace Shop.WebSite.Models
{
    public class SiteModel : ISiteModel
    {
        public string Title { get; set; }
        public string SeoDescription { get; set; }
        public string SeoKeywords { get; set; }
        public bool IsHomePage { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<MenuItem> MenuItems { get; set; }
        public IEnumerable<Product> SpecialOffers { get; set; }
        public IEnumerable<Product> AllProducts { get; set; }
        public IEnumerable<DataAccess.Entities.Content> Contents { get; set; }

        public SiteModel(IShopRepository repository)
        {
            Title = "Active Land";
            Categories = repository.GetCategories();
            AllProducts = repository.GetProducts();
            SpecialOffers = AllProducts.Where(p => p.IsDiscount || p.IsNew || p.IsTopSale);
            Contents = repository.GetContents();
        }
    
    }
}