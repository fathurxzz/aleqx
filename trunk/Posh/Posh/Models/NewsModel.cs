using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Posh.Models
{
    public class NewsModel:SiteViewModel
    {
        public List<News> News { get; set; }

        public NewsModel(ModelContainer context, string contentId, bool loadContent=true) : base(context, contentId, contentId, loadContent)
        {
            News = _context.News.ToList();
        }
    }
}