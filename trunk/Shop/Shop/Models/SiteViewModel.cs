using System.Collections.Generic;
using System.Linq;
using Shop.Helpers;

namespace Shop.Models
{
    public class SiteViewModel
    {
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Brand> Brands { get; set; }
        public IEnumerable<Tag> Tags { get; set; }
        public IEnumerable<OrderItem> OrderItems { get; set; }
        private readonly ShopContainer _context;
        public string Title { get; set; }
        public string SeoDescription { get; set; }
        public string SeoKeywords { get; set; }
        public Content Content { get; set; }
        public List<MenuItem> MainMenu { get; set; }

        public SiteViewModel(ShopContainer context, string contentId)
        {
            Title = "Магазин детских игрушек Toy-Planet";
            _context = context;

            Categories = _context.Category.Include("Children").Where(c => c.Parent == null).ToList();
            Brands = _context.Brand.ToList();
            Tags = _context.Tag.ToList();

            MainMenu = new List<MenuItem>();

            var contents = context.Content.Where(c=>c.Published).ToList();
            foreach (var content in contents)
            {
                MainMenu.Add(new MenuItem { Name = content.Name, Title = content.Title, Selected = content.Name == contentId,SortOrder = content.SortOrder});  
            }

            if (!string.IsNullOrEmpty(contentId))
            {
                var content = context.Content.FirstOrDefault(c => c.Name == contentId);
                if (content == null)
                {
                    throw new HttpNotFoundException();
                }
                Content = content;
                Title += " - " + content.Title;
                SeoDescription = content.SeoDescription;
                SeoKeywords = content.SeoKeywords;
            }
            else
            {
                Content = context.Content.ToList().OrderBy(c => c.SortOrder).FirstOrDefault();
            }
        }
    }
}