using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Leo.Helpers;
using Leo.Models;

namespace Leo.Areas.Admin.Controllers
{
    public class ProductTextBlockController : AdminController
    {
        private readonly SiteContext _context;

        public ProductTextBlockController(SiteContext context)
        {
            _context = context;
        }

        public ActionResult Create(int id)
        {
            var product = _context.Products.First(a => a.Id == id);
            return View(new ProductTextBlock { CurrentLang = CurrentLang.Id, SortOrder = 0, ProductId = id, Product = product});
        }

        [HttpPost]
        public ActionResult Create(ProductTextBlock model)
        {

            try
            {
                model.Id = 0;
                var product = _context.Products.First(a => a.Id == model.ProductId);
                model.Text = HttpUtility.HtmlDecode(model.Text);
                //model.Title = model.Title;
                var cache = new ProductTextBlock
                {
                    Product = product,
                    SortOrder = model.SortOrder,
                    Text = model.Text,
                    Title = model.Title
                };

                product.ProductTextBlocks.Add(cache);

                var lang = _context.Languages.FirstOrDefault(p => p.Id == model.CurrentLang);
                if (lang != null)
                {
                    CreateOrChangeContentLang(_context, model, cache, lang);
                }


                return RedirectToAction("Details", "Category", new { id = product.CategoryId });
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View(model);
            }
        }



        public ActionResult Edit(int id)
        {
            var ai = _context.ProductTextBlocks.First(a => a.Id == id);
            ai.CurrentLang = CurrentLang.Id;
            return View(ai);
        }


        [HttpPost]
        public ActionResult Edit(ProductTextBlock model)
        {
            try
            {
                model.Text = HttpUtility.HtmlDecode(model.Text);
                var cache = _context.ProductTextBlocks.FirstOrDefault(p => p.Id == model.Id);
                if (cache != null)
                {
                    TryUpdateModel(cache, new[] { "SortOrder", "Title", "Text" });

                    var lang = _context.Languages.FirstOrDefault(p => p.Id == model.CurrentLang);
                    if (lang != null)
                    {
                        CreateOrChangeContentLang(_context, model, cache, lang);
                    }
                }
                return RedirectToAction("Details", "Category", new { id = cache.Product.CategoryId });
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View(model);
            }
        }

        public ActionResult Delete(int id)
        {
            var articleItem = _context.ProductTextBlocks.First(a => a.Id == id);
            var categoryId = articleItem.Product.CategoryId;
            while (articleItem.ProductTextBlockFiles.Any())
            {
                var image = articleItem.ProductTextBlockFiles.First();
                ImageHelper.DeleteImage(image.ImageSource);
                _context.ProductTextBlockFiles.Remove(image);
            }
            while (articleItem.ProductTextBlockLangs.Any())
            {
                var lang = articleItem.ProductTextBlockLangs.First();
                _context.ProductTextBlockLangs.Remove(lang);
            }
            _context.ProductTextBlocks.Remove(articleItem);
            _context.SaveChanges();
            return RedirectToAction("Details", "Category", new { id = categoryId });
        }

        private void CreateOrChangeContentLang(SiteContext context, ProductTextBlock instance, ProductTextBlock cache, Language lang)
        {
            ProductTextBlockLang productLang = null;
            if (cache != null)
            {
                productLang = context.ProductTextBlockLangs.FirstOrDefault(p => p.ProductTextBlockId == cache.Id && p.LanguageId == lang.Id);
            }
            if (productLang == null)
            {
                var newPostLang = new ProductTextBlockLang
                {
                    ProductTextBlockId = instance.Id,
                    LanguageId = lang.Id,
                    Text = instance.Text,
                    Title = instance.Title
                };
                context.ProductTextBlockLangs.Add(newPostLang);
            }
            else
            {
                productLang.Text = instance.Text;
                productLang.Title = instance.Title;
            }
            context.SaveChanges();
        }

    }
}
