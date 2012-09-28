using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EM2013.Models
{
    public class CatalogueViewModel : SiteViewModel
    {
        public Category Category { get; set; }
        public Product Product { get; set; }

        public CatalogueViewModel(SiteContext context, string category, string product)
            : base(context, category == "" ? "" : null)
        {
            UpdateMainMenu(category, product);


            if(!string.IsNullOrEmpty(category))
            {
                Category = context.Category.Include("Products").First(c => c.Name == category);
                Title = Category.Title;

            }

            if(!string.IsNullOrEmpty(product))
            {
                Product = context.Product.Include("Category").Include("ProductItems").First(p => p.Name == product);
            }

        }

        private void UpdateMainMenu(string category, string product)
        {
            foreach (var item in Menu.Where(item => item.ContentName == category))
            {
                if (string.IsNullOrEmpty(product))
                    item.Current = true;
                else
                    item.Selected = true;
            }
        }
    }
}