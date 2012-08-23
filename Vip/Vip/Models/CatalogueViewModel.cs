using System.Collections.Generic;
using System.Linq;
using Vip.Helpers;

namespace Vip.Models
{
    public class CatalogueViewModel : SiteViewModel
    {
        public Category Category { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public AttributeFilter AttributesFilter { get; set; }
        public LayoutFilter LayoutFilter { get; set; }
        public new IEnumerable<Layout> Layouts { get; set; }
        public List<Brand> Brands { get; set; }
        public List<Maker> Makers { get; set; }
        public List<ProductAttribute> Attributes { get; set; }
        public List<Product> Products { get; set; }

        public CatalogueViewModel(SiteContainer context, string category)
            : base(context,null)
        {
            Title = "Каталог";
            Layouts = context.Layout.Include("Parent").Include("Children").ToList();
            Makers = context.Maker.ToList();
            Brands = context.Brand.ToList();

            if (string.IsNullOrEmpty(category))
            {
                Categories = context.Category.Include("Products").ToList();
                Title += " - Разделы";
            }
            else
            {
                Category = context.Category.Include("ProductAttributes").Include("Products").First(c => c.Name == category);
                Attributes = Category.ProductAttributes.ToList();
                Products = Category.Products.ToList();
                Title += " - " + Category.Title;
            }

            var layouts = context.Layout.Include("Parent").Include("Children").ToList();

            LayoutFilter = new LayoutFilter { Parents = layouts };

        }

        public void SetFilters()
        {
            if (Category == null)
                return;

            var filter = new AttributeFilter { CurrentCategoryTitle = Category.Title };

            var attributes = (from productAttribute in Attributes from attribute in WebSession.Attributes.Where(attribute => attribute.Id == productAttribute.Id) select productAttribute).ToList();
            var filterAttributes = attributes.Select(a => new FilterAttribute { Id = a.Id, Title = a.Title }).ToList();
            filter.Add(new FilterItem { Selector = "attribute", Title = "Показывать категории", Attributes = filterAttributes });

            var brands = (from br in Brands from attribute in WebSession.Brands.Where(attribute => attribute.Id == br.Id) select br).ToList();
            var filterAttributesBrand = brands.Select(a => new FilterAttribute { Id = a.Id, Title = a.Title }).ToList();
            filter.Add(new FilterItem { Selector = "brand", Title = "Показывать бренды", Attributes = filterAttributesBrand });

            var makers = (from br in Makers from attribute in WebSession.Makers.Where(attribute => attribute.Id == br.Id) select br).ToList();
            var filterAttributesMaker = makers.Select(a => new FilterAttribute { Id = a.Id, Title = a.Title }).ToList();
            filter.Add(new FilterItem { Selector = "maker", Title = "Страна производитель", Attributes = filterAttributesMaker });

            AttributesFilter = filter;
        }
    }
}