using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SiteExtensions;

namespace Vip.Models
{
    public class SiteViewModel:ISiteModel 
    {
        public string Title { get; set; }
        public string SeoDescription { get; set; }
        public string SeoKeywords { get; set; }
        public bool IsHomePage { get; set; }
        public Menu Menu { get; set; }
        public Content Content { get; set; }
        public IEnumerable<Layout> Layouts { get; set; }

        public SiteViewModel(SiteContainer context,string contentName)
        {
            if (contentName != null)
            {
                Content = context.Content.First(c => c.Name == contentName || c.MainPage);
                if (Content.MainPage)
                {
                    IsHomePage = true;
                    Layouts = context.Layout.Include("Parent").Include("Children").ToList();
                }

            }
        }
    }
}