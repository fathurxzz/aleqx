using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vip.Models
{
    public class CatalogueViewModel : SiteViewModel
    {
        public Category Category { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public AttributeFilter AttributesFilter { get; set; }
        public LayoutFilter LayoutFilter { get; set; }

        public CatalogueViewModel(SiteContainer context, string category)
            : base(context)
        {

            

            if (string.IsNullOrEmpty(category))
            {
                Categories = context.Category.Include("Products").ToList();
            }
            else
            {
                Category = context.Category.First(c => c.Name == category);
                var filter = new AttributeFilter { CurrentCategoryTitle = Category.Title };
                filter.Add(new FilterItem { Title = "Показывать категории", Attributes = new List<ProductAttribute> { new ProductAttribute { Title = "Корпусная" }, new ProductAttribute { Title = "Мягкая" } } });
                filter.Add(new FilterItem { Title = "Показывать бренды" });
                filter.Add(new FilterItem { Title = "Страна производитель", Attributes = new List<ProductAttribute> { new ProductAttribute { Title = "Италия" }, new ProductAttribute { Title = "Германия" } } });
                AttributesFilter = filter;
            }

            var layouts = context.Layout.Include("Parent").Include("Children").ToList();



            LayoutFilter = new LayoutFilter { Parents = layouts};
            
        }
    }
}