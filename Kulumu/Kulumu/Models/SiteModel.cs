using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
        public Article CustomArticle { get; set; }
        public List<Article> Articles { get; set; }


        public SiteModel(SiteContainer context, string contentName)
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
        }
    }
}