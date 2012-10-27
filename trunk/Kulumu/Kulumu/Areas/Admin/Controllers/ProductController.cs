using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kulumu.Helpers;
using Kulumu.Models;
using SiteExtensions;

namespace Kulumu.Areas.Admin.Controllers
{
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

        public ActionResult Create(int? categoryId)
        {
            using (var context = new SiteContainer())
            {
                var categories = context.Category.ToList();
                ViewBag.Categories = categories.Select(category => new SelectListItem {Text = category.Title, Value = category.Id.ToString()}).ToList();
                if(categoryId.HasValue)
                {
                    ViewBag.categoryId = categoryId;
                }
                return View();
            }
        } 

        [HttpPost]
        public ActionResult Create(int categoryId, FormCollection form, HttpPostedFileBase fileUpload)
        {
            try
            {
                using (var context = new SiteContainer())
                {
                    var category = context.Category.First(c => c.Id == categoryId);
                    var product = new Product{Category = category};
                    TryUpdateModel(product, new[] {"Title", "Discount", "DiscountText", "Price"});

                    product.Description = HttpUtility.HtmlDecode(form["Description"]);
                    if (fileUpload != null)
                    {
                        //if (!string.IsNullOrEmpty(product.ImageSource))
                        //{

                        //    IOHelper.DeleteFile("~/Content/Images", product.ImageSource);
                        //    foreach (var thumbnail in SiteSettings.Thumbnails)
                        //    {
                        //        IOHelper.DeleteFile("~/ImageCache/" + thumbnail.Key, product.ImageSource);
                        //    }
                        //}
                        string fileName = IOHelper.GetUniqueFileName("~/Content/Images", fileUpload.FileName);
                        string filePath = Server.MapPath("~/Content/Images");
                        filePath = Path.Combine(filePath, fileName);
                        fileUpload.SaveAs(filePath);
                        product.ImageSource = fileName;
                    }

                    context.AddToProduct(product);

                    context.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        public ActionResult Edit(int id)
        {
            using (var context = new SiteContainer())
            {
                var categories = context.Category.ToList();
                ViewBag.Categories = categories.Select(category => new SelectListItem { Text = category.Title, Value = category.Id.ToString() }).ToList();

                var product = context.Product.First(p => p.Id == id);
                return View(product);
            }
        }

        [HttpPost]
        public ActionResult Edit(int id, int categoryId, FormCollection form, HttpPostedFileBase fileUpload)
        {
            try
            {
                using (var context = new SiteContainer())
                {
                    var category = context.Category.First(c => c.Id == categoryId);
                    var product = context.Product.Include("Category").First(p => p.Id == id);
                    product.Category = category;
                    TryUpdateModel(product, new[] { "Title", "Discount", "DiscountText", "Price" });

                    product.Description = HttpUtility.HtmlDecode(form["Description"]);
                    if (fileUpload != null)
                    {
                        if (!string.IsNullOrEmpty(product.ImageSource))
                        {

                            IOHelper.DeleteFile("~/Content/Images", product.ImageSource);
                            foreach (var thumbnail in SiteSettings.Thumbnails)
                            {
                                IOHelper.DeleteFile("~/ImageCache/" + thumbnail.Key, product.ImageSource);
                            }
                        }
                        string fileName = IOHelper.GetUniqueFileName("~/Content/Images", fileUpload.FileName);
                        string filePath = Server.MapPath("~/Content/Images");
                        filePath = Path.Combine(filePath, fileName);
                        fileUpload.SaveAs(filePath);
                        product.ImageSource = fileName;
                    }
                    context.SaveChanges();
                }
                return RedirectToAction("Index");
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
                var product = context.Product.First(p => p.Id == id);
                ImageHelper.DeleteImage(product.ImageSource);
                context.DeleteObject(product);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
        }

    }
}
