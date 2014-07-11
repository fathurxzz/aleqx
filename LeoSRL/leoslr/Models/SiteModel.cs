using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Leo.Models.Json;
using Newtonsoft.Json;
using SiteExtensions;

namespace Leo.Models
{
    public class SiteModel:ISiteModel
    {
        public string Title { get; set; }
        public string SeoDescription { get; set; }
        public string SeoKeywords { get; set; }
        public bool IsHomePage { get; set; }
        
        public List<MenuItem> SiteMenu { get; set; }

        protected readonly SiteContext _context;

        public SiteModel(Language lang, SiteContext context, string contentId)
        {
            Title = "Leo";
            _context = context;

           
        }
    }
}