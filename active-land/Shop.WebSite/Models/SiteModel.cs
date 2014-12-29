using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI.WebControls;
using log4net;
using Shop.DataAccess.Entities;
using Shop.DataAccess.Repositories;
using Shop.WebSite.Helpers;
using MenuItem = Shop.WebSite.Helpers.MenuItem;

namespace Shop.WebSite.Models
{
    public class SiteModel : ISiteModel
    {
        protected static readonly ILog Log = LogManager.GetLogger(typeof(CatalogueModel));

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
        public IEnumerable<MainPageBanner> MainPageBanners { get; set; }
        public IEnumerable<MainPageBanner> SiteBanners { get; set; }


        protected Stopwatch _sw = new Stopwatch();

        public SiteModel(IShopRepository repository, int langId, string contentName, bool showSpecialOffers =false)
        {
            
            _sw.Start();


            Title = "Active Land";

            IQueryable<Category> categories = null;

            categories = repository.GetCategories().OrderBy(c => c.SortOrder);
            Categories = categories.Include(c => c.Children).ToList();

            //Categories = repository.GetCategories().Include(x=>x.Children).ToList();
            foreach (var category in Categories)
            {
                category.CurrentLang = langId;
            }

            if (showSpecialOffers)
                SpecialOffers = GetSpecialOffers(repository, langId, int.Parse(SiteSettings.GetShopSetting("SpecialOffersQuantity")));

            Contents = repository.GetContents();

            Content = contentName != null ? (contentName == "category" ? repository.GetCatalogueContent() : repository.GetContent(contentName)) : repository.GetContent();
            MainPageBanners = repository.GetMainPageBanners();
            SiteBanners = repository.GetSiteBanners().OrderBy(p => Guid.NewGuid()).Take(2).ToList();

            if (Content.ContentType == 2)
            {
                Articles = GetAllArticles(repository, langId);
            }

            LastArticles = GetLastArticles(repository, langId, int.Parse(SiteSettings.GetShopSetting("ArticlesQuantity")));
            QuickAdvices = repository.GetQuickAdvices(true);
            
           
        }

        private static IEnumerable<Product> GetSpecialOffers(IShopRepository repository, int langId, int quantity)
        {
            var orderedProducts = repository.GetAllProducts().Where(p => p.IsDiscount || p.IsNew || p.IsTopSale);
            //var randomProducts = orderedProducts.Include(x => x.ProductImages)
            //    //.ToList()
            //    //.OrderBy(p => Guid.NewGuid())
            //    .Take(quantity)
            //    .ToList();

            //var randomProducts = (from p in orderedProducts orderby SqlFunctions.Rand(1) select p).Take(8).ToList();


            var randomProducts = orderedProducts.Include(x => x.ProductImages).Take(quantity).ToList();
            
            //var randomProducts = orderedProducts.Include(x => x.ProductImages).AsEnumerable().OrderBy(p => Guid.NewGuid()).Take(quantity).ToList();

            //var cnt = orderedProducts.Count();

            foreach (var product in randomProducts)
            {
                product.CurrentLang = langId;
                //product.Category.CurrentLang = langId;
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