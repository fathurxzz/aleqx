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
    public class ProductDetailsModel : SiteModel
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

        public ProductDetailsModel(IShopRepository repository, int langId, string productName, bool showSpecialOffers = false)
            : base(repository, langId, "category", showSpecialOffers)
        {
            _repository = repository;
            if (productName != null)
            {
                try
                {
                    Product = _repository.GetProduct(productName);
                    Product.IsInCart = WebSession.OrderItems.ContainsKey(Product.Id);
                }
                catch (ObjectNotFoundException)
                {
                    ErrorMessage = "Продукт не найден";
                }
            }

            _sw.Stop();

            Log.DebugFormat("SiteModel+ProductDetailsModel: {0}", _sw.Elapsed);
        }
    }
}