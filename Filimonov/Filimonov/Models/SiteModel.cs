using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SiteExtensions;

namespace Filimonov.Models
{
    public class SiteModel : ISiteModel
    {
        public string Title { get; set; }
        public string SeoDescription { get; set; }
        public string SeoKeywords { get; set; }
        public Menu Menu { get; set; }
        public bool IsHomePage { get; set; }

        public IEnumerable<Content> Contents { get; set; }
        public IEnumerable<Project> Projects { get; set; }

        public SiteModel(SiteContainer context)
        {
            Title = "Filimonov";
            Contents = context.Content.ToList();
            Projects = context.Project.ToList();

            SeoDescription = Contents.First().SeoDescription;
            SeoKeywords = Contents.First().SeoKeywords;

        }
    }
}