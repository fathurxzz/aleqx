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
        public Article Article { get; set; }
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


        private Dictionary<string, List<string>> GroupProductAttributesString(Product product)
        {
            var result = new Dictionary<string, List<string>>();
            var criteriaGroups = product.SearchCriteriaAttributes.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var s in criteriaGroups)
            {
                var criteriaGroupItem = s.Split(new[] { "-" }, StringSplitOptions.RemoveEmptyEntries);
                var productAttributeId = criteriaGroupItem[0];
                var productAttributeValueId = criteriaGroupItem[1];
                if (result.ContainsKey(productAttributeId))
                {
                    var tmp = result[productAttributeId];
                    tmp.Add(productAttributeValueId);
                    result[productAttributeId] = tmp;
                }
                else
                {
                    result.Add(productAttributeId, new List<string> { productAttributeValueId });
                }
            }
            return result;
        }

        private bool CompareGroups(List<string> value1, List<string> value2)
        {
            foreach (var s1 in value1)
            {
                if (value2.Contains(s1))
                    return true;
            }
            return false;
        }


        public CatalogueModel(IShopRepository repository, int langId, int? page, string categoryName = null, string productName = null, string articleName = null, string filter = null, string query = null, string sortOrder = null, string sortBy = null)
            : base(repository, langId, "category")
        {

            
            _repository = repository;
            FilterArray = new string[0];

            CurrentFilter = filter ?? string.Empty;

            IQueryable<Product> products = null;
            IQueryable<Product> allProducts = null;

            //var filterValueGroups = GroupFilterString(categoryName, filter);
            var filters = CurrentFilter.Split(new[] { "-" }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse);


            if (query != null && query.Length > 1)
            {
                products = _repository.GetProductsByQueryString(query).Where(p => p.IsActive);
            }
            else
            {
                products = allProducts = _repository.GetProductsByCategory(categoryName).Where(p => p.IsActive);
                if (filters.Any())
                    //products = products.Where(x => x.ProductAttributeValues.Select(pav => filters.Contains(pav.Id)).Any());
                    //products = products.Where(x => x.ProductAttributeValues.Where(pav => filters.Contains(pav.Id)).Any());
                    products = products.Where(x => x.ProductAttributeValues.Any(pav => filters.Contains(pav.Id)));
                    //products = products.Where(p => p.ProductAttributeValues.Where(pav => pav.Id == 350).Any());
            }

            switch (sortOrder)
            {
                case "desc":
                    products = products.OrderByDescending(p => p.Price).ThenBy(p => p.Title).AsQueryable();
                    break;
                case "asc":
                    products = products.OrderBy(p => p.Price).ThenBy(p => p.Title).AsQueryable();
                    break;
                default: // "abc"
                    products = products.OrderBy(p => p.Name).AsQueryable();
                    break;
            }


            var pageSize = int.Parse(SiteSettings.GetShopSetting("ProductsPageSize"));
            Products = products.Include(x => x.ProductAttributeValues)
                .Include(x => x.ProductImages)
                .ToList();

            //var AllProducts = allProducts.Include(x => x.ProductAttributeValues)
            //    .Include(x => x.ProductImages)
            //    .ToList();

            ProductTotalCount = Products.Count();

            if (page > Products.Count() / pageSize)
            {
                page = 0;
            }


            //FilteredProducts = products;
            var category = Categories.FirstOrDefault(c => c.Name == categoryName);
            if (category != null)
            {
                ProductAttributes = _repository.GetProductAttributes(category.Id).Where(pa => pa.IsFilterable).ToList();
                CurrentCategory = _repository.GetCategory(category.Id);

                foreach (var productAttribute in ProductAttributes)
                {
                    foreach (var productAttributeValue in productAttribute.ProductAttributeValues)
                    {
                        productAttributeValue.AvailableProductsCount = Products.Count(p => p.ProductAttributeValues.Any(pav => pav.Id == productAttributeValue.Id));
                        //productAttributeValue.AvailableProductsCountAfterApplyingFilter = AllProducts.Count(p => p.ProductAttributeValues.Any(pav => pav.Id == productAttributeValue.Id));
                    }
                }

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


            }

            Products = ApplyPaging(Products.AsQueryable(), page, pageSize).ToList();

            foreach (var product in Products)
            {
                product.CurrentLang = langId;
                //product.Category.CurrentLang = langId;
                if (product.ProductImages.Any())
                {
                    var pi = product.ProductImages.FirstOrDefault(c => c.IsDefault) ?? product.ProductImages.First();
                    product.ImageSource = pi.ImageSource;
                }
            }

            if (productName != null)
            {
                try
                {
                    this.Product = _repository.GetProduct(productName);
                    this.Product.IsInCart = WebSession.OrderItems.ContainsKey(Product.Id);
                }
                catch (ObjectNotFoundException)
                {
                    ErrorMessage = "Продукт не найден";
                }
            }

            if (articleName != null)
            {
                this.Article = _repository.GetArticle(articleName);
            }

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