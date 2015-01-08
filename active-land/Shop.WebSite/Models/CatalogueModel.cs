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
        public Product Product { get; set; }

        public string CurrentFilter { get; set; }
        public string[] FilterArray { get; set; }
        public int ProductTotalCount { get; set; }
        public Category CurrentCategory { get; set; }
        public List<Product> FilteredProducts { get; set; }
        public int Page { get; set; }
        public bool IsFiltered { get; set; }

        public List<FilterViewModel> Filters { get; set; }

        private IShopRepository _repository { get; set; }

        private Dictionary<string, List<string>> GroupFilterString(string categoryName, string filterString)
        {
            var result = new Dictionary<string, List<string>>();
            if (!string.IsNullOrEmpty(filterString) && !string.IsNullOrEmpty(categoryName))
            {

                string[] filterArray = FilterArray = filterString.Split(new[] { "-" }, StringSplitOptions.RemoveEmptyEntries);

                IsFiltered = FilterArray.Any();

                var attributes = _repository.GetProductAttributes(categoryName);
                foreach (var attribute in attributes)
                {
                    foreach (var value in attribute.ProductAttributeValues)
                    {
                        if (filterArray.Contains(value.Id.ToString()))
                        {
                            if (result.ContainsKey(attribute.Id.ToString()))
                            {
                                var tmp = result[attribute.Id.ToString()];
                                tmp.Add(value.Id.ToString());
                                result[attribute.Id.ToString()] = tmp;
                            }
                            else
                            {
                                result.Add(attribute.Id.ToString(), new List<string> { value.Id.ToString() });
                            }
                        }
                    }
                }
            }
            return result;
        }


        private Dictionary<int, List<int>> GroupProductAttributes(IEnumerable<ProductAttribute> productAttributes, IEnumerable<int> filters)
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
                        result.Add(productAttribute.Id, new List<int> {productAttributeValue.Id});
                    }
                }
            }
            return result;
        }


        private IQueryable<Product> OederProducts(IQueryable<Product> products, string sortOrder)
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
            FilterArray = new string[0];

            var category = Categories.First(c => c.Name == categoryName);
            ProductAttributes = _repository.GetProductAttributes(category.Id).Where(pa => pa.IsFilterable).ToList();

            CurrentFilter = filter ?? string.Empty;

            //var filterValueGroups = GroupFilterString(categoryName, filter);
            var filters = CurrentFilter.Split(new[] { "-" }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse);

            IQueryable<Product> products = _repository.GetProductsByCategory(categoryName).Where(p => p.IsActive);

            Dictionary<int, List<int>> groupedAttributes = GroupProductAttributes(ProductAttributes, filters);

            if (filters.Any())
                products = products.Where(x => x.ProductAttributeValues.Any(pav => filters.Contains(pav.Id)));

            products = OederProducts(products, sortOrder);

            var pageSize = int.Parse(SiteSettings.GetShopSetting("ProductsPageSize"));
            
            Products = products.Include(x => x.ProductAttributeValues)
                .Include(x => x.ProductImages)
                .ToList();

            ProductTotalCount = Products.Count();

            if (page > Products.Count() / pageSize)
            {
                page = 0;
            }

            //foreach (var product in Products)
            //{
            //    product.IsSelectedByFilter = true;
            //}

            foreach (var productAttribute in ProductAttributes)
            {
                foreach (var productAttributeValue in productAttribute.ProductAttributeValues)
                {
                    productAttributeValue.AvailableProductsCount = Products.Count(p => p.ProductAttributeValues.Any(pav => pav.Id == productAttributeValue.Id));
                    //productAttributeValue.AvailableProductsCountAfterApplyingFilter = AllProducts.Count(p => p.ProductAttributeValues.Any(pav => pav.Id == productAttributeValue.Id));
            
                    //foreach (var product in Products.Where(p => p.IsSelectedByFilter))
                    //{
                    //    if (filters.Contains(productAttributeValue.Id))
                    //        product.IsSelectedByFilter = product.ProductAttributeValues.Any(pav => pav.Id == productAttributeValue.Id);
                    //}
                }
            }

            //Products = Products.Where(p => p.IsSelectedByFilter);

            // создание фильтров
            Filters = new List<FilterViewModel>();

            var filterValueGroups = GroupFilterString(categoryName, CurrentFilter);

            foreach (var productAttribute in ProductAttributes.OrderBy(p => p.SortOrder))
            {
                if (filterValueGroups.ContainsKey(productAttribute.Id.ToString()) || productAttribute.ProductAttributeValues.Any(pav => pav.AvailableProductsCount > 0))
                {
                    var fvm = new FilterViewModel { Title = productAttribute.Title, FilterItems = new List<FilterItem>() };
                    foreach (var categoryValue in productAttribute.ProductAttributeValues.OrderBy(a => a.Title))
                    {
                        if (filterValueGroups.ContainsKey(productAttribute.Id.ToString()) || categoryValue.AvailableProductsCount > 0)
                        {
                            var filterItem = new FilterItem
                            {
                                Title = categoryValue.Title,
                                AvaibleProductsCount = categoryValue.AvailableProductsCount,
                                AvaibleProductsCountAfterApplyingFilter = categoryValue.AvailableProductsCountAfterApplyingFilter,
                                Selected = FilterArray.Contains(categoryValue.Id.ToString()),
                                FilterAttributeString = CatalogueFilterHelper.GetFilterStringForCheckbox(FilterArray,
                                    categoryValue.Id.ToString(), FilterArray.Contains(categoryValue.Id.ToString())),
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