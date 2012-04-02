using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Posh.Models
{
    public class ArticlesModel:SiteViewModel
    {
        public List<Article> Articles { get; set; }
        public Article Article { get; set; }

        public ArticlesModel(ModelContainer context, string contentId, string articleId, bool loadContent=true) : base(context, contentId, articleId, loadContent)
        {
            Articles = _context.Article.ToList();

            if(!string.IsNullOrEmpty(articleId))
            {
                Article = _context.Article.First(a => a.Name == articleId);
                Title += " - " + Article.Title;
            }
        }
    }
}