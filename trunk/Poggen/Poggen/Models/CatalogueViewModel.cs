using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Poggen.Models
{
    public class CategoriesSelectorData
    {
        public List<SelectListItem> Categories { get; set; }
        public List<SelectListItem> SubCategories { get; set; }
    }

    public class CatalogueViewModel : SiteViewModel
    {
        public Category Category { get; set; }
        public CategoriesSelectorData CategoriesSelectorData { get; private set; }

        private readonly List<SelectListItem> _categories = new List<SelectListItem> { new SelectListItem { Text = "Выберите категорию...", Value = "" } };
        private readonly List<SelectListItem> _subcategories = new List<SelectListItem> { new SelectListItem { Text = "Выберите тип...", Value = "" } };


        public CatalogueViewModel(SiteContainer context, string categoryName, string subcategoryName)
            : base(context, null)
        {
            var categories = context.Category.Include("Children").Where(c => c.Parent == null).ToList();
            var subcategories = context.Category.Where(c => c.Name == categoryName).SelectMany(c => c.Children).ToList();
            _categories.AddRange(categories.Where(c=> !c.MainPage).Select(c => new SelectListItem { Text = c.Title, Value = c.Name, Selected = c.Name == categoryName }));
            _subcategories.AddRange(subcategories.Select(c => new SelectListItem { Text = c.Title, Value = c.Name, Selected = c.Name == subcategoryName }));
            CategoriesSelectorData = new CategoriesSelectorData { Categories = _categories, SubCategories = _subcategories };

            var category = categories.FirstOrDefault(c => c.Name == categoryName) ?? categories.First(c => c.MainPage);
            var subcategory = category.Children.FirstOrDefault(c => c.Name == subcategoryName);

            Title = category.Title;
            if (subcategory != null)
                Title += " > " + subcategory.Title;
            Category = subcategory ?? category;
            SeoDescription = Category.SeoDescription;
            SeoKeywords = Category.SeoKeywords;
        }
    }
}