using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Leo.Helpers;
using Leo.Models;
using SiteExtensions;
using SiteExtensions.Graphics;

namespace Leo.Areas.Admin.Controllers
{
    public class ProductTextBlockFileController : AdminController
    {
        private readonly SiteContext _context;

        public ProductTextBlockFileController(SiteContext context)
        {
            _context = context;
        }

        public ActionResult Create(int id)
        {
            return View(new ProductTextBlockFile { ProductTextBlockId = id });
        }

        [HttpPost]
        public ActionResult Create(ProductTextBlockFile model, HttpPostedFileBase image, HttpPostedFileBase filePdf)
        {
            try
            {
                model.Id = 0;
                var product = _context.ProductTextBlocks.First(a => a.Id == model.ProductTextBlockId);
                model.Title = model.Title;
                var cache = new ProductTextBlockFile
                {
                    ProductTextBlock = product,
                    Title = model.Title,
                    
                };

                var file = image;
                if (file != null && !string.IsNullOrEmpty(file.FileName))
                {
                    string fileName = IOHelper.GetUniqueFileName("~/Content/Images", file.FileName);
                    string filePath = Server.MapPath("~/Content/Images");

                    filePath = Path.Combine(filePath, fileName);
                    //GraphicsHelper.SaveOriginalImage(filePath, fileName, file, 500);
                    file.SaveAs(filePath);
                    cache.ImageSource = fileName;
                }

                file = filePdf;
                if (file != null && !string.IsNullOrEmpty(file.FileName))
                {
                    string fileName = IOHelper.GetUniqueFileName("~/Content/Files", file.FileName);
                    string filePath = Server.MapPath("~/Content/Files");

                    filePath = Path.Combine(filePath, fileName);
                    file.SaveAs(filePath);
                    cache.FileName = fileName;
                }


                product.ProductTextBlockFiles.Add(cache);
                _context.SaveChanges();
                

                return RedirectToAction("Index", "Category");
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View(model);
            }
        }


        public ActionResult Edit(int id)
        {
            var f = _context.ProductTextBlockFiles.First(a => a.Id == id);
            return View(f);
        }

        [HttpPost]
        public ActionResult Edit(ProductTextBlockFile model, HttpPostedFileBase image, HttpPostedFileBase filePdf)
        {
            try
            {
                var product = _context.ProductTextBlockFiles.First(a => a.Id == model.Id);
                TryUpdateModel(product, new[] {"Title"});

                var file = image;
                if (file != null && !string.IsNullOrEmpty(file.FileName))
                {
                    ImageHelper.DeleteImage(product.ImageSource);
                    string fileName = IOHelper.GetUniqueFileName("~/Content/Images", file.FileName);
                    string filePath = Server.MapPath("~/Content/Images");

                    filePath = Path.Combine(filePath, fileName);
                    //GraphicsHelper.SaveOriginalImage(filePath, fileName, file, 500);
                    file.SaveAs(filePath);
                    product.ImageSource = fileName;
                }

                file = filePdf;
                if (file != null && !string.IsNullOrEmpty(file.FileName))
                {
                    ImageHelper.DeleteFile(product.FileName);
                    string fileName = IOHelper.GetUniqueFileName("~/Content/Files", file.FileName);
                    string filePath = Server.MapPath("~/Content/Files");

                    filePath = Path.Combine(filePath, fileName);
                    file.SaveAs(filePath);
                    product.FileName = fileName;
                }

                _context.SaveChanges();

                return RedirectToAction("Index", "Category");
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View(model);
            }
        }


        public ActionResult Delete(int id)
        {
            var articleItem = _context.ProductTextBlockFiles.First(a => a.Id == id);
            ImageHelper.DeleteImage(articleItem.ImageSource);
            ImageHelper.DeleteFile(articleItem.FileName);
            _context.ProductTextBlockFiles.Remove(articleItem);
            _context.SaveChanges();
            return RedirectToAction("Index", "Category");
        }



    }
}
