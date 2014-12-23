using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Web;
using Shop.DataAccess.Entities;
using Shop.DataAccess.Repositories;
using Shop.WebSite.Helpers;

namespace Shop.WebSite.Models
{
    public class CatalogueModel : SiteModel
    {
        public List<Product> Products { get; set; }
        public IEnumerable<Product> SourceProducts { get; set; }
        public IEnumerable<ProductAttribute> ProductAttributes { get; set; }
        public Product Product { get; set; }
        public Article Article { get; set; }
        public string CurrentFilter { get; set; }
        public string[] FilterArray { get; set; }
        public int ProductTotalCount { get; set; }
        public Category CurrentCategory { get; set; }
        public List<Product> FilteredProducts { get; set; }
        public int Page { get; set; }

        public List<FilterViewModel> Filters { get; set; }

        private IShopRepository _repository { get; set; }

        private Dictionary<string, List<string>> GroupFilterString(string categoryName, string filterString)
        {
            var result = new Dictionary<string, List<string>>();
            if (!string.IsNullOrEmpty(filterString) && !string.IsNullOrEmpty(categoryName))
            {

                string[] filterArray = FilterArray = filterString.Split(new[] { "-" }, StringSplitOptions.RemoveEmptyEntries);


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
            
            CurrentFilter = filter;



            var filterValueGroups = GroupFilterString(categoryName, filter);




            Products = new List<Product>();




            //FilterArray = filter != null ? filter.Split(new[] { "-" }, StringSplitOptions.RemoveEmptyEntries) : new string[0];


            //SourceProducts = SourceProducts.Where(p =>
            //            p.Name == "fuji-female" ||
            //            p.Name == "fuji-male" ||
            //            p.Name == "giant-female" ||
            //            p.Name == "giant-male"||
            //            p.Name == "fuji-none"
            //            );


            if (query != null && query.Length > 1)
            {
                SourceProducts = _repository.GetProductsByQueryString(query).Where(p => p.IsActive);
                foreach (var product in SourceProducts)
                {
                    Products.Add(product);
                }
            }
            else
            {
                SourceProducts = _repository.GetProductsByCategory(categoryName).Where(p => p.IsActive);
                if (filterValueGroups.Any())
                {
                    foreach (var product in SourceProducts)
                    {
                        var productAttributeValueGroups = GroupProductAttributesString(product);
                        var matchCount = 0;
                        var mismatchConut = 0;

                        foreach (var filterValueGroup in filterValueGroups)
                        {
                            var found = 0;
                            foreach (var productAttributeValueGroup in productAttributeValueGroups)
                            {
                                if (filterValueGroup.Key == productAttributeValueGroup.Key)
                                {
                                    found++;
                                    if (CompareGroups(productAttributeValueGroup.Value, filterValueGroup.Value))
                                    {
                                        matchCount++;
                                        break;
                                    }
                                    mismatchConut++;
                                    break;
                                }
                            }
                            if (found == 0)
                            {
                                mismatchConut++;
                            }
                        }

                        if (matchCount > 0 && mismatchConut == 0)
                        {
                            Products.Add(product);
                        }
                    }
                }
                else
                {
                    foreach (var product in SourceProducts)
                    {
                        Products.Add(product);
                    }
                }
            }

            ProductTotalCount = Products.Count;

            IQueryable<Product> products = null;

            products = sortOrder == "desc" ?
                Products.OrderByDescending(p => p.Price).ThenBy(p => p.Title).AsQueryable()
                : Products.OrderBy(p => p.Price).ThenBy(p => p.Title).AsQueryable();

            var pageSize = int.Parse(SiteSettings.GetShopSetting("ProductsPageSize"));

            if (page > products.Count() / pageSize)
            {
                page = 0;
            }


            //FilteredProducts = products;
            var category = Categories.FirstOrDefault(c => c.Name == categoryName);
            if (category != null)
            {
                ProductAttributes = new List<ProductAttribute>();
                ProductAttributes = _repository.GetProductAttributes(category.Id).Where(pa => pa.IsFilterable).ToList();
                CurrentCategory = _repository.GetCategory(category.Id);

                foreach (var productAttribute in ProductAttributes)
                {
                    foreach (var productAttributeValue in productAttribute.ProductAttributeValues)
                    {
                        productAttributeValue.AvailableProductsCount = Enumerable.Count(products, product => productAttributeValue.Products.Contains(product));
                    }
                }



                // создание фильтров
                Filters = new List<FilterViewModel>();
                foreach (var productAttribute in ProductAttributes.OrderBy(p => p.SortOrder))
                {
                    if (productAttribute.ProductAttributeValues.Any(pav => pav.AvailableProductsCount > 0))
                    {
                        var fvm = new FilterViewModel { Title = productAttribute.Title, FilterItems = new List<FilterItem>()};
                        foreach (var categoryValue in productAttribute.ProductAttributeValues.OrderBy(a => a.Title))
                        {
                            //if (categoryValue.AvailableProductsCount > 0)
                            //{
                                var filterItem = new FilterItem
                                {
                                    Title = categoryValue.Title,
                                    AvaibleProductsCount = categoryValue.AvailableProductsCount,
                                    Selected = FilterArray.Contains(categoryValue.Id.ToString()),
                                    FilterAttributeString = CatalogueFilterHelper.GetFilterStringForCheckbox(FilterArray,
                                        categoryValue.Id.ToString(), FilterArray.Contains(categoryValue.Id.ToString())),
                                    Id = "cb_" + categoryValue.Id
                                };

                                fvm.FilterItems.Add(filterItem);
                            //}
                        }

                        if (fvm.FilterItems.Any())
                        {
                            Filters.Add(fvm);
                        }
                    }
                }


            }


            



            products = ApplyPaging(products, page, pageSize);

            Products = products.ToList();
            foreach (var product in Products)
            {
                product.CurrentLang = langId;
                product.Category.CurrentLang = langId;
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










            








        }

        IQueryable<Product> ApplyPaging(IQueryable<Product> products, int? page, int pageSize)
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