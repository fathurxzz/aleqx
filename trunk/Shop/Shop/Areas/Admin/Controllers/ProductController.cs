using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shop.Helpers;
using Shop.Models;

namespace Shop.Areas.Admin.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        //
        // GET: /Admin/Product/

        IQueryable<Product> ApplyPaging(IQueryable<Product> products, int? page, int pageSize)
        {
            if (products == null)
                return null;
            int currentPage = page ?? 0;
            if (page < 0)
                return products;
            return products.Skip(currentPage * pageSize).Take(pageSize);
        }

        public ActionResult Index(int? page)
        {
            using (var context = new ShopContainer())
            {
                //var products = context.Product.Include("Brand").Include("Category").Include("ProductImages").ToList();

                IQueryable<Product> products = context.Product.Include("Brand").Include("Category").Include("ProductImages");
                ViewBag.TotalCount = products.Count();

                products = products.OrderBy(p => p.Id);

                products = ApplyPaging(products, page, SiteSettings.AdminProductsPageSize);
                
                //List<Product> Products = products.ToList();
                ViewBag.Page = page ?? 0;

                return View(products.ToList());
            }
        }

        public ActionResult Prices()
        {
            using (var context = new ShopContainer())
            {
                var products = context.Product.Include("Brand").Include("Category").ToList();
                return View(products);
            }
        }

        [HttpPost]
        public ActionResult Prices(FormCollection form)
        {
            using (var context = new ShopContainer())
            {
                var products = context.Product.Include("Brand").Include("Category").ToList();

                PostCheckboxesData cbDataNew = form.ProcessPostCheckboxesData("new");
                PostCheckboxesData cbDataSpec = form.ProcessPostCheckboxesData("special");
                PostCheckboxesData cbDataPublish = form.ProcessPostCheckboxesData("publish");
                PostData oldPriceData = form.ProcessPostData("oldprice");
                PostData priceData = form.ProcessPostData("price");

                foreach (var kvp in cbDataNew)
                {
                    var productId = kvp.Key;
                    bool productValue = kvp.Value;

                    products.First(p => p.Id == productId).IsNew = productValue;
                }

                foreach (var kvp in cbDataSpec)
                {
                    var productId = kvp.Key;
                    bool productValue = kvp.Value;

                    products.First(p => p.Id == productId).IsSpecialOffer = productValue;
                }

                foreach (var kvp in cbDataPublish)
                {
                    var productId = kvp.Key;
                    bool productValue = kvp.Value;

                    products.First(p => p.Id == productId).Published = productValue;
                }

                foreach (var kvp in oldPriceData)
                {
                    int productId = Convert.ToInt32(kvp.Key);
                    foreach (var value in kvp.Value)
                    {
                        var productValue = Convert.ToDecimal(value.Value);
                        products.First(p => p.Id == productId).OldPrice = productValue;
                    }
                }

                foreach (var kvp in priceData)
                {
                    int productId = Convert.ToInt32(kvp.Key);
                    foreach (var value in kvp.Value)
                    {
                        var productValue = Convert.ToDecimal(value.Value);
                        products.First(p => p.Id == productId).Price = productValue;
                    }
                }

                context.SaveChanges();
            }
            return RedirectToAction("Prices");
        }

        //
        // GET: /Admin/Product/Details/5

        public ActionResult Details(int id)
        {
            using (var context = new ShopContainer())
            {
                var product = context.Product.Include("Brand").Include("Category").Include("ProductAttributeValues").Include("ProductAttributeStaticValues").Include("ProductImages").First(p => p.Id == id);
                product.Tags.Load();
                product.Category.ProductAttributes.Load();
                foreach (var attribute in product.Category.ProductAttributes)
                {
                    attribute.ProductAttributeValues.Load();
                    attribute.ProductAttributeStaticValues.Load();
                }

                return View(product);
            }
        }

        //
        // GET: /Admin/Product/Create

        public ActionResult Create()
        {
            using (var context = new ShopContainer())
            {
                var categories = context.Category.ToList();
                List<SelectListItem> categoryItems = categories.Select(category => new SelectListItem { Text = category.Title, Value = category.Id.ToString() }).ToList();
                ViewBag.Categories = categoryItems;

                var brands = context.Brand.ToList();
                List<SelectListItem> brandItems = brands.Select(b => new SelectListItem { Text = b.Title, Value = b.Id.ToString() }).ToList();
                ViewBag.Brands = brandItems;
                return View(new Product());
            }
        }

        //
        // POST: /Admin/Product/Create

        [HttpPost]
        public ActionResult Create(int categoryId, int brandId, FormCollection form)
        {
            try
            {
                using (var context = new ShopContainer())
                {
                    var category = context.Category.First(c => c.Id == categoryId);
                    var brand = context.Brand.First(b => b.Id == brandId);
                    var product = new Product
                                      {
                                          Category = category,
                                          Brand = brand
                                      };
                    TryUpdateModel(product,
                        new[]
                        {
                            "Title",
                            "SortOrder",
                            "Price", 
                            "OldPrice",
                            "IsNew",
                            "IsSpecialOffer",
                            "Published",
                            "SeoDescription",
                            "SeoKeywords",
                            "Articul"
                        });

                    string[] x = form["Name"].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var s in x)
                    {
                        product.Name += s[0].ToString().ToUpper() + s.Substring(1);
                    }

                    product.ShortDescription = HttpUtility.HtmlDecode(form["ShortDescription"]);
                    product.Description = HttpUtility.HtmlDecode(form["Description"]);

                    context.AddToProduct(product);
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Admin/Product/Edit/5

        public ActionResult Edit(int id)
        {
            using (var context = new ShopContainer())
            {
                var product = context.Product.Include("Category").Include("Brand").First(p => p.Id == id);
                var categories = context.Category.ToList();
                List<SelectListItem> categoryItems = categories.Select(category => new SelectListItem { Text = category.Title, Value = category.Id.ToString(), Selected = category.Id == product.CategoryId }).ToList();
                ViewBag.Categories = categoryItems;

                var brands = context.Brand.ToList();
                List<SelectListItem> brandItems = brands.Select(b => new SelectListItem { Text = b.Title, Value = b.Id.ToString(), Selected = b.Id == product.BrandId }).ToList();
                ViewBag.Brands = brandItems;

                return View(product);
            }
        }

        //
        // POST: /Admin/Product/Edit/5

        [HttpPost]
        public ActionResult Edit(int brandId, int categoryId, int id, FormCollection form)
        {
            try
            {
                using (var context = new ShopContainer())
                {
                    var brand = context.Brand.First(b => b.Id == brandId);
                    var category = context.Category.First(c => c.Id == categoryId);

                    var product = context.Product.Include("ProductAttributeValues").First(p => p.Id == id);
                    product.Brand = brand;

                    if (product.Category.Id != category.Id)
                    {
                        product.ProductAttributeValues.Clear();
                        product.Category = category;
                    }


                    TryUpdateModel(product,
                        new[]
                        {
                            "Name",
                            "Title",
                            "SortOrder",
                            "Price", 
                            "OldPrice",
                            "IsNew",
                            "IsSpecialOffer",
                            "Published",
                            "SeoDescription",
                            "SeoKeywords",
                            "Articul"
                        });

                    product.ShortDescription = HttpUtility.HtmlDecode(form["ShortDescription"]);
                    product.Description = HttpUtility.HtmlDecode(form["Description"]);

                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Admin/Product/Delete/5


        public ActionResult Images(int id)
        {
            using (var context = new ShopContainer())
            {
                var product = context.Product.Include("ProductImages").First(p => p.Id == id);
                ViewBag.ProductId = product.Id;
                ViewBag.ProductTitle = product.Title;
                return View(product.ProductImages);
            }
        }

        [HttpPost]
        public ActionResult Images(int productId, FormCollection form)
        {
            using (var context = new ShopContainer())
            {
                var product = context.Product.Include("ProductImages").First(p => p.Id == productId);
                ViewBag.ProductTitle = product.Title;
                ViewBag.ProductId = product.Id;

                if (!string.IsNullOrEmpty(form["r_default"]))
                {
                    var defaultImageId = Convert.ToInt32(form["r_default"]);
                    product.ProductImages.ToList().ForEach(p => p.Default = false);
                    product.ProductImages.First(image => image.Id == defaultImageId).Default = true;
                    context.SaveChanges();
                }

                return View(product.ProductImages);
            }

        }

        public ActionResult AddImage(int productId)
        {
            ViewBag.ProductId = productId;
            return View();
        }

        [HttpPost]
        public ActionResult AddImage(int productId, FormCollection form, IEnumerable<HttpPostedFileBase> uploadFiles)
        {
            using (var context = new ShopContainer())
            {
                var product = context.Product.First(p => p.Id == productId);

                foreach (var file in uploadFiles)
                {
                    if (file == null) continue;
                    string fileName = IOHelper.GetUniqueFileName("~/Content/Images", file.FileName);
                    string filePath = Server.MapPath("~/Content/Images");
                    filePath = Path.Combine(filePath, fileName);
                    file.SaveAs(filePath);
                    product.ProductImages.Add(new ProductImage { ImageSource = fileName });
                }
                context.SaveChanges();
            }
            return RedirectToAction("Images", new { id = productId });
        }

        public ActionResult DeleteImage(int id, int productId)
        {
            using (var context = new ShopContainer())
            {
                var image = context.ProductImage.First(i => i.Id == id);
                DeleteImage(image, context);
                context.SaveChanges();
                return RedirectToAction("Images", new { id = productId });
            }
        }

        private void DeleteImage(ProductImage image, ShopContainer context)
        {
            IOHelper.DeleteFile("~/Content/Images", image.ImageSource);

            foreach (var folder in GraphicsHelper.ThumbnailFolders)
            {
                IOHelper.DeleteFile("~/ImageCache/" + folder, image.ImageSource);
            }

            //IOHelper.DeleteFile("~/ImageCache/thumbnail0", image.ImageSource);
            //IOHelper.DeleteFile("~/ImageCache/thumbnail1", image.ImageSource);
            context.DeleteObject(image);
        }

        public ActionResult Delete(int id)
        {
            using (var context = new ShopContainer())
            {
                var product = context.Product.Include("ProductAttributeValues").Include("ProductAttributeStaticValues").First(p => p.Id == id);
                product.ProductAttributeValues.Clear();

                while (product.ProductAttributeStaticValues.Any())
                {
                    var pav = product.ProductAttributeStaticValues.First();
                    context.DeleteObject(pav);
                }

                while (product.ProductImages.Any())
                {
                    var image = product.ProductImages.First();
                    DeleteImage(image, context);
                }

                product.Tags.Clear();
                context.DeleteObject(product);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        //
        // POST: /Admin/Product/Delete/5

        public ActionResult Attributes(int id)
        {
            using (var context = new ShopContainer())
            {
                var product = context.Product.Include("Category").Include("ProductAttributeValues").Include("ProductAttributeStaticValues").First(p => p.Id == id);
                product.Category.ProductAttributes.Load();
                foreach (var attribute in product.Category.ProductAttributes)
                {
                    attribute.ProductAttributeValues.Load();
                    attribute.ProductAttributeStaticValues.Load();
                }

                ViewBag.ProductTitle = product.Title;
                ViewBag.ProductId = product.Id;

                ViewBag.ProductAttributeValues = product.ProductAttributeValues.ToList();

                return View(product.Category.ProductAttributes);
            }
        }

        [HttpPost]
        public ActionResult Attributes(int productId, FormCollection form)
        {
            using (var context = new ShopContainer())
            {
                var product = context.Product.Include("ProductAttributeValues").First(p => p.Id == productId);

                PostCheckboxesData cbData = form.ProcessPostCheckboxesData("attr", "productId");
                PostData staticAttrData = form.ProcessPostData("tb", "productId");

                product.ProductAttributeValues.Clear();

                foreach (var kvp in cbData)
                {
                    var attributeValueId = kvp.Key;
                    bool attributeValue = kvp.Value;

                    if (attributeValue)
                    {
                        var productAttributeValue = context.ProductAttributeValues.First(pv => pv.Id == attributeValueId);
                        product.ProductAttributeValues.Add(productAttributeValue);
                    }
                }


                foreach (var kvp in staticAttrData)
                {
                    int attributeId = Convert.ToInt32(kvp.Key);
                    foreach (var value in kvp.Value)
                    {
                        string attributeValue = value.Value;

                        var productAttribute = context.ProductAttribute.Include("ProductAttributeStaticValues").First(pa => pa.Id == attributeId);

                        //productAttribute.ProductAttributeStaticValues.Clear();

                        ProductAttributeStaticValues productAttributeValue = null;
                        productAttributeValue = context.ProductAttributeStaticValues.FirstOrDefault(
                                pav => pav.ProductAttribute.Id == productAttribute.Id && pav.Product.Id == product.Id);


                        if (string.IsNullOrEmpty(attributeValue))
                        {
                            if (productAttributeValue != null)
                                context.DeleteObject(productAttributeValue);
                        }
                        else
                        {
                            if (productAttributeValue == null)
                            {



                                productAttributeValue = new ProductAttributeStaticValues
                                                            {
                                                                Value = attributeValue,
                                                                ProductAttribute = productAttribute
                                                            };
                                product.ProductAttributeStaticValues.Add(productAttributeValue);

                            }
                            else
                            {
                                productAttributeValue.Value = attributeValue;
                            }
                        }
                        //productAttribute.ProductAttributeValues.Add(productAttributeValue);


                    }
                }

                context.SaveChanges();




                return RedirectToAction("Index");
            }
        }

        public ActionResult Tags(int id)
        {
            using (var context = new ShopContainer())
            {
                var product = context.Product.Include("Tags").First(p => p.Id == id);
                var tags = context.Tag.ToList();
                ViewBag.ProductTags = product.Tags;
                ViewBag.ProductId = product.Id;
                ViewBag.ProductTitle = product.Title;
                return View(tags);
            }
        }

        
        [HttpPost]
        public ActionResult Tags(int productId, FormCollection form)
        {
            using (var context = new ShopContainer())
            {
                var product = context.Product.First(p => p.Id == productId);
                PostCheckboxesData cbData = form.ProcessPostCheckboxesData("attr", "productId");
                product.Tags.Clear();
                foreach (KeyValuePair<int, bool> kvp in cbData)
                {
                    if (kvp.Value)
                    {
                        var tagId = kvp.Key;
                        var tag = context.Tag.First(t => t.Id == tagId);
                        product.Tags.Add(tag);
                    }
                }
                context.SaveChanges();

                return RedirectToAction("Index");
            }
        }
    }
}
