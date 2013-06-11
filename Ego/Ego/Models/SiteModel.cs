using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SiteExtensions;

namespace Ego.Models
{
    public class SiteModel : ISiteModel
    {
        public string Title { get; set; }
        public string SeoDescription { get; set; }
        public string SeoKeywords { get; set; }
        public Menu Menu { get; set; }
        public bool IsHomePage { get; set; }
        public Content Content { get; set; }

        public IEnumerable<Product> Products { get; set; }
        public int TotalProductsCount { get; set; }


        public SiteModel(SiteContainer context, string contentId, int? page)
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
                IQueryable<Product> products = context.Product;
                TotalProductsCount = products.Count();
                products = ApplyOrdering(products, "asc");
                products = ApplyPaging(products, page);
                Products = products.ToList();

            }

            SeoDescription = Content.SeoDescription;
            SeoKeywords = Content.SeoKeywords;
            IsHomePage = Content.MainPage;
        }

        IQueryable<Product> ApplyOrdering(IQueryable<Product> products, string direction)
        {
            if (products == null)
                return null;
            
            return direction == "desc" ? products.OrderByDescending(p => p.Id) : products.OrderBy(p => p.Id);
        }

        IQueryable<Product> ApplyPaging(IQueryable<Product> products, int? page)
        {
            if (products == null)
                return null;
            int currentPage = page ?? 0;
            int pageSize = SiteSettings.PageSize;
            if (page < 0)
                return products;
            return products.Skip(currentPage * pageSize).Take(pageSize);
        }
    }
}