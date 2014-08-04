﻿using System;
using System.Collections.Generic;
using System.Linq;
using Shop.DataAccess;
using Shop.DataAccess.Entities;
using Shop.DataAccess.Repositories;

namespace Shop.Api.Repositories
{
    public class ShopRepository : IShopRepository
    {
        private readonly IShopStore _store;

        public ShopRepository(IShopStore store)
        {
            _store = store;
        }

        public int LangId { get; set; }

        public IEnumerable<Category> GetCategories()
        {
            var categories = _store.Categories.ToList();
            foreach (var category in categories)
            {
                category.CurrentLang = LangId;
            }
            return ApplySorting(categories);
        }

        public Category GetCategory(int id)
        {
            var category = _store.Categories.SingleOrDefault(c => c.Id == id);
            if (category == null)
            {
                throw new Exception(string.Format("Category with id={0} not found", id));
            }
            category.CurrentLang = LangId;

            foreach (var productAttribute in category.ProductAttributes)
            {
                productAttribute.CurrentLang = LangId;
            }
            return category;
        }

        public void DeleteCategory(int id)
        {
            var category = _store.Categories.SingleOrDefault(c => c.Id == id);

            if (category == null)
            {
                throw new Exception(string.Format("Category with id={0} doesn't found", id));
            }

            while (category.CategoryLangs.Any())
            {
                var categoryLang = category.CategoryLangs.First();
                _store.CategoryLangs.Remove(categoryLang);
            }

            while (category.ProductAttributes.Any())
            {
                var pa = category.ProductAttributes.First();

                while (pa.ProductAttributeLangs.Any())
                {
                    var pal = pa.ProductAttributeLangs.First();
                    pa.ProductAttributeLangs.Remove(pal);
                }

                _store.ProductAttributes.Remove(pa);
            }

            _store.Categories.Remove(category);
            _store.SaveChanges();

        }

        public int AddCategory(Category category)
        {
            if (_store.Categories.Any(c => c.Name == category.Name))
            {
                throw new Exception(string.Format("Category {0} already exists", category.Name));
            }

            _store.Categories.Add(category);

            CreateOrChangeEntityLanguage(category);

            _store.SaveChanges();
            return category.Id;
        }

        public void SaveCategory(Category category)
        {
            var cache = _store.Categories.Single(c => c.Id == category.Id);
            //if (cache.Name != category.Name)
            //{
            //    if (_store.Categories.Any(c => c.Name == category.Name))
            //    {
            //        throw new Exception(string.Format("Category {0} already exists", category.Name));
            //    }
            //}


            //cache.Name = category.Name;
            //cache.SortOrder = category.SortOrder;
            //cache.Title = category.Title;
            //cache.SeoDescription = category.SeoDescription;
            //cache.SeoKeywords = category.SeoKeywords;
            //cache.SeoText = category.SeoText;

            CreateOrChangeEntityLanguage(cache);

            _store.SaveChanges();
        }

        public IEnumerable<ProductAttribute> GetProductAttributes()
        {
            var productAttributes = _store.ProductAttributes.ToList();
            foreach (var productAttribute in productAttributes)
            {
                productAttribute.CurrentLang = LangId;

                foreach (var value in productAttribute.ProductAttributeValues)
                {
                    value.CurrentLang = LangId;
                }
            }
            return productAttributes;
        }

        public IEnumerable<ProductAttribute> GetProductAttributes(int categoryId)
        {
            var category = _store.Categories.Single(c => c.Id == categoryId);
            foreach (var productAttribute in category.ProductAttributes)
            {
                productAttribute.CurrentLang = LangId;

                foreach (var value in productAttribute.ProductAttributeValues)
                {
                    value.CurrentLang = LangId;
                }
            }
            return category.ProductAttributes;
        }

