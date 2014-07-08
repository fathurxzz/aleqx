using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Leo.Helpers;
using Leo.Models;

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

        public ActionResult Create(int id)
        {
            return View(new Product { CategoryId = id, CurrentLang = CurrentLang.Id });
        }

        [HttpPost]
        public ActionResult Create(Product model)
        {
            try
            {
                model.Id = 0;
                var category = _context.Categories.First(c => c.Id == model.CategoryId);
                var cache = new Product
                {
                    Name = SiteHelper.UpdatePageWebName(model.Name),
                    SortOrder = model.SortOrder,
                    Category = category
                };

                _context.Products.Add(cache);

                var lang = _context.Languages.FirstOrDefault(p => p.Id == model.CurrentLang);
                if (lang != null)
                {
                    CreateOrChangeContentLang(_context, model, cache, lang);
                }

                return RedirectToAction("Index", "Category");
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
                var cache = _context.Products.FirstOrDefault(p => p.Id == model.Id);
                if (cache != null)
                {
                    TryUpdateModel(cache, new[] { "SortOrder" });
                    cache.Name = SiteHelper.UpdatePageWebName(model.Name);
                    var lang = _context.Languages.FirstOrDefault(p => p.Id == model.CurrentLang);
                    if (lang != null)
                    {
                        CreateOrChangeContentLang(_context, model, cache, lang);
                    }
                }
                return RedirectToAction("Index", "Category");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            var product = _context.Products.First(p => p.Id == id);
            while (product.ProductLangs.Any())
            {
                var lang = product.ProductLangs.First();
                _context.ProductLangs.Remove(lang);
            }
            _context.Products.Remove(product);
            _context.SaveChanges();
            return RedirectToAction("Index", "Category");
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
    }
}
