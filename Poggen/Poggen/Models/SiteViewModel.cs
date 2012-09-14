using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SiteExtensions;

namespace Poggen.Models
{
    public class SiteViewModel : ISiteModel
    {
        public string Title { get; set; }
        public string SeoDescription { get; set; }
        public string SeoKeywords { get; set; }
        public Menu Menu { get; set; }
        public bool IsHomePage { get; set; }
        public Content Content { get; set; }

        public SiteViewModel(SiteContainer context, string contentName)
        {
            Title = "Poggenpohl";

            if (contentName != null)
            {
                Content = contentName != ""
                              ? context.Content.First(c => c.Name == contentName)
                              : context.Content.First(c => c.MainPage);
                SeoDescription = Content.SeoDescription;
                SeoKeywords = Content.SeoKeywords;
                IsHomePage = Content.MainPage;
            }
        }
    }
}