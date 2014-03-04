using System.Linq;
using Mayka.Models.Entities;
using SiteExtensions;

namespace Mayka.Models
{
    public class SiteModel : ISiteModel 
    {
        public string Title { get; set; }
        public string SeoDescription { get; set; }
        public string SeoKeywords { get; set; }
        public Menu Menu { get; set; }
        public bool IsHomePage { get; set; }
        public Content Content { get; set; }

        public SiteModel(SiteContext context, string contentId)
        {
            Title = "Майкаджексон";
            Content = context.Content.FirstOrDefault(c => c.Name == contentId) ?? context.Content.First(c => c.MainPage);
        }
    }
}