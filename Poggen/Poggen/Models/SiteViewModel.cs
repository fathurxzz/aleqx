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

        public SiteViewModel(SiteContainer context, string contentName)
        {
            Title = "Poggenpohl";

        }
    }
}