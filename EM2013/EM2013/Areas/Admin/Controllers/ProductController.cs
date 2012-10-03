using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EM2013.Helpers;
using EM2013.Models;
using SiteExtensions;

namespace EM2013.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        public ActionResult Create(int id)
        {
            using (var context = new SiteContext())
            {
                ViewBag.CategoryId = id;
                ViewBag.CategoryName = context.Category.First(c => c.Id == id).Name;
                //var max = context.Product.Max(p => p.SortOrder);
                return View(new Product {/*SortOrder = max + 1,*/Date = DateTime.Now});
            }
        }

        [HttpPost]
        public ActionResult Create(int categoryId, FormCollection form, HttpPostedFileBase fileUpload)
        {
            using (var context = new SiteContext())
            {
                var category = context.Category.First(c => c.Id == categoryId);
                var product = new Product{SortOrder = 0};
                TryUpdateModel(product, new[] { "Name","Date", "Title",/* "SortOrder",*/ "SeoDescription", "SeoKeywords" });
                product.Description = HttpUtility.HtmlDecode(form["Description"]);
                product.Category = category;

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
                return RedirectToAction("Index", "Home", new { area = "", category = category.Name, product = product.Name });
            }
        }



        public ActionResult Edit(int id)
        {
            using (var context = new SiteContext())
            {
                var product = context.Product.Include("Category").First(p => p.Id == id);
                //TryUpdateModel(product, new[] { "Name", "Title", "Description", "SortOrder", "SeoDescription", "SeoKeywords" });

                //context.SaveChanges();
                return View(product);
            }
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection form, HttpPostedFileBase fileUpload)
        {
            using (var context = new SiteContext())
            {
                var product = context.Product.Include("Category").First(p => p.Id == id);
                TryUpdateModel(product, new[] { "Name", "Date", "Title",/* "SortOrder",*/ "SeoDescription", "SeoKeywords" });
                product.Description = HttpUtility.HtmlDecode(form["Description"]);
                if (fileUpload != null)
                {
                    if (!string.IsNullOrEmpty(product.ImageSource))
                    {
                        IOHelper.DeleteFile("~/Content/Images", product.ImageSource);
                    }

                    string fileName = IOHelper.GetUniqueFileName("~/Content/Images", fileUpload.FileName);
                    string filePath = Server.MapPath("~/Content/Images");
                    filePath = Path.Combine(filePath, fileName);
                    //GraphicsHelper.SaveOriginalImage(filePath, fileName, fileUpload);
                    fileUpload.SaveAs(filePath);
                    product.ImageSource = fileName;
                }
                context.SaveChanges();
                return RedirectToAction("Index", "Home", new { area = "", category = product.Category.Name, product=product.Name });
            }
        }

        public ActionResult Delete(int id)
        {
            using (var context = new SiteContext())
            {
                var product = context.Product.Include("Category").Include("ProductItems").First(c => c.Id == id);
                var categoryName = product.Category.Name;
                if (!product.ProductItems.Any())
                {
                    ImageHelper.DeleteImage(product.ImageSource);

                    context.DeleteObject(product);
                    context.SaveChanges();
                }
                return RedirectToAction("Index", "Home", new { area = "", category = categoryName });
            }
        }
    }
}
