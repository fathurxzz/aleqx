using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.DataAccess.Entities;
using Shop.DataAccess.Repositories;

namespace Shop.Api.Repositories
{
    public partial class ShopRepository : IShopRepository
    {
       public IQueryable<Product> GetAllProducts()
        {
            return _store.Products;
        }
        public IEnumerable<Product> GetActiveProducts()
        {
            var products = _store.Products.Where(p => p.IsActive).ToList();
            foreach (var product in products)
            {
                product.CurrentLang = LangId;
                product.Category.CurrentLang = LangId;

                if (product.ProductImages.Any())
                {
                    var pi = product.ProductImages.FirstOrDefault(c => c.IsDefault) ?? product.ProductImages.First();
                    product.ImageSource = pi.ImageSource;
                }
            }
            return products;
        }

        public IQueryable<Product> GetProductsByCategory(string categoryName)
        {
            return _store.Products.Where(p => p.Category.Name == categoryName);
        }

        public IQueryable<Product> GetProductsByQueryString(string query)
        {
            string[] x = query.Split(new[] {" "}, StringSplitOptions.RemoveEmptyEntries);
            if (x.Length == 2)
            {
                return _store.Products.ToList()
                    .Where(p => 
                        p.SearchCriteria.IndexOf(x[0], StringComparison.InvariantCultureIgnoreCase) > -1 && 
                        p.SearchCriteria.IndexOf(x[1], StringComparison.InvariantCultureIgnoreCase) > -1).AsQueryable();
            }
            
            if (x.Length == 3)
            {
                return _store.Products.ToList()
                    .Where(p =>
                        p.SearchCriteria.IndexOf(x[0], StringComparison.InvariantCultureIgnoreCase) > -1 &&
                        p.SearchCriteria.IndexOf(x[1], StringComparison.InvariantCultureIgnoreCase) > -1 && 
                        p.SearchCriteria.IndexOf(x[2], StringComparison.InvariantCultureIgnoreCase) > -1).AsQueryable();
            }

            if (x.Length == 4)
            {
                return _store.Products.ToList()
                    .Where(p =>
                        p.SearchCriteria.IndexOf(x[0], StringComparison.InvariantCultureIgnoreCase) > -1 &&
                        p.SearchCriteria.IndexOf(x[1], StringComparison.InvariantCultureIgnoreCase) > -1 &&
                        p.SearchCriteria.IndexOf(x[2], StringComparison.InvariantCultureIgnoreCase) > -1 && 
                        p.SearchCriteria.IndexOf(x[3], StringComparison.InvariantCultureIgnoreCase) > -1).AsQueryable();
            }

            return _store.Products.ToList().Where(p => p.SearchCriteria.IndexOf(x[0], StringComparison.InvariantCultureIgnoreCase) > -1).AsQueryable();
        }

        public Product FindProduct(int id)
        {
            var product = _store.Products.SingleOrDefault(p => p.Id == id);
            if (product == null)
            {
                return null;
            }
            product.CurrentLang = LangId;

            foreach (var productAttributeValue in product.ProductAttributeValues)
            {
                productAttributeValue.CurrentLang = LangId;
            }

            foreach (var productAttributeStaticValue in product.ProductAttributeStaticValues)
            {
                productAttributeStaticValue.CurrentLang = LangId;
            }
            if (product.ProductImages.Any())
            {
                var pi = product.ProductImages.FirstOrDefault(c => c.IsDefault) ?? product.ProductImages.First();
                product.ImageSource = pi.ImageSource;
            }

            return product;
        }

        public Product GetProduct(int id)
        {
            var product = _store.Products.SingleOrDefault(p => p.Id == id);
            if (product == null)
            {
                throw new ObjectNotFoundException(string.Format("Product with id={0} not found", id));
            }
            product.CurrentLang = LangId;

            foreach (var productAttributeValue in product.ProductAttributeValues)
            {
                productAttributeValue.CurrentLang = LangId;
            }

            foreach (var productAttributeStaticValue in product.ProductAttributeStaticValues)
            {
                productAttributeStaticValue.CurrentLang = LangId;
            }
            if (product.ProductImages.Any())
            {
                var pi = product.ProductImages.FirstOrDefault(c => c.IsDefault) ?? product.ProductImages.First();
                product.ImageSource = pi.ImageSource;
            }

            return product;
        }

