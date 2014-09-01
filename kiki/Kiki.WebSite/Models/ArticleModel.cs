using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kiki.DataAccess.Entities;
using Kiki.DataAccess.Repositories;

namespace Kiki.WebSite.Models
{
    public class ArticleModel:SiteModel
    {
        public Article Article { get; set; }

        public ArticleModel(ISiteRepository repository, string contentName, string articleId) : base(repository, contentName)
        {
            Article = Articles.First(a => a.Name == articleId);
        }
    }
}