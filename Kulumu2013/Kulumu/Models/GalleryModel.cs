using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kulumu.Models
{
    public class GalleryModel:SiteModel
    {
        public Category Category { get; set; }
        public List<Category> Categories { get; set; }
        public GalleryModel(SiteContainer context, string categoryId, int productId) : base(context, "gallery", false)
        {
            Categories = context.Category.Include("Children").Where(c => c.Parent == null).ToList();


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
                    Title += " - " + category.Title + " - " + child.Title;
                    SeoDescription = child.SeoDescription;
                    SeoKeywords = child.SeoKeywords;
                    category.IsParent = true;
                }
            }


            //Category = !string.IsNullOrEmpty(categoryId)
            //               ? context.Category.Include("Products").First(c => c.Name == categoryId)
            //               : context.Category.Include("Products").FirstOrDefault();

            if (!string.IsNullOrEmpty(categoryId))
            {
                Category = context.Category.Include("ProductSizes").Include("Products").First(c => c.Name == categoryId);
            }
            else
            {
                //Category = context.Category.Include("Products").First();

                var cat = context.Category.FirstOrDefault();
                if (cat != null)
                {
                    var catId = cat.Id;
                    Category = context.Category.Include("ProductSizes").Include("Products").First(c => c.Id == catId);
                }
            }
        }
    }
}







