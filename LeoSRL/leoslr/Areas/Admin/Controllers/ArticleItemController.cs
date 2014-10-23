using System;
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
    public class ArticleItemController : AdminController
    {

         private readonly SiteContext _context;

        public ArticleItemController(SiteContext context)
        {
            _context = context;
        }

        public ActionResult Create(int id)
        {
            
            return View(new ArticleItem{CurrentLang = CurrentLang.Id,SortOrder = 0, ArticleId = id});
        }

        [HttpPost]
        public ActionResult Create(ArticleItem model)
        {
            try
            {
                model.Id = 0;
                var article = _context.Articles.First(a => a.Id == model.ArticleId);
                model.Text = HttpUtility.HtmlDecode(model.Text);
                var cache = new ArticleItem
                {
                    Article = article,
                    ContentType = 0,
                    SortOrder = model.SortOrder,
                    Text = model.Text
                };

                article.ArticleItems.Add(cache);

                int maxSortOrder = article.ArticleItems.Max(c => (int?)c.SortOrder) ?? 0;
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    maxSortOrder++;
                    var file = Request.Files[i];

                    if (file == null) continue;
                    if (string.IsNullOrEmpty(file.FileName)) continue;

                    var ai = new ArticleItemImage { SortOrder = maxSortOrder };
                    string fileName = IOHelper.GetUniqueFileName("~/Content/Images", file.FileName);
                    string filePath = Server.MapPath("~/Content/Images");

                    filePath = Path.Combine(filePath, fileName);
                    GraphicsHelper.SaveOriginalImage(filePath, fileName, file);

                    ai.ImageSource = fileName;

                    cache.ArticleItemImages.Add(ai);
                }


                var lang = _context.Languages.FirstOrDefault(p => p.Id == model.CurrentLang);
                if (lang != null)
                {
                    CreateOrChangeContentLang(_context, model, cache, lang);
                }


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
            var ai = _context.ArticleItems.First(a => a.Id == id);
            ai.CurrentLang = CurrentLang.Id;
            return View(ai);
        }


        [HttpPost]
        public ActionResult Edit(ArticleItem model)
        {
            try
            {
                model.Text = HttpUtility.HtmlDecode(model.Text);
                var cache = _context.ArticleItems.FirstOrDefault(p => p.Id == model.Id);
                if (cache != null)
                {
                    TryUpdateModel(cache, new[] { "SortOrder", "Title", "Text" });

                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        var file = Request.Files[i];
                        if (file == null) continue;
                        if (string.IsNullOrEmpty(file.FileName)) continue;
                        var ai = new ArticleItemImage ();
                        string fileName = IOHelper.GetUniqueFileName("~/Content/Images", file.FileName);
                        string filePath = Server.MapPath("~/Content/Images");

                        filePath = Path.Combine(filePath, fileName);
                        GraphicsHelper.SaveOriginalImage(filePath, fileName, file);
                        ai.ImageSource = fileName;
                        cache.ArticleItemImages.Add(ai);
                    }


                    var lang = _context.Languages.FirstOrDefault(p => p.Id == model.CurrentLang);
                    if (lang != null)
                    {
                        CreateOrChangeContentLang(_context, model, cache, lang);
                    }
                }
                return RedirectToAction("Index", "Category");
            }
            catch(Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View(model);
            }
        }

        public ActionResult Delete(int id)
        {
            var articleItem = _context.ArticleItems.First(a => a.Id == id);
            while (articleItem.ArticleItemImages.Any())
            {
                var image = articleItem.ArticleItemImages.First();
                ImageHelper.DeleteImage(image.ImageSource);
                _context.ArticleItemImages.Remove(image);
            }
            while (articleItem.ArticleItemLangs.Any())
            {
                var lang = articleItem.ArticleItemLangs.First();
                _context.ArticleItemLangs.Remove(lang);
            }
            _context.ArticleItems.Remove(articleItem);
            _context.SaveChanges();
            return RedirectToAction("Index", "Category");
        }

        public ActionResult DeleteImage(int id)
        {
            var productImage = _context.ArticleItemImages.First(pi => pi.Id == id);
            ImageHelper.DeleteImage(productImage.ImageSource);
            _context.ArticleItemImages.Remove(productImage);
            _context.SaveChanges();
            return RedirectToAction("Index", "Category", new { area = "Admin" });
        }



        private void CreateOrChangeContentLang(SiteContext context, ArticleItem instance, ArticleItem cache, Language lang)
        {

            ArticleItemLang productLang = null;
            if (cache != null)
            {
                productLang = context.ArticleItemLangs.FirstOrDefault(p => p.ArticleItemId == cache.Id && p.LanguageId == lang.Id);
            }
            if (productLang == null)
            {
                var newPostLang = new ArticleItemLang
                {
                    ArticleItemId = instance.Id,
                    LanguageId = lang.Id,
                    Text = instance.Text
                };
                context.ArticleItemLangs.Add(newPostLang);
            }
            else
            {
                productLang.Text = instance.Text;
            }
            context.SaveChanges();

        }

    }
}