        public Product GetProduct(string name)
        {
            var product = _store.Products.SingleOrDefault(p => p.Name == name);
            if (product == null)
            {
                throw new ObjectNotFoundException(string.Format("Product with name={0} not found", name));
            }
            product.CurrentLang = LangId;

            foreach (var productChild in product.ProductChildren)
            {
                productChild.CurrentLang = LangId;

                if (productChild.ProductImages.Any())
                {
                    var cpp = productChild.ProductImages.FirstOrDefault(pi => pi.IsDefault) ?? productChild.ProductImages.First();
                    productChild.ImageSource = cpp.ImageSource;
                }
            }
            
            foreach (var productAttributeValue in product.ProductAttributeValues)
            {
                productAttributeValue.CurrentLang = LangId;

                if (productAttributeValue.ProductAttributeValueTag != null)
                {
                    productAttributeValue.ProductAttributeValueTag.CurrentLang = LangId;
                }
            }

            foreach (var productAttributeStaticValue in product.ProductAttributeStaticValues)
            {
                productAttributeStaticValue.CurrentLang = LangId;
            }

            foreach (var productAttribute in product.Category.ProductAttributes)
            {
                productAttribute.CurrentLang = LangId;
            }

            if (product.ProductImages.Any())
            {
                var pp = product.ProductImages.FirstOrDefault(pi => pi.IsDefault) ?? product.ProductImages.First();
                product.ImageSource = pp.ImageSource;
            }


            return product;
        }

        public Product GetProductByExternalId(string externalId)
        {
            //Log.DebugFormat("GetProductByExternalId started ExternalId:{0}", externalId);
            var product = _store.Products.SingleOrDefault(p => p.ExternalId == externalId);
            if (product == null)
            {
                //Log.Debug("GetProductByExternalId Product not found");
                return null;
            }
            //Log.DebugFormat("GetProductByExternalId set current lang started");
            product.CurrentLang = LangId;
            //Log.DebugFormat("GetProductByExternalId set current lang finished");


            //Log.DebugFormat("GetProductByExternalId read ProductAttributeValues started");
            foreach (var productAttributeValue in product.ProductAttributeValues)
            {
                //Log.DebugFormat("GetProductByExternalId set productAttributeValue current lang started");
                productAttributeValue.CurrentLang = LangId;
                //Log.DebugFormat("GetProductByExternalId set productAttributeValue current lang finished");

                if (productAttributeValue.ProductAttributeValueTag != null)
                {
                    //Log.DebugFormat("GetProductByExternalId set ProductAttributeValueTag current lang started");
                    productAttributeValue.ProductAttributeValueTag.CurrentLang = LangId;
                    //Log.DebugFormat("GetProductByExternalId set ProductAttributeValueTag current lang finished");
                }
            }
            //Log.DebugFormat("GetProductByExternalId read ProductAttributeValues finished");


            //Log.DebugFormat("GetProductByExternalId read ProductAttributeStaticValues started");
            foreach (var productAttributeStaticValue in product.ProductAttributeStaticValues)
            {
                //Log.DebugFormat("GetProductByExternalId set productAttributeStaticValue current lang started");
                productAttributeStaticValue.CurrentLang = LangId;
                //Log.DebugFormat("GetProductByExternalId set productAttributeStaticValue current lang finished");
            }
            //Log.DebugFormat("GetProductByExternalId read ProductAttributeStaticValues finished");


            //Log.DebugFormat("GetProductByExternalId read Category.ProductAttributes started");
            foreach (var productAttribute in product.Category.ProductAttributes)
            {
                //Log.DebugFormat("GetProductByExternalId set Category.ProductAttribute current lang started");
                productAttribute.CurrentLang = LangId;
                //Log.DebugFormat("GetProductByExternalId set Category.ProductAttribute current lang finished");
            }
            //Log.DebugFormat("GetProductByExternalId read Category.ProductAttributes finished");

            //Log.DebugFormat("GetProductByExternalId read IsDefaultProductImage started");
            foreach (var image in product.ProductImages.Where(pi => pi.IsDefault))
            {
                product.ImageSource = image.ImageSource;
            }
            //Log.DebugFormat("GetProductByExternalId read IsDefaultProductImage finished");

            //Log.DebugFormat("GetProductByExternalId finished");
            return product;
        }

