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
    [Authorize]
    public class ProductItemController : Controller
    {
        public ActionResult Create(int id)
        {
            using (var context = new SiteContext())
            {
                ViewBag.ProductId = id;
                int max=0;
                if(context.ProductItem.Any())
                max = context.ProductItem.Max(p => p.SortOrder);
                var product = context.Product.Include("Category").First(p => p.Id == id);
                ViewBag.ProductName = product.Name;
                ViewBag.CategoryName = product.Category.Name;

                return View(new ProductItem { SortOrder = max + 1 });
            }
        } 

        [HttpPost]
        public ActionResult Create(int productId, FormCollection collection, HttpPostedFileBase fileUpload)
        {
            using (var context = new SiteContext())
            {
                var product = context.Product.Include("Category").First(c => c.Id == productId);
                
                var pi = new ProductItem();
                TryUpdateModel(pi, new[] { "Description", "SortOrder" });
                pi.Product = product;

                if (fileUpload != null)
                {
                    string fileName = IOHelper.GetUniqueFileName("~/Content/Images", fileUpload.FileName);
                    string filePath = Server.MapPath("~/Content/Images");
                    filePath = Path.Combine(filePath, fileName);
                    //GraphicsHelper.SaveOriginalImage(filePath, fileName, fileUpload);
                    fileUpload.SaveAs(filePath);
                    pi.ImageSource = fileName;
                }

                context.AddToProductItem(pi);
                context.SaveChanges();
                return RedirectToAction("Index", "Home", new { area = "", category =product.Category.Name, product = product.Name });
            }
        }
        
        public ActionResult Edit(int id)
        {
            using (var context = new SiteContext())
            {
                var pi = context.ProductItem.Include("Product").First(p => p.Id == id);
                var productId = pi.Product.Id;
                var product = context.Product.Include("Category").First(p => p.Id == productId);
                ViewBag.ProductName = product.Name;
                ViewBag.CategoryName = product.Category.Name;
                return View(pi);
            }
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection, HttpPostedFileBase fileUpload)
        {
            try
            {
                using (var context = new SiteContext())
                {
                    var pi = context.ProductItem.Include("Product").First(p => p.Id == id);
                    var productId = pi.Product.Id;
                    var product = context.Product.Include("Category").First(p => p.Id == productId);
                    TryUpdateModel(pi, new[] { "Description", "SortOrder" });
                    if (fileUpload != null)
                    {
                        if (!string.IsNullOrEmpty(pi.ImageSource))
                        {
                            ImageHelper.DeleteImage(pi.ImageSource);
                        }

                        string fileName = IOHelper.GetUniqueFileName("~/Content/Images", fileUpload.FileName);
                        string filePath = Server.MapPath("~/Content/Images");
                        filePath = Path.Combine(filePath, fileName);
                        //GraphicsHelper.SaveOriginalImage(filePath, fileName, fileUpload);
                        fileUpload.SaveAs(filePath);
                        pi.ImageSource = fileName;
                    }
                    context.SaveChanges();

                    return RedirectToAction("Index", "Home", new { area = "", category = product.Category.Name, product = pi.Product.Name });
                }
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            using (var context = new SiteContext())
            {
                var pi = context.ProductItem.Include("Product").First(p => p.Id == id);
                var productId = pi.Product.Id;
                var product = context.Product.Include("Category").First(p => p.Id == productId);

                ImageHelper.DeleteImage(pi.ImageSource);
                context.DeleteObject(pi);
                context.SaveChanges();

                return RedirectToAction("Index", "Home", new { area = "", category = product.Category.Name, product = product.Name });
            }
        }
    }
}
