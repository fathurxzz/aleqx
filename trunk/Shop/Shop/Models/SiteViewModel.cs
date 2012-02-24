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
        private readonly ShopContainer _context;

        public SiteViewModel(ShopContainer context)
        {
            _context = context;
            Categories = _context.Category.ToList();
            Brands = _context.Brand.ToList();
        }
    }
}