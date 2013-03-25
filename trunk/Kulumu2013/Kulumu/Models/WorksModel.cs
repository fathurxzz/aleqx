using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kulumu.Models
{
    public class WorksModel : SiteModel
    {
        public Category Category { get; set; }
        public Product Product { get; set; }
        private readonly SiteContainer _context;


        public WorksModel(SiteContainer context, int productId)
            : base(context, "gallery")
        {
            _context = context;

            Product = _context.Product.Include("Category").Include("ProductImages").First(p => p.Id == productId);
            Category = Product.Category;
        }
    }
}