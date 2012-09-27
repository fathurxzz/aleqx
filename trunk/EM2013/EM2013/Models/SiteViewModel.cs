using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SiteExtensions;

namespace EM2013.Models
{
    public class SiteViewModel:ISiteModel 
    {
        public string Title { get; set; }
        public string SeoDescription { get; set; }
        public string SeoKeywords { get; set; }
        public Menu Menu { get; set; }
        public bool IsHomePage { get; set; }

        public Content Content { get; set; }


        public SiteViewModel(SiteContext context, string id)
        {

        }
    }
}