using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Poggen.Models
{
    public class CatalogueViewModel:SiteViewModel
    {
        public IEnumerable<Category> Categories { get; set; }
        public Category Category { get; set; }

        public CatalogueViewModel(SiteContainer context, string categoryName, string subcategoryName) : base(context, null)
        {
            Categories = context.Category.Include("Children").Where(c => c.Parent == null).ToList();
            Category = context.Category.Include("Children").FirstOrDefault(c => c.Name == categoryName);
        }
    }
}