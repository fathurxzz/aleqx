using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SiteExtensions;

namespace Ego.Models
{
    public class SiteModel:ISiteModel 
    {
        public string Title { get; set; }
        public string SeoDescription { get; set; }
        public string SeoKeywords { get; set; }
        public Menu Menu { get; set; }
        public bool IsHomePage { get; set; }
        public Content Content { get; set; }

        public IEnumerable<Product> Products { get; set; }



        public SiteModel(SiteContainer context, string contentId)
        {
            Title = HttpUtility.HtmlDecode("Я &mdash; ЭГО");


            
            Content = context.Content.FirstOrDefault(c => c.Name == contentId) ?? context.Content.First(c => c.MainPage);

            var contents = context.Content.ToList();
            Menu = new Menu();
            foreach (var content in contents)
            {
                Menu.Add(new MenuItem
                {
                    ContentId = content.Id,
                    ContentName = content.Name,
                    Current = content.Name == Content.Name,
                    Selected = false,
                    SortOrder = content.SortOrder,
                    Title = content.Title
                });
            }




            if (Content.Name == "gallery")
            {
                Products = context.Product.ToList();
            }

            SeoDescription = Content.SeoDescription;
            SeoKeywords = Content.SeoKeywords;
            IsHomePage = Content.MainPage;
        }
    }
}