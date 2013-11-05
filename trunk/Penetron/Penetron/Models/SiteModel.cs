using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SiteExtensions;

namespace Penetron.Models
{
    public class SiteModel : ISiteModel
    {
        public string Title { get; set; }
        public string SeoDescription { get; set; }
        public string SeoKeywords { get; set; }
        public Menu Menu { get; set; }
        public bool IsHomePage { get; set; }
        public Technology Technology { get; set; }
        public Building Building { get; set; }
        public Content Content { get; set; }

        public SiteModel(SiteContext context, string contentId)
        {
            Title = "ПЕНЕТРОН УКРАИНА";
            Content = context.Content.First(c => c.Name == contentId || c.MainPage);
            SeoDescription = Content.SeoDescription;
            SeoKeywords = Content.SeoKeywords;
        }

    }
}