using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Leo.Models.Json;
using Newtonsoft.Json;

namespace Leo.Models
{
    public class CategoryModel:SiteModel
    {
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<SpecialContent> SpecialContents { get; set; }
        public string SpecialContentJson { get; set; }


        public CategoryModel(Language lang, SiteContext context, string categoryName =null, string subcategoryName=null, bool intro = false)
            : base(lang, context, categoryName)
        {
            Title = "Leo";
            
            SpecialContents = context.SpecialContents.ToList();
            foreach (var specialContent in SpecialContents)
            {
                specialContent.CurrentLang = lang.Id;
            }

            Categories = context.Categories.ToList();

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

            if (intro)
            {
                Categories = Categories.Where(c => c.Parent == null);
                return;
            }

            var currentCategory = Categories.First(c => c.Name == categoryName);

            Categories = currentCategory.Children;
        }
    }
}