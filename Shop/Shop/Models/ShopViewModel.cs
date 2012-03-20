using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Web;

namespace Shop.Models
{
    public class ShopViewModel : SiteViewModel
    {
        public List<Product> Products { get; set; }
        public Product Product { get; set; }
        public IEnumerable<Category> ChildCategories { get; set; }
        public int TotalProductsCount { get; set; }
        public int Page { get; set; }

        public ShopViewModel(ShopContainer context, string categoryId, string brandId, string tagId, string productId, int? page, bool showChildCategories = false, string searchQuery = null)
            : base(context, null, false)
        {
            foreach (var category in Categories)
            {
                if (category.Name == categoryId)
                {
                    category.Selected = true;
                    Title += " - " + category.Title;
                    SeoDescription = category.SeoDescription;
                    SeoKeywords = category.SeoKeywords;
                }

                foreach (var child in category.Children.Where(child => child.Name == categoryId))
                {
                    child.Selected = true;
                    Title += " - " + child.Title;
                    SeoDescription = child.SeoDescription;
                    SeoKeywords = child.SeoKeywords;
                    category.IsParent = true;
                }
            }
            ChildCategories = new List<Category>();
            if (showChildCategories)
            {
                if (string.IsNullOrEmpty(categoryId))
                {
                    ChildCategories = Categories;
                }
                else
                {
                    ChildCategories = context.Category.Include("Children").Where(c => c.Name == categoryId).First().Children.ToList();
                }
            }

            IQueryable<Product> products = null;
            if (!string.IsNullOrEmpty(categoryId))
            {
                products = context.Product.Include("ProductImages").Where(p => p.Category.Name == categoryId && p.Published);
            }

            if (!string.IsNullOrEmpty(brandId))
            {
                products = context.Product.Include("ProductImages").Where(p => p.Brand.Name == brandId && p.Published);
                foreach (var brand in Brands.Where(brand => brand.Name == brandId))
                {
                    brand.Selected = true;
                    Title += " - " + brand.Title;
                }
            }

            if (!string.IsNullOrEmpty(tagId))
            {
                var tag = context.Tag.Include("Products").First(t => t.Name == tagId);
                foreach (var product in tag.Products)
                {
                    product.ProductImages.Load();
                }

                products = tag.Products.Where(p => p.Published).ToList().AsQueryable();
                foreach (var currentTag in Tags.Where(t => t.Name == tagId))
                {
                    currentTag.Selected = true;
                    Title += " - " + tag.Title;
                }
            }
            if (products != null)
                TotalProductsCount = products.Count();

            products = ApplyOrdering(products, "");
            products = ApplyPaging(products, page);

            if (products == null)
                Products = new List<Product>();
            else
                Products = products.ToList();

            if (!string.IsNullOrEmpty(productId))
            {

                var product = context.Product
                    .Include("Brand")
                    .Include("Category")
                    //.Include("ProductImages")
                    //.Include("ProductAttributeValues")
                    //.Include("ProductAttributeStaticValues")
                    .First(p => p.Name == productId);

                product.ProductImages.Load();

                product.Tags.Load();
                product.ProductAttributeValues.Load();
                product.ProductAttributeStaticValues.Load();
                product.Category.ProductAttributes.Load();
                foreach (var attribute in product.Category.ProductAttributes)
                {
                    attribute.ProductAttributeValues.Load();
                    attribute.ProductAttributeStaticValues.Load();
                }
                Product = product;
                Title += " - " + product.Title;
                SeoDescription = product.SeoDescription;
                SeoKeywords = product.SeoKeywords;
            }




        }

        IQueryable<Product> ApplyOrdering(IQueryable<Product> products, string orderBy)
        {
            if (products == null)
                return null;
            switch (orderBy)
            {
                case "name":
                    return products.OrderBy(p => p.Name);
                case "price":
                    return products.OrderBy(p => p.Price);
                default:
                    return products.OrderBy(p => p.Id);
            }
        }

        IQueryable<Product> ApplyPaging(IQueryable<Product> products, int? page)
        {
            if (products == null)
                return null;
            int currentPage = page ?? 0;
            int pageSize = 10;
            if (page < 0)
                return products;
            return products.Skip(currentPage * pageSize).Take(pageSize);
        }
    }


}