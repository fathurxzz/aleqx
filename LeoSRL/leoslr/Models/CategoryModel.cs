using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Leo.Models
{
    public class CategoryModel:SiteModel
    {
        public IEnumerable<Category> Categories { get; set; }

        public CategoryModel(Language lang, SiteContext context, string contentId) : base(lang, context, contentId)
        {
            Categories = context.Categories.ToList();
            foreach (var category in Categories)
            {
                category.CurrentLang = lang.Id;
            }
        }
    }
}