using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kulumu.Helpers;
using SiteExtensions;

namespace Kulumu.Models
{
    public class SiteModel : ISiteModel
    {
        public string Title { get; set; }
        public string SeoDescription { get; set; }
        public string SeoKeywords { get; set; }
        public Menu Menu { get; set; }
        public bool IsHomePage { get; set; }


        public Content Content { get; set; }
        public Article Article { get; set; }
        //public Article RandomArticle { get; set; }
        public List<Article> Articles { get; set; }

        public Category OurWorks { get; set; }

        //public Product SingleDiscountProduct { get; set; }
        //public List<Product> Products { get; set; }

        public List<Category> MainPageCategories { get; set; }
        //public IEnumerable<Category> ChildCategories { get; set; }

        public IEnumerable<Banner> Banners { get; set; }


        public SiteModel(SiteContainer context, string contentName, bool showArticles = false, bool showOurWorks = false)
        {
            Title = "Килими";
            if (contentName == null)
            {
                Content = context.Content.First(c => c.MainPage);
            }
            else
            {
                Content = context.Content.FirstOrDefault(c => c.Name == contentName);
                if (Content == null)
                {
                    throw new HttpNotFoundException();
                }
            }

            if (!string.IsNullOrEmpty(Content.Title))
                Title += " » " + Content.Title;

            SeoDescription = Content.SeoDescription;
            SeoKeywords = Content.SeoKeywords;
            if (Content.MainPage)
            {
                IsHomePage = true;
            }

            if (showArticles)
            {
                Articles = context.Article.OrderByDescending(a => a.Date).ToList();
            }

            if (showOurWorks)
            {
                OurWorks = context.Category.Include("Products").First(c => c.Name == "ourworks");
            }

            //if (context.Article.FirstOrDefault() != null)
            //{
            //    RandomArticle = context.Article.RandomElement(new Random());
            //}



            //ChildCategories = new List<Category>();
            //if (showChildCategories)
            //{
            //    if (string.IsNullOrEmpty(categoryId))
            //    {
            //        ChildCategories = Categories;
            //    }
            //    else
            //    {
            //        ChildCategories = context.Category.Include("Children").First(c => c.Name == categoryId).Children.ToList();
            //    }
            //}

            if (IsHomePage)
            {
                //MainPageCategories = context.Category.Include("Products").Where(c => c.Parent != null && !c.SpecialCategory).ToList();

                MainPageCategories = new List<Category>();
                var allCategories = context.Category.Include("Children").Where(c => c.Parent == null && !c.SpecialCategory).ToList();
                foreach (var category in allCategories)
                {
                    var cat = new Category { Name = category.Name, Title = category.Title, Id = category.Id };

                    foreach (var child in category.Children)
                    {
                        child.Products.Load();
                        foreach (var product in child.Products)
                        {
                            var p = new Product { Id = product.Id, ImageSource = product.ImageSource, Title = product.Title, Description = product.Description,Discount = product.Discount,DiscountText = product.DiscountText};
                            cat.Products.Add(p);
                        }
                    }
                    
                    MainPageCategories.Add(cat);
                }

                Banners = context.Banner.ToList();
            }

            //if (Products.Any(p => p.Discount))
            //{
            //    SingleDiscountProduct = Products.Where(p => p.Discount).RandomElement(new Random());
            //}
        }
    }
}