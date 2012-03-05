using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop.Models
{
    public class ShopViewModel : SiteViewModel
    {
        public List<Product> Products { get; set; }
        public Product Product { get; set; }

        public ShopViewModel(ShopContainer context, string categoryId, string brandId, string tagId, string productId)
            : base(context,null)
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

                foreach (var child in category.Children.Where(child => child.Name==categoryId))
                {
                    child.Selected = true;
                    Title += " - " + child.Title;
                    SeoDescription = child.SeoDescription;
                    SeoKeywords = child.SeoKeywords;
                    category.IsParent = true;
                }
            }

            //foreach (var category in Categories.Where(category => category.Name == categoryId))
            //{
            //    category.Selected = true;
            //    Title += " - " + category.Title;
            //    SeoDescription = category.SeoDescription;
            //    SeoKeywords = category.SeoKeywords;
            //}




            if (!string.IsNullOrEmpty(categoryId))
                Products = context.Product.Include("ProductImages").Where(p => p.Category.Name == categoryId && p.Published).ToList();
            

            if (!string.IsNullOrEmpty(brandId))
            {
                Products = context.Product.Include("ProductImages").Where(p => p.Brand.Name == brandId && p.Published).ToList();
                foreach (var brand in Brands.Where(brand=>brand.Name==brandId))
                {
                    brand.Selected = true;
                    Title += " - " + brand.Title;
                }
            }

            if(!string.IsNullOrEmpty(tagId))
            {
                var tag = context.Tag.Include("Products").First(t => t.Name == tagId);
                foreach (var product in tag.Products)
                {
                    product.ProductImages.Load();
                }

                Products = tag.Products.Where(p => p.Published).ToList();
                foreach (var currentTag in Tags.Where(t=>t.Name==tagId))
                {
                    currentTag.Selected = true;
                    Title += " - " + tag.Title;
                }
            }



            if (!string.IsNullOrEmpty(productId))
            {
                var product = context.Product.Include("Brand").Include("Category").Include("ProductAttributeValues").Include("ProductImages").First(p => p.Name == productId);
                product.Tags.Load();
                product.Category.ProductAttributes.Load();
                foreach (var attribute in product.Category.ProductAttributes)
                {
                    attribute.ProductAttributeValues.Load();
                }
                Product = product;
                Title += " - " + product.Title;
                SeoDescription = product.SeoDescription;
                SeoKeywords = product.SeoKeywords;
            }

        }
    }
}