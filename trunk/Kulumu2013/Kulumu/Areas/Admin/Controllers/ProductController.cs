using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kulumu.Helpers;
using Kulumu.Models;
using SiteExtensions;
using SiteExtensions.Graphics;

namespace Kulumu.Areas.Admin.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        public ActionResult Index()
        {
            using (var context = new SiteContainer())
            {
                var products = context.Product.Include("Category").ToList();
                return View(products);
            }
        }

        public ActionResult Create(int id)
        {
            using (var context = new SiteContainer())
            {
                var category = context.Category.First(c => c.Id == id);

                var product = new Product { Category = category };
                //ViewBag.Categories = categories.Select(category => new SelectListItem {Text = category.Title, Value = category.Id.ToString()}).ToList();
                ViewBag.categoryId = id;
                return View(product);
            }
        }

        [HttpPost]
        public ActionResult Create(int categoryId, FormCollection form)
        {
            try
            {
                using (var context = new SiteContainer())
                {
                    var category = context.Category.First(c => c.Id == categoryId);
                    var product = new Product { Category = category, ImageSource = "" };
                    TryUpdateModel(product, new[] { "Title", "Discount", "Price", "Structure", "Consistence", "Producer", "Nap", "PageTitle" });

                    product.Description = HttpUtility.HtmlDecode(form["Description"]);
                    product.DiscountText = HttpUtility.HtmlDecode(form["DiscountText"]);

                    for (int i = 0; i < Request.Files.Count; i++)
                    {

                        var file = Request.Files[i];

                        if (file == null) continue;
                        if (string.IsNullOrEmpty(file.FileName)) continue;

                        var pi = new ProductImage();
                        string fileName = IOHelper.GetUniqueFileName("~/Content/Images", file.FileName);
                        string filePath = Server.MapPath("~/Content/Images");


                        filePath = Path.Combine(filePath, fileName);
                        GraphicsHelper.SaveOriginalImage(filePath, fileName, file, 1500);
                        //file.SaveAs(filePath);

                        pi.ImageSource = fileName;
                        product.ProductImages.Add(pi);
                        if (string.IsNullOrEmpty(product.ImageSource))
                            product.ImageSource = pi.ImageSource;
                    }

                    context.AddToProduct(product);
                    context.SaveChanges();

                    if (category.SpecialCategory)
                    {
                        return RedirectToAction("OurWorks", "Home", new { area = "" });
                    }

                    return RedirectToAction("Gallery", "Home", new { area = "", id = category.Name });
                }
            }
            catch (Exception ex)
            {
                return View(ViewBag.ErrorMessage = ex.Message);
            }
        }

        public ActionResult Edit(int id)
        {
            using (var context = new SiteContainer())
            {
                var categories = context.Category.ToList();
                ViewBag.Categories =
                    categories.Select(
                        category => new SelectListItem { Text = category.Title, Value = category.Id.ToString() }).ToList();

                var product = context.Product.First(p => p.Id == id);
                return View(product);
            }
        }

        [HttpPost]
        public ActionResult Edit(int id, int categoryId, FormCollection form)
        {
            try
            {
                using (var context = new SiteContainer())
                {
                    var category = context.Category.First(c => c.Id == categoryId);
                    var product = context.Product.Include("Category").First(p => p.Id == id);
                    product.Category = category;
                    TryUpdateModel(product, new[] { "Title", "Discount", "Price", "Structure", "Consistence", "Producer", "Nap", "PageTitle" });

                    product.Description = HttpUtility.HtmlDecode(form["Description"]);
                    product.DiscountText = HttpUtility.HtmlDecode(form["DiscountText"]);

                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        var file = Request.Files[i];
                        if (file == null) continue;
                        if (string.IsNullOrEmpty(file.FileName)) continue;

                        var pi = new ProductImage();
                        string fileName = IOHelper.GetUniqueFileName("~/Content/Images", file.FileName);
                        string filePath = Server.MapPath("~/Content/Images");
                        filePath = Path.Combine(filePath, fileName);
                        GraphicsHelper.SaveOriginalImage(filePath, fileName, file, 1500);
                        //file.SaveAs(filePath);
                        pi.ImageSource = fileName;
                        product.ProductImages.Add(pi);
                        if (string.IsNullOrEmpty(product.ImageSource))
                            product.ImageSource = pi.ImageSource;
                    }

                    context.SaveChanges();
                    if (category.SpecialCategory)
                    {
                        return RedirectToAction("OurWorks", "Home", new { area = "" });
                    }
                    return RedirectToAction("Gallery", "Home", new { area = "", id = category.Name });
                }
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id, int? page)
        {
            using (var context = new SiteContainer())
            {
                var product = context.Product.Include("Category").Include("ProductImages").First(p => p.Id == id);
                var categoryName = product.Category.Name;
                var specialCategory = product.Category.SpecialCategory;
                while (product.ProductImages.Any())
                {
                    var productImage = product.ProductImages.First();
                    ImageHelper.DeleteImage(productImage.ImageSource);
                    context.DeleteObject(productImage);
                }

                ImageHelper.DeleteImage(product.ImageSource);
                context.DeleteObject(product);
                context.SaveChanges();

                if (specialCategory)
                {
                    return RedirectToAction("OurWorks", "Home", new { area = "" });
                }

                return RedirectToAction("Gallery", "Home", new { area = "", id = categoryName });
            }
        }

        public ActionResult SetCoverImage(int id)
        {
            using (var context = new SiteContainer())
            {
                var productImage = context.ProductImage.Include("Product").First(pi => pi.Id == id);
                productImage.Product.ImageSource = productImage.ImageSource;
                var productId = productImage.Product.Id;
                context.SaveChanges();

                var product = context.Product.Include("Category").First(p => p.Id == productId);
                if (product.Category.SpecialCategory)
                {
                    return RedirectToAction("WorkDetails", "Home", new { area = "", id = product.Id });
                }
                return RedirectToAction("ProductDetails", "Home", new { area = "", id = productImage.Product.Id });
            }
        }

        public ActionResult DeleteProductImage(int id)
        {
            using (var context = new SiteContainer())
            {
                var productImage = context.ProductImage.Include("Product").First(pi => pi.Id == id);
                var productId = productImage.Product.Id;

                //if (productImage.Product.ImageSource == productImage.ImageSource)
                //{

                //}


                ImageHelper.DeleteImage(productImage.ImageSource);
                context.DeleteObject(productImage);
                context.SaveChanges();

                return RedirectToAction("ProductDetails", "Home", new { area = "", id = productId });
            }
        }


        public ActionResult AddProductSize(int productId)
        {
            ViewBag.productId = productId;
            return View();
        }

        [HttpPost]
        public ActionResult AddProductSize(int productId, FormCollection form)
        {
            using (var context = new SiteContainer())
            {
                var product = context.Product.First(c => c.Id == productId);
                var ps = new ProductSize();
                TryUpdateModel(ps, new[] { "Size" });
                product.ProductSizes.Add(ps);
                context.SaveChanges();
                return RedirectToAction("ProductDetails", "Home", new { area = "", id = productId });
            }
        }

        public ActionResult EditProductSize(int id)
        {
            using (var context = new SiteContainer())
            {
                var productSize = context.ProductSize.Include("Product").First(ps => ps.Id == id);
                ViewBag.productId = productSize.Product.Id;
                return View(productSize);
            }
        }

        [HttpPost]
        public ActionResult EditProductSize(int id, FormCollection form)
        {
            using (var context = new SiteContainer())
            {
                var productSize = context.ProductSize.Include("Product").First(ps => ps.Id == id);
                TryUpdateModel(productSize, new[] { "Size" });
                context.SaveChanges();
                return RedirectToAction("ProductDetails", "Home", new { area = "", id = productSize.Product.Id });
            }
        }

        public ActionResult DeleteProductSize(int id)
        {
            using (var context = new SiteContainer())
            {
                var productSize = context.ProductSize.Include("Product").First(ps => ps.Id == id);
                var productId = productSize.Product.Id;
                context.DeleteObject(productSize);
                context.SaveChanges();
                return RedirectToAction("ProductDetails", "Home", new { area = "", id = productId });
            }
        }

    }
}
