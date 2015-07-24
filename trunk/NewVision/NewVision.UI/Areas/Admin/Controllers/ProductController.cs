using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewVision.UI.Helpers;
using NewVision.UI.Models;

namespace NewVision.UI.Areas.Admin.Controllers
{
    public class ProductController : AdminController
    {
        private readonly SiteContext _context;

        public ProductController(SiteContext context)
        {
            _context = context;
        }

        //
        // GET: /Admin/Product/

        public ActionResult Index()
        {
            var products = _context.Products.ToList();
            return View(products);
        }

        //
        // GET: /Admin/Product/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Admin/Product/Create

        public ActionResult Create(int? id)
        {
            var authors = _context.Authors.ToList();
            var author = authors.FirstOrDefault(a => a.Id == id || id == null);
            ViewBag.AuthorId = author != null ? author.Id : 0;
            ViewBag.Tags = _context.Tags.ToList();
            ViewBag.Authors = authors;
            return View(new Product { SortOrder = (_context.Products.Max(c => (int?)c.SortOrder) ?? 0) + 1 });
        }

        //
        // POST: /Admin/Product/Create

        [HttpPost]
        public ActionResult Create(Product model, FormCollection form, HttpPostedFileBase photo)
        {
            try
            {
                var authors = _context.Authors.ToList();
                var author = authors.First(a => a.Id == model.AuthorId);
                ViewBag.AuthorId = author.Id;
                ViewBag.Tags = _context.Tags.ToList();
                ViewBag.Authors = authors;

                var product = new Product
                {
                    Title = model.Title,
                    TitleEn = model.TitleEn,
                    TitleUa = model.TitleUa,
                    Price = model.Price,
                    SortOrder = model.SortOrder,
                    Author = author
                };

                if (photo != null)
                {
                    string fileName = IOHelper.GetUniqueFileName("~/Content/Images/product", photo.FileName);
                    string filePath = Server.MapPath("~/Content/Images/product");
                    string filePathThumb = Server.MapPath("~/Content/Images/product/thumb");
                    filePath = Path.Combine(filePath, fileName);
                    filePathThumb = Path.Combine(filePathThumb, fileName);
                    //GraphicsHelper.SaveOriginalImage(filePath, fileName, photo, 670);
                    GraphicsHelper.SaveOriginalImageWithDefinedDimentions(filePath, fileName, photo, 1440, 960, ScaleMode.Crop);
                    GraphicsHelper.SaveOriginalImageWithDefinedDimentions(filePathThumb, fileName, photo, 324, 324, ScaleMode.Crop);
                    product.ImageSrc = fileName;
                }

                PostCheckboxesData attrData = form.ProcessPostCheckboxesData("tag");

                foreach (var kvp in attrData)
                {
                    var tagId = kvp.Key;
                    bool tagValue = kvp.Value;

                    //author.Tags.Clear();

                    if (tagValue)
                    {
                        var tag = _context.Tags.First(t => t.Id == tagId);
                        product.Tags.Add(tag);
                    }
                }

                _context.Products.Add(product);
                _context.SaveChanges();


                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.GetEntityValidationException();

                if (string.IsNullOrEmpty((string)TempData["errorMessage"]))
                {
                    TempData["errorMessage"] = ex.Message;
                }
                return View(model);
            }
        }

        //
        // GET: /Admin/Product/Edit/5

        public ActionResult Edit(int id)
        {
            var product = _context.Products.First(p => p.Id == id);
            var authors = _context.Authors.ToList();
            var author = product.Author;
            ViewBag.AuthorId = author.Id;
            ViewBag.Tags = _context.Tags.ToList();
            ViewBag.Authors = authors;
            return View(product);
        }

        //
        // POST: /Admin/Product/Edit/5

        [HttpPost]
        public ActionResult Edit(Product model, int id, FormCollection form, HttpPostedFileBase photo)
        {
            try
            {
                var product = _context.Products.First(p => p.Id == id);

                var authors = _context.Authors.ToList();
                var author = _context.Authors.First(a=>a.Id==model.AuthorId);
                ViewBag.AuthorId = author.Id;
                ViewBag.Tags = _context.Tags.ToList();
                ViewBag.Authors = authors;


                product.Title = model.Title;
                product.TitleEn = model.TitleEn;
                product.TitleUa = model.TitleUa;
                product.Price = model.Price;
                product.SortOrder = model.SortOrder;
                product.Author = author;

                if (photo != null)
                {
                    if (!string.IsNullOrEmpty(product.ImageSrc))
                    {
                        ImageHelper.DeleteImage(product.ImageSrc, "~/Content/Images/product");
                        ImageHelper.DeleteImage(product.ImageSrc, "~/Content/Images/product/thumb");
                    }

                    string fileName = IOHelper.GetUniqueFileName("~/Content/Images/product", photo.FileName);
                    string filePath = Server.MapPath("~/Content/Images/product");
                    string filePathThumb = Server.MapPath("~/Content/Images/product/thumb");
                    filePath = Path.Combine(filePath, fileName);
                    filePathThumb = Path.Combine(filePathThumb, fileName);
                    //GraphicsHelper.SaveOriginalImage(filePath, fileName, photo, 670);
                    GraphicsHelper.SaveOriginalImageWithDefinedDimentions(filePath, fileName, photo, 1440, 960, ScaleMode.Crop);
                    GraphicsHelper.SaveOriginalImageWithDefinedDimentions(filePathThumb, fileName, photo, 324, 324, ScaleMode.Crop);
                    product.ImageSrc = fileName;
                }

                PostCheckboxesData attrData = form.ProcessPostCheckboxesData("tag");
                product.Tags.Clear();
                foreach (var kvp in attrData)
                {
                    var tagId = kvp.Key;
                    bool tagValue = kvp.Value;

                    if (tagValue)
                    {
                        var tag = _context.Tags.First(t => t.Id == tagId);
                        product.Tags.Add(tag);
                    }
                }

                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.GetEntityValidationException();

                if (string.IsNullOrEmpty((string)TempData["errorMessage"]))
                {
                    TempData["errorMessage"] = ex.Message;
                }
                return View(model);
            }
        }

        //
        // GET: /Admin/Product/Delete/5

        public ActionResult Delete(int id)
        {
            try
            {
                var product = _context.Products.First(e => e.Id == id);
                ImageHelper.DeleteImage(product.ImageSrc, "~/Content/Images/product");
                ImageHelper.DeleteImage(product.ImageSrc, "~/Content/Images/product/thumb");

                product.Tags.Clear();

                _context.Products.Remove(product);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.GetEntityValidationException();

                if (string.IsNullOrEmpty((string)TempData["errorMessage"]))
                {
                    TempData["errorMessage"] = ex.Message;
                }
                return RedirectToAction("Index");
            }


        }

    }
}
