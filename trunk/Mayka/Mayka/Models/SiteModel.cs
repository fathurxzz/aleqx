using System.Linq;
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
        //public Content Content { get; set; }

        //public SiteModel(SiteContext context, string contentId)
        //{
        //    Title = "Майкаджексон";
        //    Content = context.Contents.FirstOrDefault(c => c.Name == contentId) ?? context.Contents.First(c => c.MainPage);
        //}
    }
}