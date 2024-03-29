﻿using System.ComponentModel.DataAnnotations;
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
    public class ProductController : AdminController
    {

        private readonly SiteContext _context;

        public ProductController(SiteContext context)
        {
            _context = context;
        }

        public ActionResult Index(int id)
        {
            var products = _context.Products.Where(p => p.CategoryId == id).ToList();
            return View(products);
        }

        public ActionResult Create(int id, bool isContentPage)
        {
            return View(new Product { CategoryId = id, CurrentLang = CurrentLang.Id, IsContentPage = isContentPage});
        }

        [HttpPost]
        public ActionResult Create(Product model)
        {
            try
            {
                model.Text = HttpUtility.HtmlDecode(model.Text);
                model.Id = 0;
                var category = _context.Categories.First(c => c.Id == model.CategoryId);
                var cache = new Product
                {
                    Name = SiteHelper.UpdatePageWebName(model.Name),
                    SortOrder = model.SortOrder,
                    Category = category,
                    Title = model.Title,
                    Text = model.Text,
                    IsContentPage = model.IsContentPage,
                    
                };

                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var file = Request.Files[i];
                    if (file == null) continue;
                    if (string.IsNullOrEmpty(file.FileName)) continue;
                    var pi = new ProductImage();
                    string fileName = IOHelper.GetUniqueFileName("~/Content/Images", file.FileName);
                    string filePath = Server.MapPath("~/Content/Images");

                    filePath = Path.Combine(filePath, fileName);
                    //file.SaveAs(filePath);
                    GraphicsHelper.SaveOriginalImage(filePath, fileName, file);
                    pi.ImageSource = fileName;
                    cache.ProductImages.Add(pi);
                }


                _context.Products.Add(cache);

                var lang = _context.Languages.FirstOrDefault(p => p.Id == model.CurrentLang);
                if (lang != null)
                {
                    CreateOrChangeContentLang(_context, model, cache, lang);
                }

                return RedirectToAction("Details", "Category", new { id = category.Id });
            }
            catch
            {
                return View(model);
            }
        }

        //
        // GET: /Admin/Product/Edit/5

        public ActionResult Edit(int id)
        {
            var product = _context.Products.First(p => p.Id == id);
            product.CurrentLang = CurrentLang.Id;
            return View(product);
        }

        //
        // POST: /Admin/Product/Edit/5

        [HttpPost]
        public ActionResult Edit(Product model)
        {
            try
            {
                model.Text = HttpUtility.HtmlDecode(model.Text);
                var cache = _context.Products.FirstOrDefault(p => p.Id == model.Id);
                if (cache != null)
                {
                    TryUpdateModel(cache, new[] { "SortOrder", "Title", "Text", "IsContentPage" });
                    cache.Name = SiteHelper.UpdatePageWebName(model.Name);

                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        var file = Request.Files[i];
                        if (file == null) continue;
                        if (string.IsNullOrEmpty(file.FileName)) continue;
                        var pi = new ProductImage();
                        string fileName = IOHelper.GetUniqueFileName("~/Content/Images", file.FileName);
                        string filePath = Server.MapPath("~/Content/Images");

                        filePath = Path.Combine(filePath, fileName);
                        //file.SaveAs(filePath);
                        GraphicsHelper.SaveOriginalImage(filePath, fileName, file);
                        pi.ImageSource = fileName;
                        cache.ProductImages.Add(pi);
                    }


                    var lang = _context.Languages.FirstOrDefault(p => p.Id == model.CurrentLang);
                    if (lang != null)
                    {
                        CreateOrChangeContentLang(_context, model, cache, lang);
                    }
                }
                return RedirectToAction("Details", "Category", new { id = cache.CategoryId});
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Details(int id)
        {
            var product = _context.Products.First(p => p.Id == id);

            return View(product);
        }

        public ActionResult Delete(int id)
        {
            var product = _context.Products.First(p => p.Id == id);
            var categoryId = product.CategoryId;
            while (product.ProductLangs.Any())
            {
                var lang = product.ProductLangs.First();
                _context.ProductLangs.Remove(lang);
            }

            while (product.ProductImages.Any())
            {
                var image = product.ProductImages.First();
                ImageHelper.DeleteImage(image.ImageSource);
                _context.ProductImages.Remove(image);
            }

            _context.Products.Remove(product);
            _context.SaveChanges();
            return RedirectToAction("Details", "Category", new { id = categoryId });
        }

        private void CreateOrChangeContentLang(SiteContext context, Product instance, Product cache, Language lang)
        {

            ProductLang productLang = null;
            if (cache != null)
            {
                productLang = context.ProductLangs.FirstOrDefault(p => p.ProductId == cache.Id && p.LanguageId == lang.Id);
            }
            if (productLang == null)
            {
                var newPostLang = new ProductLang
                {
                    ProductId = instance.Id,
                    LanguageId = lang.Id,
                    Title = instance.Title,
                    Text = instance.Text
                };
                context.ProductLangs.Add(newPostLang);
            }
            else
            {
                productLang.Title = instance.Title;
                productLang.Text = instance.Text;
            }
            context.SaveChanges();

        }

        public ActionResult AddImage(int id)
        {
            ViewBag.ProductId = id;
            return View();
        }

        [HttpPost]
        public ActionResult AddImage(int productId, HttpPostedFileBase fileUpload)
        {
            var product = _context.Products.First(p => p.Id == productId);
            if (fileUpload != null)
            {
                string fileName = IOHelper.GetUniqueFileName("~/Content/Images", fileUpload.FileName);
                string filePath = Server.MapPath("~/Content/Images");
                filePath = Path.Combine(filePath, fileName);
                fileUpload.SaveAs(filePath);
                var pi = new ProductImage
                {
                    ImageSource = fileName
                };
                product.ProductImages.Add(pi);
                _context.SaveChanges();
            }

            return RedirectToAction("Index", "Category", new {area = "Admin"});
        }

        public ActionResult DeleteImage(int id)
        {
            var productImage = _context.ProductImages.First(pi => pi.Id == id);
            ImageHelper.DeleteImage(productImage.ImageSource);
            _context.ProductImages.Remove(productImage);
            _context.SaveChanges();
            return RedirectToAction("Index", "Category", new { area = "Admin" });
        }

    }
}
