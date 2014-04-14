using System.Collections.Generic;
using System.Linq;
using SiteExtensions;

namespace EM2014.Models
{
    public class SiteModel : ISiteModel
    {
        public string Title { get; set; }
        public string SeoDescription { get; set; }
        public string SeoKeywords { get; set; }
        public bool IsHomePage { get; set; }

        public Content Content { get; set; }
        public Product Product { get; set; }
        public List<Helpers.MenuItem> Menu { get; set; }
        protected IQueryable<Content> Contents { get; set; }

        public SiteModel(SiteContext context, string contentId, string productId)
        {
            Contents = context.Contents;
            Title = "Студия Евгения Миллера";
            var minSortorder = Contents.Min(c => c.SortOrder);
            foreach (var content in Contents.Where(content => content.SortOrder == minSortorder))
            {
                content.IsHomepage = true;
                break;
            }
            
            if (productId != null)
            {
                Product = context.Products.First(p => p.Name == productId);
                Content = Product.Content;
            }
            else if (contentId == "")
            {
                //Content = Contents.First(c => c.IsHomepage==true);
                Content = Contents.First(content => content.SortOrder == minSortorder);
            }
            else
            {
                Content = Contents.FirstOrDefault(c => c.Name == contentId);
            }

            Menu = new List<Helpers.MenuItem>();

            foreach (var c in Contents)
            {
                Menu.Add(new Helpers.MenuItem
                {
                    ContentId = c.Id,
                    ContentName = c.Name,
                    Current = (c.Name == contentId || contentId == "" && c.IsHomepage)&&Product==null,
                    Selected = c.Name == Content.Name,
                    SortOrder = c.SortOrder,
                    Title = c.Title
                });
            }

            Title += " » " + Content.Title;

            SeoDescription = Content.SeoDescription;
            SeoKeywords = Content.SeoKeywords;

        }
    }
}