        public ProductAttribute GetProductAttribute(int id)
        {
            var productAttibute = _store.ProductAttributes.SingleOrDefault(c => c.Id == id);
            if (productAttibute == null)
            {
                throw new Exception(string.Format("ProductAttibute with id={0} not found", id));
            }
            productAttibute.CurrentLang = LangId;

            foreach (var pav in productAttibute.ProductAttributeValues)
            {
                pav.CurrentLang = LangId;

                if (pav.ProductAttributeValueTag != null)
                {
                    pav.ProductAttributeValueTag.CurrentLang = LangId;
                }
            }

            foreach (var pasv in productAttibute.ProductAttributeStaticValues)
            {
                pasv.CurrentLang = LangId;
            }


            return productAttibute;
        }

        public void DeleteProductAttribute(int id)
        {
            var productAttribute = _store.ProductAttributes.SingleOrDefault(c => c.Id == id);

            if (productAttribute == null)
            {
                throw new Exception(string.Format("ProductAttribute with id={0} not found", id));
            }

            while (productAttribute.ProductAttributeLangs.Any())
            {
                var productAttributeLang = productAttribute.ProductAttributeLangs.First();
                _store.ProductAttributeLangs.Remove(productAttributeLang);
            }

            // TODO: Add removing ProductAttributeValues and ProductAttributeStaticValues

            _store.ProductAttributes.Remove(productAttribute);
            _store.SaveChanges();
        }

        public int AddProductAttribute(ProductAttribute productAttribute)
        {
            _store.ProductAttributes.Add(productAttribute);
            CreateOrChangeEntityLanguage(productAttribute);
            _store.SaveChanges();
            return productAttribute.Id;
        }

        public void SaveProductAttribute(ProductAttribute productAttribute)
        {
            var cache = _store.ProductAttributes.Single(c => c.Id == productAttribute.Id);

            //cache.UnitTitle = productAttribute.UnitTitle;
            //cache.SortOrder = productAttribute.SortOrder;
            //cache.Title = productAttribute.Title;

            CreateOrChangeEntityLanguage(cache);
            _store.SaveChanges();
        }

        public int AddProductAttributeValue(ProductAttributeValue productAttributeValue)
        {
            _store.ProductAttributeValues.Add(productAttributeValue);
            CreateOrChangeEntityLanguage(productAttributeValue);
            _store.SaveChanges();
            return productAttributeValue.Id;
        }

        public void SaveProductAttributeValue(ProductAttributeValue productAttributeValue)
        {
            var cache = _store.ProductAttributeValues.Single(c => c.Id == productAttributeValue.Id);
            CreateOrChangeEntityLanguage(cache);
            _store.SaveChanges();
        }

        public ProductAttributeValue GetProductAttributeValue(int id)
        {
            var productAttibuteValue = _store.ProductAttributeValues.SingleOrDefault(c => c.Id == id);
            if (productAttibuteValue == null)
            {
                throw new Exception(string.Format("ProductAttibuteValue with id={0} not found", id));
            }
            productAttibuteValue.CurrentLang = LangId;

            if (productAttibuteValue.ProductAttributeValueTag != null)
            {
                productAttibuteValue.ProductAttributeValueTag.CurrentLang = LangId;
            }

            return productAttibuteValue;
        }

        public void DeleteProductAttributeValue(int id)
        {
            var productAttributeValue = _store.ProductAttributeValues.SingleOrDefault(c => c.Id == id);
            if (productAttributeValue == null)
            {
                throw new Exception(string.Format("ProductAttributeValue with id={0} not found", id));
            }

            while (productAttributeValue.ProductAttributeValueLangs.Any())
            {
                var productAttributeValueLang = productAttributeValue.ProductAttributeValueLangs.First();
                _store.ProductAttributeValueLangs.Remove(productAttributeValueLang);
            }
            _store.ProductAttributeValues.Remove(productAttributeValue);
            _store.SaveChanges();
        }

        public int AddProductAttributeStaticValue(ProductAttributeStaticValue productAttributeStaticValue)
        {
            _store.ProductAttributeStaticValues.Add(productAttributeStaticValue);
            CreateOrChangeEntityLanguage(productAttributeStaticValue);
            _store.SaveChanges();
            return productAttributeStaticValue.Id;
        }