        public void DeleteProduct(int id, Action<String> deleteImages)
        {
            var product = _store.Products.SingleOrDefault(c => c.Id == id);

            if (product == null)
            {
                throw new ObjectNotFoundException(string.Format("Product with id={0} not found", id));
            }

            product.Category = null;

            while (product.ProductLangs.Any())
            {
                var productLang = product.ProductLangs.First();
                _store.ProductLangs.Remove(productLang);
            }
            product.ProductLangs = null;
         
            product.ProductAttributeValues.Clear();
            _store.SaveChanges();

            while (product.ProductStocks.Any())
            {
                var productStock = product.ProductStocks.First();
                _store.ProductStocks.Remove(productStock);
            }
            product.ProductStocks = null;
            _store.SaveChanges();

            product.ProductAttributeStaticValues = null;

            while (product.ProductImages.Any())
            {
                var pi = product.ProductImages.First();
                deleteImages(pi.ImageSource);
                _store.ProductImages.Remove(pi);
            }
            product.ProductImages = null;

            _store.Products.Remove(product);
            _store.SaveChanges();
        }

        public int AddProduct(Product product)
        {
            if (_store.Products.Any(c => c.Name == product.Name))
            {
                throw new ObjectNotFoundException(string.Format("Товар с Name={0} существует в базе. Параметр Name должен быть уникален.", product.Name));
            }

            _store.Products.Add(product);

            CreateOrChangeEntityLanguage(product);

            string searchCriteria = product.ProductLangs.Aggregate("", (current, productLang) => current + (productLang.Title + " ")) + " " + product.Name;

            product.SearchCriteria = searchCriteria;
            
            
            _store.SaveChanges();
            return product.Id;
        }

        public void SaveProduct(Product product)
        {
            //Log.DebugFormat("SaveProduct started");
            var cache = _store.Products.Single(c => c.Id == product.Id);
            //Log.DebugFormat("SaveProduct CreateOrChangeEntityLanguage started");
            CreateOrChangeEntityLanguage(cache);
            //Log.DebugFormat("SaveProduct CreateOrChangeEntityLanguage finished");
            string searchCriteria = product.ProductLangs.Aggregate("", (current, productLang) => current + (productLang.Title + " ")) + " " + product.Name;
            product.SearchCriteria = searchCriteria;
            //Log.DebugFormat("SaveProduct _store.SaveChanges() started");
            _store.SaveChanges();
            //Log.DebugFormat("SaveProduct _store.SaveChanges() finished");
            //Log.DebugFormat("SaveProduct finished");
        }

        private void CreateOrChangeEntityLanguage(Product cache)
        {
            try
            {
                //Log.DebugFormat("private void CreateOrChangeEntityLanguage started");

                //Log.DebugFormat("product {0}", cache);
                //Log.DebugFormat("LangId {0}", LangId);

                //Log.DebugFormat("var categoryLang = _store.ProductLangs.FirstOrDefault(r => r.ProductId == cache.Id && r.LanguageId == LangId);");

                var categoryLang = _store.ProductLangs.FirstOrDefault(r => r.ProductId == cache.Id && r.LanguageId == LangId);
                //Log.DebugFormat("product lang {0}", categoryLang);
                if (categoryLang == null)
                {
                    //Log.DebugFormat("product lang not found. creating new ProductLang");

                    var entityLang = new ProductLang
                    {
                        ProductId = cache.Id,
                        LanguageId = LangId,

                        Title = cache.Title,
                        Description = cache.Description,
                        SeoDescription = cache.SeoDescription,
                        SeoKeywords = cache.SeoKeywords,
                        SeoText = cache.SeoText,
                    };
                    //Log.DebugFormat("new ProductLang created");
                    //Log.DebugFormat(" _store.ProductLangs.Add(entityLang) started");
                    _store.ProductLangs.Add(entityLang);
                    //Log.DebugFormat(" _store.ProductLangs.Add(entityLang) finished");

                }
                else
                {
                    //Log.DebugFormat("product langfound. updating lang");
                    categoryLang.Title = cache.Title;
                    categoryLang.Description = cache.Description;
                    categoryLang.SeoDescription = cache.SeoDescription;
                    categoryLang.SeoKeywords = cache.SeoKeywords;
                    categoryLang.SeoText = cache.SeoText;
                }

                //Log.DebugFormat("private void CreateOrChangeEntityLanguage finished");
            }
            catch (Exception ex)
            {

                Log.ErrorFormat("exception {0}", ex);
            }
        }
    }
}
