using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Leo.Models.Json;
using Newtonsoft.Json;
using SiteExtensions;

namespace Leo.Models
{
    public class CategoryModel : SiteModel
    {
        public IEnumerable<Category> Categories { get; set; }
        public Category Category { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public Product Product { get; set; }
        public IEnumerable<SpecialContent> SpecialContents { get; set; }
        public string SpecialContentJson { get; set; }
        public Article Article { get; set; }

        public CategoryModel(Language lang, SiteContext context, string categoryName = null, string subcategoryName = null, string productName=null, int? articleId=null, bool intro = false)
            : base(lang, context, categoryName)
        {
            
            if (subcategoryName == null)
            {
                SpecialContents = _context.SpecialContents.ToList();
                foreach (var specialContent in SpecialContents)
                {
                    specialContent.CurrentLang = lang.Id;
                }

                var specialContentJsonModel = new SpecialContentJsonModel()
                {
                    imagePath = "/content/images/",
                    items = new List<item>()
                };

                foreach (var content in SpecialContents)
                {
                    specialContentJsonModel.items.Add(new item
                    {
                        contentImageSource = content.ContentImageSource,
                        pageImageSource = content.PageImageSource,
                        title = content.Title,
                        text = content.Text
                    });
                }

                SpecialContentJson = "settings.specialContent = " + JsonConvert.SerializeObject(specialContentJsonModel);
            }


            Categories = _context.Categories.ToList();
            
            var currentCategoryName = subcategoryName ?? categoryName;
            Category = Categories.FirstOrDefault(c => c.Name == currentCategoryName);

            if (Category != null)
            {
                foreach (var article in Category.Articles)
                {
                    article.CurrentLang = lang.Id;
                }

                if (articleId.HasValue)
                {
                    Article = Category.Articles.First(a => a.Id == articleId);
                    Article.CurrentLang = lang.Id;
                }

                Products = Category.Products.ToList();
                foreach (var product in Products)
                {
                    product.CurrentLang = lang.Id;
                }
                
                if (productName != null)
                {
                    Product = Category.Products.First(p => p.Name == productName);
                }
                else if (subcategoryName != null && Products.Any())
                {
                    Product = Category.Products.First();
                }
            }
            
            foreach (var category in Categories)
            {
                category.CurrentLang = lang.Id;
            }

            SiteMenu = CreateSiteMenu(Categories);

            if (categoryName != null)
            {
                var menu = SiteMenu.First(m => m.ContentName == categoryName);
                SiteMenu = menu.Children.ToList();
            }

            foreach (var item in SiteMenu.Where(item => item.ContentName == categoryName || item.ContentName == subcategoryName))
            {
                item.Current = true;
            }

            if (intro)
            {
                Categories = Categories.Where(c => c.Parent == null);
                return;
            }

            var currentCategory = Categories.First(c => c.Name == categoryName);
            Categories = currentCategory.Children;


            if (Product != null && Product.ProductImages.Any())
            {
                var specialContentJsonModel = new SpecialContentJsonModel()
                {
                    imagePath = "/content/images/",
                    items = new List<item>()
                };

                foreach (var img in Product.ProductImages)
                {
                    specialContentJsonModel.items.Add(new item
                    {
                        contentImageSource = null,
                        pageImageSource = img.ImageSource
                    });
                }

                SpecialContentJson = "settings.specialContent = " + JsonConvert.SerializeObject(specialContentJsonModel);
            }




        }

        private static List<MenuItem> CreateSiteMenu(IEnumerable<Category> categories)
        {
            var result = new List<MenuItem>();
            foreach (var category in categories.Where(c => c.Parent == null).OrderBy(c => c.Id))
            {
                var parent = CreateMenuItem(category,null, 0);
                Visit(category, parent, 0);
                result.Add(parent);
            }
            return result;
        }

        private static void Visit(Category node, MenuItem parent, int level)
        {
            if (node.Children == null || node.Children.Count == 0)
            {
                return;
            }
            foreach (var item in node.Children)
            {
                var child = CreateMenuItem(item,parent, level+1);
                parent.Children.Add(child);
                Visit(item, child, level + 1);
            }
        }

        private static MenuItem CreateMenuItem(Category category, MenuItem parent, int level)
        {
            return new MenuItem
            {
                Title = category.Title,
                ContentId = category.Id,
                ContentName = category.Name,
                SortOrder = category.SortOrder,
                Level = level,
                Parent = parent,
                Children = new List<MenuItem>()
            };
        }

       
    }
}