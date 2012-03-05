using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shop.Helpers;

namespace Shop.Models
{
    public class SiteViewModel
    {
        public List<Category> Categories { get; set; }
        public List<Brand> Brands { get; set; }
        public List<Tag> Tags { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        private readonly ShopContainer _context;
        public string Title { get; set; }
        public string SeoDescription { get; set; }
        public string SeoKeywords { get; set; }
        public Content Content { get; set; }

        public SiteViewModel(ShopContainer context, string contentId)
        {
            Title = "Магазин детских игрушек Toy-Planet";
            _context = context;

            Categories = _context.Category.Include("Children").Where(c => c.Parent == null).ToList();
            Brands = _context.Brand.ToList();
            Tags = _context.Tag.ToList();

            if (!string.IsNullOrEmpty(contentId))
            {
                using (var contentContext = new ContentContainer())
                {
                    var content = contentContext.Content.FirstOrDefault(c => c.Name == contentId);
                    if (content == null)
                    {
                        throw new HttpNotFoundException();
                    }
                    Content = content;
                }
            }
        }
    }
}