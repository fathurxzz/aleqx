using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Leo.Models.Json;
using Newtonsoft.Json;
using SiteExtensions;

namespace Leo.Models
{
    public class CategoryModel:SiteModel
    {
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<SpecialContent> SpecialContents { get; set; }
        public string SpecialContentJson { get; set; }
        //private IEnumerable<MenuItem> UnsortedMenu { get; set; }
        public List<MenuItem> SiteMenu { get; set; }


        public CategoryModel(Language lang, SiteContext context, string categoryName =null, string subcategoryName=null, bool intro = false)
            : base(lang, context, categoryName)
        {
            Title = "Leo";
            //SiteMenu = new List<MenuItem>();
            SpecialContents = context.SpecialContents.ToList();
            foreach (var specialContent in SpecialContents)
            {
                specialContent.CurrentLang = lang.Id;
            }

            Categories = context.Categories.ToList();

            //UnsortedMenu = CreateMenu(Categories);
           

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

            SpecialContentJson = "settings.specialContent = "+ JsonConvert.SerializeObject(specialContentJsonModel);


            foreach (var category in Categories)
            {
                category.CurrentLang = lang.Id;
            }
            
             //ApplySorting(UnsortedMenu);

            //foreach (var item in SiteMenu)
            //{
            //    if (item.ContentName == categoryName || item.ContentName == subcategoryName)
            //    {
            //        item.Current = true;
            //    }
            //}

            if (intro)
            {
                Categories = Categories.Where(c => c.Parent == null);
                return;
            }

            var currentCategory = Categories.First(c => c.Name == categoryName);

            Categories = currentCategory.Children;

            //var menu = SiteMenu.First(c => c.ContentName == categoryName);
            //SiteMenu = menu.Children;
        }


        private void ApplySorting(IEnumerable<MenuItem> source)
        {
            foreach (var item in source.Where(c => c.Parent == null).OrderBy(c => c.ContentId))
            {
                Visit(item);
            }

        }

        private void Visit(MenuItem node)
        {
            //SiteMenu.Add(node);
            if (node.Children == null || node.Children.Count == 0)
            {
                return;
            }
            foreach (var child in node.Children)
            {
                Visit(child);
            }
        }

        private static IEnumerable<MenuItem> CreateMenu(IEnumerable<Category> categories)
        {
            return categories.Select(category => new MenuItem
            {
                Title = category.Title,
                ContentId = category.Id,
                ContentName = category.Name,
                SortOrder = category.SortOrder
            });
        }
    }
}