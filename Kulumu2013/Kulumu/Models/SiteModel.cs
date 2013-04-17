using System;
using System.Collections.Generic;
using System.Data;
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
        public List<Article> Articles { get; set; }
        public Category OurWorks { get; set; }
        public List<Category> MainPageCategories { get; set; }
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

            if (IsHomePage)
            {
                MainPageCategories = new List<Category>();



                var categories = new List<CategoryProductPresentation>();
                var conn = DbHelper.Connection;
                using (conn.StateManager())
                {
                    var query = @"select
	                                parent.Id as CategoryId,
	                                parent.Title as CategoryTitle, 
	                                parent.Name as CategoryName,
	                                t2.title as ProductTitle,
	                                t2.DiscountText as DiscountText,
	                                t2.Id as ProductId,
	                                t2.ImageSource as ImageSource
                                from 
	                                Category t1
                                join Product t2 on t2.CategoryId=t1.Id
                                join Category parent on parent.Id=t1.CategoryId";// where parent.ShowOnMainPage=1";

                    conn.ReadToCollection(categories, r => CategoryProductPresentation.Init(new CategoryProductPresentation(), r), query);

                    int oldCategoryId = 0;
                    int newCategoryId;
                    Category category = null;

                    foreach (var presentation in categories.OrderBy(c => c.CategoryId))
                    {
                        newCategoryId = presentation.CategoryId;
                        if (oldCategoryId != newCategoryId)
                        {
                            if (category != null)
                                MainPageCategories.Add(category);

                            category = new Category
                                         {
                                             Id = presentation.CategoryId,
                                             Title = presentation.CategoryTitle,
                                             Name = presentation.CategoryName
                                         };
                        }

                        var p = new Product
                                    {
                                        Id = presentation.ProductId,
                                        Title = presentation.ProductTitle,
                                        ImageSource = presentation.ProductImageSource,
                                        DiscountText = presentation.ProductDiscountText
                                    };
                        category.Products.Add(p);
                        oldCategoryId = newCategoryId;
                    }
                    if (category != null)
                        MainPageCategories.Add(category);
                }



                //var allCategories = context.Category.Include("Children").Include("Products").Where(c => c.ShowOnMainPage).ToList();
                //foreach (var category in allCategories.Where(c => c.Parent == null && !c.SpecialCategory))
                //{
                //    var cat = new Category { Name = category.Name, Title = category.Title, Id = category.Id };

                //    foreach (var child in category.Children)
                //    {
                //        foreach (var product in child.Products)
                //        {
                //            var p = new Product { Id = product.Id, ImageSource = product.ImageSource, Title = product.Title, Description = product.Description, Discount = product.Discount, DiscountText = product.DiscountText };
                //            cat.Products.Add(p);
                //        }
                //    }
                //    MainPageCategories.Add(cat);
                //}


                Banners = context.Banner.ToList();
            }
        }
    }
}