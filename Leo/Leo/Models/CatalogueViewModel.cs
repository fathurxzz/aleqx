using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SiteExtensions;

namespace Leo.Models
{
    public class CatalogueViewModel : SiteViewModel
    {
        public Category Category { get; set; }
        public IEnumerable<Product> Products { get; set; }

        public CatalogueViewModel(SiteContainer context, string categoryName)
            : base(context, categoryName)
        {
            var category = context.Category.Include("Products").FirstOrDefault(c => c.Name == categoryName || categoryName == null);
            if (category == null)
            {
                throw new HttpNotFoundException();
            }
            Category = category;
            Products = category.Products;
        }
    }
}