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
        public Helpers.Menu SiteMenu { get; set; }

        public SiteModel(SiteContext context, string contentId)
        {
            var contents = context.Content.ToList();
            Title = "Майкаджексон";


            Content = contents.FirstOrDefault(c => c.Name == contentId) ?? context.Content.First(c => c.MainPage);

            SiteMenu = new Helpers.Menu();

            foreach (var c in contents)
            {
                SiteMenu.Add(new MenuItem
                {
                    ContentId = c.Id,
                    ContentName = c.Name,
                    Current = c.Name == contentId,
                    SortOrder = c.SortOrder,
                    Title = c.MenuTitle
                });
            }
        }
    }
}