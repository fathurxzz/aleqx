using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vip.Models
{
    public class ArticlesViewModel:SiteViewModel
    {
        public List<Article> Articles { get; set; }

        public ArticlesViewModel(SiteContainer context, string contentName) : base(context, contentName)
        {
            Articles = context.Article.ToList();
        }
    }
}