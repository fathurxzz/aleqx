﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SiteExtensions;

namespace Kulumu.Models
{
    public class GalleryModel : SiteModel
    {
        public Category Category { get; set; }
        public Category ParentCategory { get; set; }
        public List<Category> Categories { get; set; }
        public Product Product { get; set; }
        private readonly SiteContainer _context;


        private string GetFirstCategory(string categoryId)
        {
            Category category;

            if (!string.IsNullOrEmpty(categoryId))
            {
                category = _context.Category.Include("Children").FirstOrDefault(c => c.Name == categoryId);
                if (category == null)
                {
                    throw new HttpNotFoundException();
                }
            }
            else
            {
                category = _context.Category.FirstOrDefault(c=>!c.SpecialCategory);
                if (category != null)
                {
                    var catId = category.Id;
                    category = _context.Category.Include("Children").First(c => c.Id == catId);
                }
            }

            if (category != null)
            {
                if (category.Children.Any())
                {
                    var childId = category.Children.First().Id;
                    category = _context.Category.First(c => c.Id == childId);
                }
                categoryId = category.Name;
            }
            return categoryId;
        }

        public GalleryModel(SiteContainer context, string categoryId, int? productId = null, int? productImageId = null)
            : base(context, "gallery", false)
        {
            _context = context;

            if (productId.HasValue)
            {
                Product = _context.Product.Include("Category").Include("ProductSizes").Include("ProductImages").First(p => p.Id == productId);

                if (productImageId.HasValue)
                {
                    foreach (var productImage in Product.ProductImages.Where(productImage => productImage.Id == productImageId))
                    {
                        productImage.Selected = true;
                    }
                }

                if (categoryId == null)
                {
                    categoryId = Product.Category.Name;
                }

                PageTitle = Product.PageTitle;
            }


            //if (productId.HasValue)
            //{
            //    var product = context.Product.Include("Category").First(p => p.Id == productId);
            //    categoryId = product.Category.Name;
            //}

            categoryId = GetFirstCategory(categoryId);

            if (!string.IsNullOrEmpty(categoryId))
            {
                Category =
                    _context.Category.Include("Parent")
                            .Include("Products")
                            //.Include("ProductSizes")
                            .First(c => c.Name == categoryId);

                ParentCategory = Category.Parent ?? Category;
            }



            Categories = _context.Category.Include("Children").Where(c => c.Parent == null&& !c.SpecialCategory).ToList();

            
            foreach (var category in Categories)
            {
                if (category.Name == categoryId)
                {
                    category.Selected = true;
                    Title += " - " + category.Title;
                    SeoDescription = category.SeoDescription;
                    SeoKeywords = category.SeoKeywords;
                    
                    PageTitle = category.PageTitle;
                }

                foreach (var child in category.Children.Where(child => child.Name == categoryId))
                {
                    child.Selected = true;
                    Title += " - " + category.Title + " - " + child.Title;
                    SeoDescription = child.SeoDescription;
                    SeoKeywords = child.SeoKeywords;
                    category.IsParent = true;
                    PageTitle = child.PageTitle;
                }
            }



        }
    }
}







