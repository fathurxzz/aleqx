using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Listelli.Models
{
    public class FactoryCatalogueModel:SiteModel
    {
        public IEnumerable<Category> Categories { get; set; }
        public Category Category { get; set; }
        public CategoryBrand Brand { get; set; }

        public FactoryCatalogueModel(Language lang, SiteContainer context, string categoryId, string brandId)
            : base(lang, context, "factory")
        {
            Categories = context.Category.ToList();
            foreach (var category in Categories)
            {
                category.CurrentLang = lang.Id;
            }


            if (!string.IsNullOrEmpty(categoryId))
            {
                Category = context.Category.Include("CategoryBrands").First(c => c.Name == categoryId);
                Category.CurrentLang = lang.Id;

                if (!string.IsNullOrEmpty(brandId))
                {
                    Brand = Category.CategoryBrands.First(c => c.Name == brandId);
                    Brand.CategoryBrandItems.Load();
                    foreach (var item in Brand.CategoryBrandItems)
                    {
                        item.CurrentLang = lang.Id;
                    }
                }

            }




        }
    }
}