using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Diagnostics;
using System.Linq;
using System.Web;
using log4net;
using log4net.Config;
using Shop.DataAccess.Entities;
using Shop.DataAccess.Repositories;
using Shop.WebSite.Helpers;

namespace Shop.WebSite.Models
{
    public class SearchModel : SiteModel
    {


        public IEnumerable<Product> Products { get; set; }
        public int ProductTotalCount { get; set; }
        public int Page { get; set; }

        private IShopRepository _repository { get; set; }

        public SearchModel(IShopRepository repository, int langId, int? page, string categoryName = null, string productName = null, string articleName = null, string filter = null, string query = null, string sortOrder = null, string sortBy = null, bool showSpecialOffers = false)
            : base(repository, langId, "category", showSpecialOffers)
        {


            _repository = repository;

            IQueryable<Product> products = null;

            if (query != null && query.Length > 1)
            {
                products = _repository.GetProductsByQueryString(query).Where(p => p.ProductStocks.Any(ps => ps.IsAvailable));
            }
            else
            {
                products = _repository.GetProductsByCategory("");
            }

            switch (sortOrder)
            {
                case "desc":
                    products = products.OrderByDescending(p => p.Price).ThenBy(p => p.Name).AsQueryable();
                    break;
                case "asc":
                    products = products.OrderBy(p => p.Price).ThenBy(p => p.Name).AsQueryable();
                    break;
                default: // "abc"
                    products = products.OrderBy(p => p.Name).AsQueryable();
                    break;
            }


            var pageSize = int.Parse(SiteSettings.GetShopSetting("ProductsPageSize"));
            Products = products.Include(x => x.ProductAttributeValues)
                .Include(x => x.ProductImages)
                .ToList();

            ProductTotalCount = Products.Count();

            if (page > Products.Count() / pageSize)
            {
                page = 0;
            }

            Products = ApplyPaging(Products.AsQueryable(), page, pageSize).ToList();

            foreach (var product in Products)
            {
                product.CurrentLang = langId;
                if (product.ProductImages.Any())
                {
                    var pi = product.ProductImages.FirstOrDefault(c => c.IsDefault) ?? product.ProductImages.First();
                    product.ImageSource = pi.ImageSource;
                }
            }

            _sw.Stop();

            Log.DebugFormat("SiteModel+SearchModel: {0}", _sw.Elapsed);
        }

        IEnumerable<Product> ApplyPaging(IEnumerable<Product> products, int? page, int pageSize)
        {
            if (products == null)
                return null;
            int currentPage = page ?? 0;
            Page = currentPage;
            if (page < 0)
                return products;
            return products.Skip(currentPage * pageSize).Take(pageSize);
        }
    }
}