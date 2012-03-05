using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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

        public SiteViewModel(ShopContainer context)
        {
            Title = "Магазин детских игрушек Toy-Planet";
            _context = context;
            Categories = _context.Category.Include("Children").Where(c => c.Parent == null).ToList();
            Brands = _context.Brand.ToList();
            Tags = _context.Tag.ToList();
        }
    }
}