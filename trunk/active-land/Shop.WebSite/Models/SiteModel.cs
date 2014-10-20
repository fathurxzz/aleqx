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
        public IEnumerable<DataAccess.Entities.Content> Contents { get; set; }
        public Shop.DataAccess.Entities.Content Content { get; set; }
        public IEnumerable<Article> LastArticles { get; set; }
        public IEnumerable<Article> Articles { get; set; }
        public string CurrentLangCode { get; set; }
        public IEnumerable<QuickAdvice> QuickAdvices { get; set; }
        public string ErrorMessage { get; set; }

        public SiteModel(IShopRepository repository, int langId, string contentName )
        {
            Title = "Active Land";
            Categories = repository.GetCategories();
            SpecialOffers = GetSpecialOffers(repository, langId, int.Parse(SiteSettings.GetShopSetting( "SpecialOffersQuantity")));
            Contents = repository.GetContents();
            Content = contentName != null ? repository.GetContent(contentName) : repository.GetContent();

            if (Content.ContentType == 2)
            {
                Articles = GetAllArticles(repository, langId);
            }

            LastArticles = GetLastArticles(repository, langId, int.Parse(SiteSettings.GetShopSetting( "ArticlesQuantity")));
            QuickAdvices = repository.GetQuickAdvices(true);
        }

        private static IEnumerable<Product> GetSpecialOffers(IShopRepository repository, int langId, int quantity)
        {
            var orderedProducts = repository.GetAllProducts().Where(p => p.IsDiscount || p.IsNew || p.IsTopSale);
            var randomProducts = orderedProducts.OrderBy(p => Guid.NewGuid()).Take(quantity).ToList();
            foreach (var product in randomProducts)
            {
                product.CurrentLang = langId;
                product.Category.CurrentLang = langId;
                if (product.ProductImages.Any())
                {
                    var pi = product.ProductImages.FirstOrDefault(c => c.IsDefault) ?? product.ProductImages.First();
                    product.ImageSource = pi.ImageSource;
                }
            }
            return randomProducts;
        }

        private static IEnumerable<Article> GetLastArticles(IShopRepository repository, int langId, int quantity)
        {
            var articles = repository.GetActiveArticles().OrderByDescending(a => a.Date).Take(quantity).ToList();
            return LoadArticles(articles, langId);
        }

        private static IEnumerable<Article> GetAllArticles(IShopRepository repository, int langId)
        {
            var articles = repository.GetActiveArticles().OrderByDescending(a => a.Date).ToList();
            return LoadArticles(articles, langId);
        }

        private static IEnumerable<Article> LoadArticles(IEnumerable<Article> articles, int langId)
        {
            foreach (var article in articles)
            {
                article.CurrentLang = langId;
                foreach (var articleItem in article.ArticleItems)
                {
                    articleItem.CurrentLang = langId;
                }
            }
            return articles;
        }
    }
}