        public void SaveProductAttributeStaticValue(ProductAttributeStaticValue productAttributeStaticValue)
        {
            var cache = _store.ProductAttributeStaticValues.Single(c => c.Id == productAttributeStaticValue.Id);
            CreateOrChangeEntityLanguage(cache);
            _store.SaveChanges();
        }

        public ProductAttributeStaticValue GetProductAttributeStaticValue(int productAttributeId, int productId)
        {
            var productAttibuteStaticValue = _store.ProductAttributeStaticValues.SingleOrDefault(c => c.ProductAttributeId == productAttributeId && c.ProductId == productId);
            if (productAttibuteStaticValue != null)
            {
                productAttibuteStaticValue.CurrentLang = LangId;
                return productAttibuteStaticValue;
            }
            return null;
        }

        public ProductAttributeStaticValue GetProductAttributeStaticValue(int id)
        {
            var productAttibuteStaticValue = _store.ProductAttributeStaticValues.SingleOrDefault(c => c.Id == id);
            if (productAttibuteStaticValue == null)
            {
                throw new Exception(string.Format("ProductAttibuteStaticValue with id={0} not found", id));
            }
            productAttibuteStaticValue.CurrentLang = LangId;
            return productAttibuteStaticValue;
        }

        public void DeleteProductAttributeStaticValue(int id)
        {
            var productAttributeStaticValue = _store.ProductAttributeStaticValues.SingleOrDefault(c => c.Id == id);
            if (productAttributeStaticValue == null)
            {
                throw new Exception(string.Format("ProductAttributeStaticValue with id={0} not found", id));
            }

            while (productAttributeStaticValue.ProductAttributeStaticValueLangs.Any())
            {
                var productAttributeStaticValueLang = productAttributeStaticValue.ProductAttributeStaticValueLangs.First();
                _store.ProductAttributeStaticValueLangs.Remove(productAttributeStaticValueLang);
            }
            _store.ProductAttributeStaticValues.Remove(productAttributeStaticValue);
            _store.SaveChanges();
        }

        public IEnumerable<ProductAttributeValueTag> GetProductAttributeValueTags()
        {
            var productAttributeValueTags = _store.ProductAttributeValueTags.ToList();
            foreach (var pavt in productAttributeValueTags)
            {
                pavt.CurrentLang = LangId;
            }
            return (productAttributeValueTags);
        }

        public ProductAttributeValueTag GetProductAttributeValueTag(int id)
        {
            var pavt = _store.ProductAttributeValueTags.SingleOrDefault(c => c.Id == id);
            if (pavt == null)
            {
                return null;
            }
            pavt.CurrentLang = LangId;
            return pavt;
        }

        public void DeleteProductAttributeValueTag(int id)
        {
            var pavt = _store.ProductAttributeValueTags.SingleOrDefault(c => c.Id == id);

            if (pavt == null)
            {
                throw new Exception(string.Format("ProductAttributeValueTag with id={0} doesn't found", id));
            }

            while (pavt.ProductAttributeValueTagLangs.Any())
            {
                var pavtLang = pavt.ProductAttributeValueTagLangs.First();
                _store.ProductAttributeValueTagLangs.Remove(pavtLang);
            }
            _store.ProductAttributeValueTags.Remove(pavt);
            _store.SaveChanges();

        }

        public int AddProductAttributeValueTag(ProductAttributeValueTag productAttributeValueTag)
        {
            _store.ProductAttributeValueTags.Add(productAttributeValueTag);
            CreateOrChangeEntityLanguage(productAttributeValueTag);
            _store.SaveChanges();
            return productAttributeValueTag.Id;
        }

        public void SaveProductAttributeValueTag(ProductAttributeValueTag productAttributeValueTag)
        {
            var cache = _store.ProductAttributeValueTags.Single(c => c.Id == productAttributeValueTag.Id);
            CreateOrChangeEntityLanguage(cache);
            _store.SaveChanges();
        }

