using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Leo.Helpers;
using SiteExtensions;

namespace Leo.Models
{
    public class CatalogueViewModel : SiteViewModel
    {
        public Category Category { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public List<ProductAttribute> Attributes { get; set; }

        public CatalogueViewModel(SiteContainer context, string categoryName)
            : base(context, categoryName)
        {
            // TODO: сделать дефолтный пункт меню
            if (categoryName == null)
                categoryName = "sceneries";
            //
            var category = context.Category.Include("ProductAttributes").Include("Products").FirstOrDefault(c => c.Name == categoryName || categoryName == null);
            if (category == null)
            {
                throw new HttpNotFoundException("page " + categoryName);
            }

            if (categoryName == null)
                foreach (var item in Menu.Where(c => c.ContentId == category.Id))
                {
                    item.Current = true;
                }

            Title = category.Title;
            Category = category;
            Products = category.Products;
            Attributes = category.ProductAttributes != null ? category.ProductAttributes.ToList() : new List<ProductAttribute>();
        }

        public void ApplyFilterForProducts(List<ProductAttribute> attributes)
        {
            if (!attributes.Any())
                return;
            List<Product> result = Products.Where(product => product.ProductAttributes.Intersect(attributes).Any()).ToList();
            Products = result;
        }
    }
}