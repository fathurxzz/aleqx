using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ego.Helpers;
using Ego.Models;
using SiteExtensions;
using SiteExtensions.Graphics;

namespace Ego.Areas.Admin.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        public ActionResult Create()
        {
            using (var context = new SiteContainer())
            {
                int maxSortOrder = context.Product.Max(c => (int?)c.SortOrder) ?? 0;
                return View(new Product { SortOrder = maxSortOrder + 1 });
            }
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection, HttpPostedFileBase fileUpload, HttpPostedFileBase previewFileUpload)
        {
            using (var context = new SiteContainer())
            {
                if (fileUpload != null && previewFileUpload != null)
                {
                    var product = new Product();

                    TryUpdateModel(product, new[] { "SortOrder" });
                    product.Description = HttpUtility.HtmlDecode(collection["Description"]);

                    string fileName = IOHelper.GetUniqueFileName("~/Content/Images", fileUpload.FileName);
                    string filePath = Server.MapPath("~/Content/Images");
                    filePath = Path.Combine(filePath, fileName);
                    GraphicsHelper.SaveOriginalImage(filePath, fileName, fileUpload, 500);
                    product.ImageSource = fileName;

                    fileName = IOHelper.GetUniqueFileName("~/Content/Images", previewFileUpload.FileName);
                    filePath = Server.MapPath("~/Content/Images");
                    filePath = Path.Combine(filePath, fileName);
                    GraphicsHelper.SaveOriginalImage(filePath, fileName, previewFileUpload, 500);
                    product.PreviewImageSource = fileName;

                    context.AddToProduct(product);

                    context.SaveChanges();
                }

                return RedirectToAction("Index", "Home", new { area = "", id = "gallery" });
            }
        }


        public ActionResult CustomCreate()
        {
            using (var context = new SiteContainer())
            {
                int maxSortOrder = context.Product.Max(c => (int?)c.SortOrder) ?? 0;
                return View(new Product { SortOrder = maxSortOrder + 1 });
            }
        }

        [HttpPost]
        public ActionResult CustomCreate(FormCollection form, HttpPostedFileBase files)
        {
            using (var context = new SiteContainer())
            {
                if (files!=null)
                {
                    var product = new Product();
                    TryUpdateModel(product, new[] { "SortOrder"});
                    product.Description = HttpUtility.HtmlDecode(form["Description"]);

                    string fileName = IOHelper.GetUniqueFileName("~/Content/Images", files.FileName);
                    string filePath = Server.MapPath("~/Content/Images");
                    filePath = Path.Combine(filePath, fileName);
                    GraphicsHelper.SaveOriginalImage(filePath, fileName, files, 1200);
                    product.ImageSource = fileName;

                    fileName = IOHelper.GetUniqueFileName("~/Content/Images", files.FileName);
                    filePath = Server.MapPath("~/Content/Images");
                    filePath = Path.Combine(filePath, fileName);
                    int x1 = Convert.ToInt32(form["x1"]);
                    int y1 = Convert.ToInt32(form["y1"]);
                    int w = Convert.ToInt32(form["w"]);
                    int h = Convert.ToInt32(form["h"]);
                    
                    GraphicsHelper.SaveCropPreview(filePath, fileName, files, x1, y1, w, h);
                    product.PreviewImageSource = fileName;


                    context.AddToProduct(product);
                    context.SaveChanges();
                }
            }
            return RedirectToAction("Index", "Home", new { area = "", id = "gallery" });
        }

        public ActionResult Edit(int id)
        {
            using (var context = new SiteContainer())
            {
                var product = context.Product.First(p => p.Id == id);
               
                return View(product);
            }
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            using (var context = new SiteContainer())
            {
                var product = context.Product.First(p => p.Id == id);
                TryUpdateModel(product, new[] { "SortOrder" });
                product.Description = HttpUtility.HtmlDecode(collection["Description"]);
                context.SaveChanges();
                return RedirectToAction("Index", "Home", new { area = "", id = "gallery" });
            }
        }

        public ActionResult Delete(int id)
        {
            using (var context = new SiteContainer())
            {
                var product = context.Product.First(p => p.Id == id);
                ImageHelper.DeleteImage(product.ImageSource);
                ImageHelper.DeleteImage(product.PreviewImageSource);
                context.DeleteObject(product);
                context.SaveChanges();
                return RedirectToAction("Index", "Home", new { area = "", id = "gallery" });
            }
        }


    }
}
