using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Leo.Helpers;
using Leo.Models;

namespace Leo.Areas.Admin.Controllers
{
    public class ArticleController : AdminController
    {
        private readonly SiteContext _context;

        public ArticleController(SiteContext context)
        {
            _context = context;
        }

        public ActionResult Index(int id)
        {
            var category = _context.Categories.First(c => c.Id == id);
            var articles = category.Articles.ToList();
            foreach (var article in articles)
            {
                article.CurrentLang = CurrentLang.Id;
                foreach (var item in article.ArticleItems)
                {
                    item.CurrentLang = CurrentLang.Id;
                }
            }
            return View(articles);
        }


        public ActionResult Create(int id)
        {
            return View(new Article { CurrentLang = CurrentLang.Id, CategoryId = id, Date = DateTime.Now});
        }

        [HttpPost]
        public ActionResult Create(Article model)
        {
            try
            {
                model.Id = 0;
                var category = _context.Categories.First(c => c.Id == model.CategoryId);

                model.Description = HttpUtility.HtmlDecode(model.Description);

                var cache = new Article
                {
                    Category = category,
                    Published = false,
                    Date = DateTime.Now,
                    Title = model.Title,
                    Description = model.Description
                };

                _context.Articles.Add(cache);

                var lang = _context.Languages.FirstOrDefault(p => p.Id == model.CurrentLang);
                if (lang != null)
                {
                    CreateOrChangeContentLang(_context, model, cache, lang);
                }
                return RedirectToAction("Index","Category");
            }
            catch (Exception)
            {

                return View(model);
            }
        }


        public ActionResult Edit(int id)
        {
            var article = _context.Articles.First(p => p.Id == id);
            article.CurrentLang = CurrentLang.Id;
            return View(article);
        }

        [HttpPost]
        public ActionResult Edit(Article model)
        {
            try
            {
                model.Description = HttpUtility.HtmlDecode(model.Description);
                var cache = _context.Articles.FirstOrDefault(p => p.Id == model.Id);
                if (cache != null)
                {
                    TryUpdateModel(cache, new[] { "Title", "Published" });
                    cache.Description = model.Description;
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
                return View(model);
            }
        }

        public ActionResult Delete(int id)
        {
            var article = _context.Articles.First(p => p.Id == id);
            while (article.ArticleLangs.Any())
            {
                var lang = article.ArticleLangs.First();
                _context.ArticleLangs.Remove(lang);
            }

            //while (product.ProductImages.Any())
            //{
            //    var image = product.ProductImages.First();
            //    ImageHelper.DeleteImage(image.ImageSource);
            //    _context.ProductImages.Remove(image);
            //}

            _context.Articles.Remove(article);
            _context.SaveChanges();
            return RedirectToAction("Index", "Category");
        }


       



        private void CreateOrChangeContentLang(SiteContext context, Article instance, Article cache, Language lang)
        {

            ArticleLang productLang = null;
            if (cache != null)
            {
                productLang = context.ArticleLangs.FirstOrDefault(p => p.ArticleId == cache.Id && p.LanguageId == lang.Id);
            }
            if (productLang == null)
            {
                var newPostLang = new ArticleLang
                {
                    ArticleId = instance.Id,
                    LanguageId = lang.Id,
                    Title = instance.Title,
                    Description = instance.Description
                };
                context.ArticleLangs.Add(newPostLang);
            }
            else
            {
                productLang.Title = instance.Title;
                productLang.Description = instance.Description;
            }
            context.SaveChanges();

        }
    }
}
