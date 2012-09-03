using System;
using System.Collections.Generic;
using System.Configuration;
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
        public int TotalProductsCount { get; set; }

        public CatalogueViewModel(SiteContainer context, string category, int? page)
            : base(context,null)
        {
            Title = "Каталог";
            Layouts = context.Layout.Include("Parent").Include("Children").ToList();
            Makers = context.Maker.ToList();
            Brands = context.Brand.ToList();
            Products = context.Product.Include("ProductAttributes").Include("Layouts").Include("Brand").Include("Maker").ToList();
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
                foreach (var product in Products)
                {
                    product.Layouts.Load();
                }
                Title += " - " + Category.Title;
            }

            var layouts = context.Layout.Include("Parent").Include("Children").ToList();

            LayoutFilter = new LayoutFilter { Parents = layouts };

            if (Products != null)
                TotalProductsCount = Products.Count();

            Products = ApplyPaging(Products, page);

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

        public void ApplyFilers()
        {
            var result = new List<Product>();

            var layoutIds = new List<int>();
            if (WebSession.Layouts.Any())
            {
                layoutIds.AddRange(from product in Products from layout in product.Layouts from layout1 in WebSession.Layouts where layout.Id == layout1.Id select product.Id);
                result.Clear();
                result.AddRange(from id in layoutIds from product in Products where product.Id == id select product);
                Products.Clear();
                foreach (var product in result.Where(product => !Products.Contains(product)))
                {
                    Products.Add(product);
                }
            }

            var attrIds = new List<int>();
            if (WebSession.Attributes.Any())
            {
                attrIds.AddRange(from product in Products from attr in product.ProductAttributes from attribute in WebSession.Attributes where attr.Id == attribute.Id select product.Id);
                result.Clear();
                result.AddRange(from id in attrIds from product in Products where product.Id == id select product);
                Products.Clear();
                foreach (var product in result.Where(product => !Products.Contains(product)))
                {
                    Products.Add(product);
                }
                
            }

            var brandIds = new List<int>();
            if(WebSession.Brands.Any())
            {
                brandIds.AddRange(from product in Products from brand in WebSession.Brands where product.Brand.Id == brand.Id select product.Id);
                result.Clear();
                result.AddRange(from id in brandIds from product in Products where product.Id == id select product);
                Products.Clear();
                foreach (var product in result.Where(product => !Products.Contains(product)))
                {
                    Products.Add(product);
                }
                
            }

            var makerIds = new List<int>();
            if (WebSession.Makers.Any())
            {
                makerIds.AddRange(from product in Products from maker in WebSession.Makers where product.Maker.Id == maker.Id select product.Id);
                result.Clear();
                result.AddRange(from id in makerIds from product in Products where product.Id == id select product);
                foreach (var product in result.Where(product => !Products.Contains(product)))
                {
                    Products.Add(product);
                }
            }
        }

        List<Product> ApplyPaging(List<Product> products, int? page)
        {
            if (products == null)
                return null;
            int currentPage = page ?? 0;
            int pageSize = Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]);
            if (page < 0)
                return products;
            return products.Skip(currentPage * pageSize).Take(pageSize).ToList();
        }
    }
}