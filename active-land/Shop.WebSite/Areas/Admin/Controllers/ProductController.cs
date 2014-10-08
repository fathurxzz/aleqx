using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Shop.DataAccess.Entities;
using Shop.DataAccess.Repositories;
using Shop.WebSite.Helpers;
using Shop.WebSite.Helpers.Graphics;

namespace Shop.WebSite.Areas.Admin.Controllers
{
    public class ProductController : AdminController
    {
        public int Page { get; set; }

        public ProductController(IShopRepository repository)
            : base(repository)
        {
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

        public ActionResult Index(int? page, string q)
        {
            _repository.LangId = CurrentLangId;
            q = q != null ? q.ToLower() : null;
            var orderedProducts = _repository.GetAllProducts()
                .Where(p => string.IsNullOrEmpty(q) || p.SearchCriteria.ToLower().Contains(q))
                .OrderBy(p => p.CategoryId).ThenBy(p => p.Title).AsQueryable();

            int productsCount = orderedProducts.Count();
            orderedProducts = ApplyPaging(orderedProducts, page, int.Parse(WebSession.ShopSettings.First(ss => ss.Key == "AdminProductsPageSize").Value));
            var products = orderedProducts.ToList();
            foreach (var product in products)
            {
                product.CurrentLang = CurrentLangId;
                product.Category.CurrentLang = CurrentLangId;
                if (product.ProductImages.Any())
                {
                    var pi = product.ProductImages.FirstOrDefault(c => c.IsDefault) ?? product.ProductImages.First();
                    product.ImageSource = pi.ImageSource;
                }
            }

            //foreach (var product in products)
            //{
            //    foreach (var pav in product.ProductAttributeValues)
            //    {
            //        product.SearchCriteriaAttributes += pav.ProductAttributeId + "-" + pav.Id + ";";
            //    }

            //    _repository.SaveProduct(product);
            //}

            ViewBag.ProductTotalCount = productsCount;
            ViewBag.Page = Page;
            ViewBag.Q = q;

            return View(products);
        }


        public ActionResult Create(int? id)
        {
            _repository.LangId = CurrentLangId;

            var categories = _repository.GetCategories();
            if (id.HasValue)
            {
                foreach (var category in categories.Where(category => category.Id == id))
                {
                    category.Selected = true;
                }
            }
            ViewBag.Categories = categories;

            return View(new Product { CategoryId = id ?? 0 });
        }

        [HttpPost]
        public ActionResult Create(Product model, FormCollection form)
        {
            _repository.LangId = CurrentLangId;
            try
            {
                model.Id = 0;
                var categories = _repository.GetCategories();


                int categoryId = int.Parse(form["categoryId"]);

                foreach (var category in categories.Where(category => category.Id == categoryId))
                {
                    category.Selected = true;
                }

                ViewBag.Categories = categories;

                var product = new Product
                {
                    Name = string.IsNullOrEmpty(model.Name)
                        ? SiteHelper.UpdatePageWebName(model.Name, model.Title)
                        : SiteHelper.UpdatePageWebName(model.Name),
                    CategoryId = categoryId,
                    Title = model.Title,
                    SeoDescription = model.SeoDescription,
                    SeoKeywords = model.SeoKeywords,
                    SeoText = model.SeoText,
                    IsActive = model.IsActive,
                    IsDiscount = model.IsDiscount,
                    IsNew = model.IsNew,
                    IsTopSale = model.IsTopSale,
                    OldPrice = model.OldPrice,
                    Price = model.Price,
                    SearchCriteriaAttributes = ""
                };

                product.Description = model.Description == null ? "" : HttpUtility.HtmlDecode(model.Description);

                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var file = Request.Files[i];
                    if (file == null) continue;
                    if (string.IsNullOrEmpty(file.FileName)) continue;

                    string fileName = IOHelper.GetUniqueFileName("~/Content/Images", file.FileName);
                    string filePath = Server.MapPath("~/Content/Images");

                    filePath = Path.Combine(filePath, fileName);
                    GraphicsHelper.SaveOriginalImage(filePath, fileName, file, 1500);
                    var productImage = new ProductImage { ImageSource = fileName };
                    product.ProductImages.Add(productImage);
                }




                _repository.AddProduct(product);

            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View(model);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            _repository.LangId = CurrentLangId;

            try
            {
                var product = _repository.GetProduct(id);
                var categories = _repository.GetCategories();
                foreach (var category in categories.Where(category => category.Id == product.CategoryId))
                {
                    category.Selected = true;
                }
                ViewBag.Categories = categories;

                return View(product);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Edit(Product model, FormCollection form)
        {
            _repository.LangId = CurrentLangId;

            try
            {
                var product = _repository.GetProduct(model.Id);

                var categories = _repository.GetCategories();
                foreach (var category in categories.Where(category => category.Id == product.CategoryId))
                {
                    category.Selected = true;
                }
                ViewBag.Categories = categories;

                foreach (var category in categories.Where(category => category.Id == product.CategoryId))
                {
                    category.Selected = true;
                }

                ViewBag.Categories = categories;


                product.Name = SiteHelper.UpdatePageWebName(model.Name);
                TryUpdateModel(product, new[] { "Title", "SeoDescription", "SeoKeywords", "SeoText", "IsActive", "IsDiscount", "IsNew", "IsTopSale" });
                int categoryId = int.Parse(form["categoryId"]);
                product.Price = decimal.Parse(form["Price"]);
                product.OldPrice = decimal.Parse(form["OldPrice"]);
                product.CategoryId = categoryId;
                product.Description = model.Description == null ? "" : HttpUtility.HtmlDecode(model.Description);
                product.SearchCriteriaAttributes = "";


                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var file = Request.Files[i];
                    if (file == null) continue;
                    if (string.IsNullOrEmpty(file.FileName)) continue;

                    string fileName = IOHelper.GetUniqueFileName("~/Content/Images", file.FileName);
                    string filePath = Server.MapPath("~/Content/Images");

                    filePath = Path.Combine(filePath, fileName);
                    GraphicsHelper.SaveOriginalImage(filePath, fileName, file, 1500);
                    var productImage = new ProductImage { ImageSource = fileName };
                    product.ProductImages.Add(productImage);
                }



                _repository.SaveProduct(product);

            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View(model);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Attributes(int id)
        {
            _repository.LangId = CurrentLangId;
            var product = _repository.GetProduct(id);
            var productAttributes = _repository.GetProductAttributes(product.CategoryId);
            ViewBag.ProductAttributeValues = product.ProductAttributeValues.ToList();
            ViewBag.ProductId = product.Id;
            return View(productAttributes);
        }



        public ActionResult Delete(int id)
        {
            try
            {
                _repository.DeleteProduct(id, ImageHelper.DeleteImage);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Attributes(int productId, FormCollection form)
        {
            _repository.LangId = CurrentLangId;
            var product = _repository.GetProduct(productId);
            PostCheckboxesData attrData = form.ProcessPostCheckboxesData("attr", "productId");
            PostData staticAttrData = form.ProcessPostData("tb", "productId");

            product.ProductAttributeValues.Clear();
            string searchCriteriaAttributes = "";

            foreach (var kvp in attrData)
            {
                var attributeValueId = kvp.Key;
                bool attributeValue = kvp.Value;

                if (attributeValue)
                {
                    var productAttributeValue = _repository.GetProductAttributeValue(attributeValueId);
                    searchCriteriaAttributes += productAttributeValue.ProductAttributeId + "-" + attributeValueId + ";";
                    product.ProductAttributeValues.Add(productAttributeValue);
                }
            }

            foreach (var kvp in staticAttrData)
            {
                int attributeId = Convert.ToInt32(kvp.Key);
                foreach (var value in kvp.Value)
                {
                    string attributeValue = value.Value;

                    var productAttribute = _repository.GetProductAttribute(attributeId);

                    //productAttribute.ProductAttributeStaticValues.Clear();

                    ProductAttributeStaticValue productAttributeValue = null;
                    productAttributeValue = _repository.GetProductAttributeStaticValue(productAttribute.Id, product.Id);


                    if (string.IsNullOrEmpty(attributeValue))
                    {
                        if (productAttributeValue != null)
                            _repository.DeleteProductAttributeStaticValue(productAttributeValue.Id);
                    }
                    else
                    {
                        if (productAttributeValue == null)
                        {
                            productAttributeValue = new ProductAttributeStaticValue
                            {
                                Title = attributeValue,
                                ProductAttribute = productAttribute,
                                ProductId = product.Id
                            };
                            _repository.AddProductAttributeStaticValue(productAttributeValue);
                        }
                        else
                        {
                            productAttributeValue.Title = attributeValue;
                            _repository.SaveProductAttributeStaticValue(productAttributeValue);
                        }
                    }
                }
            }

            product.SearchCriteriaAttributes = searchCriteriaAttributes;

            _repository.SaveProduct(product);

            return RedirectToAction("Index");

        }
    }
}
