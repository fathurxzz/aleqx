using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Listelli.Models
{
    public class ArticlesModel:SiteModel
    {
        public IEnumerable<Article> Articles { get; set; }

        public ArticlesModel(Language lang, SiteContainer context) : base(lang, context, "news")
        {
            Articles = context.Article.Include("ArticleItems").ToList();
            foreach (var article in Articles)
            {
                article.CurrentLang = lang.Id;

                foreach (var item in article.ArticleItems)
                {
                    item.CurrentLang = lang.Id;
                }
            }
        }
    }
}