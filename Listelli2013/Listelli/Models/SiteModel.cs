using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SiteExtensions;

namespace Listelli.Models
{
    public class SiteModel:ISiteModel 
    {
        public string Title { get; set; }
        public string SeoDescription { get; set; }
        public string SeoKeywords { get; set; }
        public Menu Menu { get; set; }
        public bool IsHomePage { get; set; }
        public Content Content { get; set; }

        public SiteModel(Language lang, SiteContainer context, string contentId)
        {
            Title = "Listelli 2013";
            
            Content = context.Content.FirstOrDefault(c => c.Name == contentId) ?? context.Content.First(c => c.MainPage);
            IsHomePage = Content.MainPage;
            Content.CurrentLang = lang.Id;

            SeoDescription = Content.SeoDescription;
            SeoKeywords = Content.SeoKeywords;
        }
    }
}