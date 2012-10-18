using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kulumu.Helpers;
using SiteExtensions;

namespace Kulumu.Models
{
    public class SiteModel : ISiteModel
    {
        public string Title { get; set; }
        public string SeoDescription { get; set; }
        public string SeoKeywords { get; set; }
        public Menu Menu { get; set; }
        public bool IsHomePage { get; set; }


        public Content Content { get; set; }
        public Article Article { get; set; }
        public Article RandomArticle { get; set; }
        public List<Article> Articles { get; set; }
        public Product SingleDiscountProduct { get; set; }
        public List<Product> Products { get; set; }


        public SiteModel(SiteContainer context, string contentName, bool showArticles = false)
        {
            Title = "Килими";
            if (contentName == null)
            {
                Content = context.Content.First(c => c.MainPage);
            }
            else
            {
                Content = context.Content.FirstOrDefault(c => c.Name == contentName);
                if (Content == null)
                {
                    throw new HttpNotFoundException();
                }
            }

            if (!string.IsNullOrEmpty(Content.Title))
                Title += " » " + Content.Title;

            SeoDescription = Content.SeoDescription;
            SeoKeywords = Content.SeoKeywords;
            if (Content.MainPage)
            {
                IsHomePage = true;
            }

            if (showArticles)
            {
                Articles = context.Article.ToList();
            }

            if (context.Article.FirstOrDefault() != null)
            {
                RandomArticle = context.Article.RandomElement(new Random());
            }
            Products = context.Product.ToList();
            SingleDiscountProduct = Products.RandomElement(new Random());
        }
    }
}