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

        public FactoryCatalogueModel(Language lang, SiteContainer context, string categoryId)
            : base(lang, context, "factory")
        {
            Categories = context.Category.ToList();
            foreach (var category in Categories)
            {
                category.CurrentLang = lang.Id;
            }


            if (!string.IsNullOrEmpty(categoryId))
            {
                Category = context.Category.First(c => c.Name == categoryId);
                Category.CurrentLang = lang.Id;
            }


        }
    }
}