        public IEnumerable<Product> GetProducts()
        {
            var products = _store.Products.ToList();
            foreach (var product in products)
            {
                product.CurrentLang = LangId;
                product.Category.CurrentLang = LangId;
            }
            return products;
        }

        public Product GetProduct(int id)
        {
            var product = _store.Products.SingleOrDefault(p => p.Id == id);
            if (product == null)
            {
                throw new Exception(string.Format("Product with id={0} not found", id));
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

            return product;
        }

        public void DeleteProduct(int id, Action<String> deleteImages)
        {
            var product = _store.Products.SingleOrDefault(c => c.Id == id);

            if (product == null)
            {
                throw new Exception(string.Format("Product with id={0} not found", id));
            }

            while (product.ProductLangs.Any())
            {
                var productLang = product.ProductLangs.First();
                _store.ProductLangs.Remove(productLang);
            }

            while (product.ProductAttributeValues.Any())
            {
                var pav = product.ProductAttributeValues.First();

                while (pav.ProductAttributeValueLangs.Any())
                {
                    var pavl = pav.ProductAttributeValueLangs.First();
                    pav.ProductAttributeValueLangs.Remove(pavl);
                }

                _store.ProductAttributeValues.Remove(pav);
            }

            while (product.ProductImages.Any())
            {
                var pi = product.ProductImages.First();
                deleteImages(pi.ImageSource);
                //var images = deleteImages;
                //images(pi.ImageSource);
                _store.ProductImages.Remove(pi);
            }

            _store.Products.Remove(product);
            _store.SaveChanges();
        }

        public int AddProduct(Product product)
        {
            if (_store.Products.Any(c => c.Name == product.Name))
            {
                throw new Exception(string.Format("Category {0} already exists", product.Name));
            }

            _store.Products.Add(product);

            CreateOrChangeEntityLanguage(product);

            _store.SaveChanges();
            return product.Id;
        }

        public void SaveProduct(Product product)
        {
            var cache = _store.Products.Single(c => c.Id == product.Id);
            //if (cache.Name != category.Name)
            //{
            //    if (_store.Categories.Any(c => c.Name == category.Name))
            //    {
            //        throw new Exception(string.Format("Category {0} already exists", category.Name));
            //    }
            //}


            //cache.Name = category.Name;
            //cache.SortOrder = category.SortOrder;
            //cache.Title = category.Title;
            //cache.SeoDescription = category.SeoDescription;
            //cache.SeoKeywords = category.SeoKeywords;
            //cache.SeoText = category.SeoText;

            CreateOrChangeEntityLanguage(cache);

            _store.SaveChanges();
        }

        public ProductImage GetProductImage(int id)
        {
            var productImage = _store.ProductImages.SingleOrDefault(p => p.Id == id);
            if (productImage == null)
            {
                throw new Exception(string.Format("ProductImage with id={0} not found", id));
            }
            return productImage;
        }

        public void DeleteProductImage(int id, Action<String> deleteImage)
        {
            var productImage = _store.ProductImages.SingleOrDefault(p => p.Id == id);
            if (productImage == null)
            {
                throw new Exception(string.Format("ProductImage with id={0} not found", id));
            }
            deleteImage(productImage.ImageSource);
            _store.ProductImages.Remove(productImage);
            _store.SaveChanges();
        }


        private void CreateOrChangeEntityLanguage(Category cache)
        {
            var categoryLang = _store.CategoryLangs.FirstOrDefault(r => r.CategoryId == cache.Id && r.LanguageId == LangId);
            if (categoryLang == null)
            {
                var entityLang = new CategoryLang
                {
                    CategoryId = cache.Id,
                    LanguageId = LangId,

                    Title = cache.Title,
                    SeoDescription = cache.SeoDescription,
                    SeoKeywords = cache.SeoKeywords,
                    SeoText = cache.SeoText
                };
                _store.CategoryLangs.Add(entityLang);
            }
            else
            {
                categoryLang.Title = cache.Title;
                categoryLang.SeoDescription = cache.SeoDescription;
                categoryLang.SeoKeywords = cache.SeoKeywords;
                categoryLang.SeoText = cache.SeoText;
            }

        }
        private void CreateOrChangeEntityLanguage(ProductAttribute cache)
        {
            var categoryLang = _store.ProductAttributeLangs.FirstOrDefault(r => r.ProductAttributeId == cache.Id && r.LanguageId == LangId);
            if (categoryLang == null)
            {
                var entityLang = new ProductAttributeLang
                {
                    ProductAttributeId = cache.Id,
                    LanguageId = LangId,

                    Title = cache.Title,
                    UnitTitle = cache.UnitTitle,
                };
                _store.ProductAttributeLangs.Add(entityLang);
            }
            else
            {
                categoryLang.Title = cache.Title;
                categoryLang.UnitTitle = cache.UnitTitle;
            }

        }
        private void CreateOrChangeEntityLanguage(ProductAttributeValue cache)
        {
            var categoryLang = _store.ProductAttributeValueLangs.FirstOrDefault(r => r.ProductAttributeValueId == cache.Id && r.LanguageId == LangId);
            if (categoryLang == null)
            {
                var entityLang = new ProductAttributeValueLang
                {
                    ProductAttributeValueId = cache.Id,
                    LanguageId = LangId,

                    Title = cache.Title,
                };
                _store.ProductAttributeValueLangs.Add(entityLang);
            }
            else
            {
                categoryLang.Title = cache.Title;
            }

        }
        private void CreateOrChangeEntityLanguage(ProductAttributeStaticValue cache)
        {
            var categoryLang = _store.ProductAttributeStaticValueLangs.FirstOrDefault(r => r.ProductAttributeStaticValueId == cache.Id && r.LanguageId == LangId);
            if (categoryLang == null)
            {
                var entityLang = new ProductAttributeStaticValueLang
                {
                    ProductAttributeStaticValueId = cache.Id,
                    LanguageId = LangId,

                    Title = cache.Title,
                };
                _store.ProductAttributeStaticValueLangs.Add(entityLang);
            }
            else
            {
                categoryLang.Title = cache.Title;
            }

        }
        private void CreateOrChangeEntityLanguage(ProductAttributeValueTag cache)
        {
            var categoryLang = _store.ProductAttributeValueTagLangs.FirstOrDefault(r => r.ProductAttributeValueTagId == cache.Id && r.LanguageId == LangId);
            if (categoryLang == null)
            {
                var entityLang = new ProductAttributeValueTagLang
                {
                    ProductAttributeValueTagId = cache.Id,
                    LanguageId = LangId,

                    Title = cache.Title,
                };
                _store.ProductAttributeValueTagLangs.Add(entityLang);
            }
            else
            {
                categoryLang.Title = cache.Title;
            }

        }

        private void CreateOrChangeEntityLanguage(Product cache)
        {
            var categoryLang = _store.ProductLangs.FirstOrDefault(r => r.ProductId == cache.Id && r.LanguageId == LangId);
            if (categoryLang == null)
            {
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
                _store.ProductLangs.Add(entityLang);
            }
            else
            {
                categoryLang.Title = cache.Title;
                categoryLang.Description = cache.Description;
                categoryLang.SeoDescription = cache.SeoDescription;
                categoryLang.SeoKeywords = cache.SeoKeywords;
                categoryLang.SeoText = cache.SeoText;
            }

        }


        private List<Category> _result = new List<Category>();
        private IEnumerable<Category> ApplySorting(IEnumerable<Category> source)
        {
            foreach (var item in source.Where(c => c.Parent == null).OrderBy(c => c.SortOrder))
            {
                Visit(item);
            }

            return _result;
        }
        private void Visit(Category node)
        {
            _result.Add(node);
            if (node.Children == null || node.Children.Count == 0)
            {
                return;
            }
            foreach (var child in node.Children.OrderBy(c => c.SortOrder))
            {
                Visit(child);
            }
        }
    }
}
