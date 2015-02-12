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
    public class CatalogueModel : SiteModel
    {


        public IEnumerable<ProductAttribute> ProductAttributes { get; set; }
        public IEnumerable<Product> Products { get; set; }
        //public Product Product { get; set; }

        //public string CurrentFilter { get; set; }
        //public string[] FilterArray { get; set; }
        public int ProductTotalCount { get; set; }
        public Category CurrentCategory { get; set; }
        //public List<Product> FilteredProducts { get; set; }
        public int Page { get; set; }
        public bool IsFiltered { get; set; }

        public List<FilterViewModel> Filters { get; set; }

        private IShopRepository _repository { get; set; }

        private static Dictionary<int, List<int>> GroupProductAttributes(IEnumerable<ProductAttribute> productAttributes, IEnumerable<int> filters)
        {
            var result = new Dictionary<int, List<int>>();

            foreach (var productAttribute in productAttributes)
            {
                foreach (var productAttributeValue in productAttribute.ProductAttributeValues.Where(pav => filters.Contains(pav.Id)))
                {
                    if (result.ContainsKey(productAttribute.Id))
                    {
                        var tmp = result[productAttribute.Id];
                        tmp.Add(productAttributeValue.Id);
                        result[productAttribute.Id] = tmp;
                    }
                    else
                    {
                        result.Add(productAttribute.Id, new List<int> { productAttributeValue.Id });
                    }
                }
            }
            return result;
        }


        private static IQueryable<Product> OrderProducts(IQueryable<Product> products, string sortOrder)
        {
            switch (sortOrder)
            {
                case "desc":
                    return products.OrderByDescending(p => p.Price).ThenBy(p => p.Name).AsQueryable();
                case "asc":
                    return products.OrderBy(p => p.Price).ThenBy(p => p.Name).AsQueryable();
                default: // "abc"
                    return products.OrderBy(p => p.Name).AsQueryable();
            }
        }



        public CatalogueModel(IShopRepository repository, int langId, int? page, string categoryName = null, string filter = null, string sortOrder = null, bool showSpecialOffers = false)
            : base(repository, langId, "category", showSpecialOffers)
        {

            _repository = repository;
            var category = Categories.FirstOrDefault(c => c.Name == categoryName);
            if (category != null)
            {
                ProductAttributes = _repository.GetProductAttributes(category.Id).Where(pa => pa.IsFilterable).ToList();

                filter = filter ?? string.Empty;
                var filters =
                    filter.Split(new[] {"-"}, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                Dictionary<int, List<int>> groupedAttributes = GroupProductAttributes(ProductAttributes, filters);
                IQueryable<Product> products =
                    _repository.GetProductsByCategory(categoryName)
                        .Where(p => p.ProductStocks.Any(ps => ps.IsAvailable));


                if (filters.Any())
                {
                    products = products.Where(x => x.ProductAttributeValues.Any(pav => filters.Contains(pav.Id)));
                    IsFiltered = true;
                }

                products = OrderProducts(products, sortOrder);

                var pageSize = int.Parse(SiteSettings.GetShopSetting("ProductsPageSize"));

                Products = products.Include(x => x.ProductAttributeValues)
                    .Include(x => x.ProductImages)
                    .ToList();

                if (filters.Any())
                {
                    Products =
                        Products.Where(
                            product =>
                                groupedAttributes.Count(
                                    groupedAttribute =>
                                        product.ProductAttributeValues.Any(
                                            pav => groupedAttribute.Value.Contains(pav.Id))) == groupedAttributes.Count)
                            .ToList();
                }

                ProductTotalCount = Products.Count();

                if (page > Products.Count()/pageSize)
                {
                    page = 0;
                }

                foreach (var productAttribute in ProductAttributes)
                {
                    foreach (var productAttributeValue in productAttribute.ProductAttributeValues)
                    {
                        productAttributeValue.AvailableProductsCount =
                            Products.Count(p => p.ProductAttributeValues.Any(pav => pav.Id == productAttributeValue.Id));
                    }
                }

                // создание фильтров
                Filters = new List<FilterViewModel>();

                //var filterValueGroups = GroupFilterString(categoryName, CurrentFilter);

                foreach (var productAttribute in ProductAttributes.OrderBy(p => p.SortOrder))
                {
                    if (groupedAttributes.ContainsKey(productAttribute.Id) ||
                        productAttribute.ProductAttributeValues.Any(pav => pav.AvailableProductsCount > 0))
                    {
                        var fvm = new FilterViewModel
                        {
                            Title = productAttribute.Title,
                            FilterItems = new List<FilterItem>()
                        };
                        foreach (var categoryValue in productAttribute.ProductAttributeValues.OrderBy(a => a.Title))
                        {
                            if (groupedAttributes.ContainsKey(productAttribute.Id) ||
                                categoryValue.AvailableProductsCount > 0)
                            {
                                var filterItem = new FilterItem
                                {
                                    Title = categoryValue.Title,
                                    AvaibleProductsCount = categoryValue.AvailableProductsCount,
                                    AvaibleProductsCountAfterApplyingFilter =
                                        categoryValue.AvailableProductsCountAfterApplyingFilter,
                                    Selected = filters.Contains(categoryValue.Id),
                                    FilterAttributeString =
                                        CatalogueFilterHelper.GetFilterStringForCheckbox(filters, categoryValue.Id,
                                            filters.Contains(categoryValue.Id)),
                                    Id = "cb_" + categoryValue.Id
                                };

                                fvm.FilterItems.Add(filterItem);
                            }
                        }
                        Filters.Add(fvm);
                    }
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

                CurrentCategory = category;
                _sw.Stop();
                Log.DebugFormat("SiteModel+CatalogueModel: {0}", _sw.Elapsed);
            }
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