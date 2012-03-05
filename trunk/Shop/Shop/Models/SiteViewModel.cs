using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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

            var contents = context.Content.ToList();
            foreach (var content in contents)
            {
                MainMenu.Add(new MenuItem { Name = content.Name, Title = content.Title, Selected = content.Name == contentId });  
            }

            

            /*
            MainMenu.AddRange(context.Content.Select(c =>
                new MenuItem
                {
                    Title = c.Title,
                    Name = c.Name,
                    Selected = c.Name == contentId
                }).ToList());
            */
            /*
            MainMenu = context.Content.Select(c =>
                new MenuItem
                    {
                        Title = c.Title,
                        Name = c.Name,
                        Selected = c.Name == contentId
                    }).ToList();
            */

            

            if (!string.IsNullOrEmpty(contentId))
            {
                var content = context.Content.FirstOrDefault(c => c.Name == contentId);
                if (content == null)
                {
                    throw new HttpNotFoundException();
                }
                Content = content;
            }
        }
    }
}