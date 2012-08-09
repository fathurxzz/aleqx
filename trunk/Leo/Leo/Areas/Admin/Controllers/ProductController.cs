using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Leo.Models;
using SiteExtensions;

namespace Leo.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        //
        // GET: /Admin/Product/Create

        public ActionResult Create(int id)
        {
            using (var context = new SiteContainer())
            {
                var category = context.Category.Include("ProductAttributes").First(c => c.Id == id);
                ViewBag.CategoryId = category.Id;
                ViewBag.Attributes = category.ProductAttributes;
                ViewBag.CategoryName = category.Name;
                return View();
            }
        }

        //
        // POST: /Admin/Product/Create

        [HttpPost]
        public ActionResult Create(FormCollection form, int categoryId, HttpPostedFileBase fileUpload)
        {
            using (var context = new SiteContainer())
            {
                Product product = new Product();
                Category category = context.Category.First(c => c.Id == categoryId);
                product.Category = category;

                var attributes = context.ProductAttribute.ToList();
                PostCheckboxesData postData = form.ProcessPostCheckboxesData("attr", "categoryId");

                foreach (var kvp in postData)
                {
                    var attribute = attributes.First(a => a.Id == kvp.Key);
                    if (kvp.Value)
                    {
                        if (!product.ProductAttributes.Contains(attribute))
                            product.ProductAttributes.Add(attribute);
                    }
                    else
                    {
                        if (product.ProductAttributes.Contains(attribute))
                            product.ProductAttributes.Remove(attribute);
                    }
                }


                TryUpdateModel(product, new[] { "Title", "Description" });

                if (fileUpload != null)
                {
                    string fileName = IOHelper.GetUniqueFileName("~/Content/Images", fileUpload.FileName);
                    string filePath = Server.MapPath("~/Content/Images");
                    filePath = Path.Combine(filePath, fileName);
                    //GraphicsHelper.SaveOriginalImage(filePath, fileName, fileUpload);
                    fileUpload.SaveAs(filePath);

                    product.ImageSource = fileName;
                }

                context.AddToProduct(product);

                context.SaveChanges();

                return RedirectToAction("Index", "Catalogue", new { Area = "", id = category.Name });
            }
        }
        
        //
        // GET: /Admin/Product/Edit/5

        public ActionResult Edit(int id)
        {
            using (var context = new SiteContainer())
            {
                ViewBag.productId = id;
                var product = context.Product.Include("ProductAttributes").Include("Category").First(p => p.Id == id);
                var category = context.Category.Include("ProductAttributes").First(c => c.Id == product.Category.Id);
                ViewBag.CategoryName = category.Name;
                ViewBag.CategoryId = category.Id;
                ViewBag.Attributes = category.ProductAttributes;

                return View(product);
            }
        }

        //
        // POST: /Admin/Product/Edit/5

        [HttpPost]
        public ActionResult Edit(FormCollection form, int id, HttpPostedFileBase fileUpload)
        {
            using (var context = new SiteContainer())
            {
                var product = context.Product.Include("ProductAttributes").Include("Category").First(p => p.Id == id);
                var category = context.Category.First(c => c.Id == product.Category.Id);


                var attributes = context.ProductAttribute.ToList();
                PostCheckboxesData postData = form.ProcessPostCheckboxesData("attr", "categoryId");

                foreach (var kvp in postData)
                {
                    var attribute = attributes.First(a => a.Id == kvp.Key);
                    if (kvp.Value)
                    {
                        if (!product.ProductAttributes.Contains(attribute))
                            product.ProductAttributes.Add(attribute);
                    }
                    else
                    {
                        if (product.ProductAttributes.Contains(attribute))
                            product.ProductAttributes.Remove(attribute);
                    }
                }
                
                TryUpdateModel(product, new[] { "Title", "Description"});




                if (fileUpload != null)
                {
                    if (!string.IsNullOrEmpty(product.ImageSource))
                    {
                        IOHelper.DeleteFile("~/Content/Images", product.ImageSource);
                        IOHelper.DeleteFile("~/ImageCache/galleryThumbnail", product.ImageSource);
                    }

                    string fileName = IOHelper.GetUniqueFileName("~/Content/Images", fileUpload.FileName);
                    string filePath = Server.MapPath("~/Content/Images");
                    filePath = Path.Combine(filePath, fileName);
                    //GraphicsHelper.SaveOriginalImage(filePath, fileName, fileUpload);
                    fileUpload.SaveAs(filePath);
                    product.ImageSource = fileName;
                }

                context.SaveChanges();

                return RedirectToAction("Index", "Catalogue", new { Area = "", id = category.Name });
            }
        }

        public ActionResult Delete(int id)
        {
            using (var context = new SiteContainer())
            {
                var product = context.Product.Include("Category").First(p => p.Id == id);
                var categoryId = product.Category.Id;
                var category = context.Category.First(c => c.Id == categoryId);

                if (!string.IsNullOrEmpty(product.ImageSource))
                {
                    IOHelper.DeleteFile("~/Content/Images", product.ImageSource);
                    IOHelper.DeleteFile("~/ImageCache/galleryThumbnail", product.ImageSource);
                }
                product.ProductAttributes.Clear();
                context.DeleteObject(product);
                context.SaveChanges();

                return RedirectToAction("Index", "Catalogue", new { Area = "", id = category.Name });
            }
        }
    }
}
