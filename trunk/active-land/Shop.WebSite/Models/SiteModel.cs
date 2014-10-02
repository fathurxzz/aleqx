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
        public Shop.DataAccess.Entities.Content Content { get; set; }
        public IEnumerable<Article> Articles { get; set; }
        public string CurrentLangCode { get; set; }
        public IEnumerable<QuickAdvice> QuickAdvices { get; set; }
        public string ErrorMessage { get; set; }
        //public DateTime QueryTime { get; set; }

        public SiteModel(IShopRepository repository, string contentName )
        {
            Title = "Active Land";
            Categories = repository.GetCategories();
            AllProducts = repository.GetActiveProducts();
            SpecialOffers = AllProducts.Where(p => p.IsDiscount || p.IsNew || p.IsTopSale).OrderBy(p=>Guid.NewGuid()).Take(8);
            Contents = repository.GetContents();
            Content = contentName != null ? repository.GetContent(contentName) : repository.GetContent();
            Articles = repository.GetArticles(true);
            QuickAdvices = repository.GetQuickAdvices(true);
        }
    
    }
}