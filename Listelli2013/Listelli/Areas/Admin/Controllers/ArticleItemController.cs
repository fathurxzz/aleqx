using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Listelli.Helpers;
using Listelli.Models;
using SiteExtensions;
using SiteExtensions.Graphics;

namespace Listelli.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrators")]
    public class ArticleItemController : AdminController
    {
        public ActionResult CreateTextBlock(int articleId)
        {
            using (var context = new SiteContainer())
            {
                var brand = context.Article.First(b => b.Id == articleId);
                int maxSortOrder = context.ArticleItem.Where(b => b.ArticleId == brand.Id).Max(c => (int?)c.SortOrder) ?? 0;
                return View(new ArticleItem { SortOrder = maxSortOrder + 1, CurrentLang = CurrentLang.Id, ArticleId = articleId });
            }
        }

        [HttpPost]
        public ActionResult CreateTextBlock(ArticleItem model)
        {
            try
            {
                using (var context = new SiteContainer())
                {
                    var article = context.Article.First(b => b.Id == model.ArticleId);
                    var cache = new ArticleItem
                    {
                        SortOrder = model.SortOrder,
                        ContentType = 1,
                        Article = article,
                        ImageSource = ""
                    };

                    var lang = context.Language.FirstOrDefault(p => p.Id == model.CurrentLang);
                    if (lang != null)
                    {
                        CreateOrChangeContentLang(context, model, cache, lang);
                    }

                    return RedirectToAction("Articles", "Home", new { area = "" });
                }
            }
            catch
            {
                return View();
            }
        }

        public ActionResult EditTextBlock(int id)
        {
            using (var context = new SiteContainer())
            {
                var articleItem = context.ArticleItem.Include("Article").First(b => b.Id == id);
                articleItem.CurrentLang = CurrentLang.Id;
                return View(articleItem);
            }
        }

        [HttpPost]
        public ActionResult EditTextBlock(ArticleItem model)
        {
            using (var context = new SiteContainer())
            {
                var cache = context.ArticleItem.Include("Article").First(p => p.Id == model.Id);

                TryUpdateModel(cache, new[] { "SortOrder" });

                var lang = context.Language.FirstOrDefault(p => p.Id == model.CurrentLang);
                if (lang != null)
                {
                    CreateOrChangeContentLang(context, model, cache, lang);
                }


                return RedirectToAction("Articles", "Home", new { area = "" });
            }
        }

        public ActionResult DeleteTextBlock(int id)
        {
            using (var context = new SiteContainer())
            {
                var articleItem = context.ArticleItem.Include("Article").Include("ArticleItemLangs").First(b => b.Id == id);
                while (articleItem.ArticleItemLangs.Any())
                {
                    var ail = articleItem.ArticleItemLangs.First();
                    context.DeleteObject(ail);
                }
                context.DeleteObject(articleItem);
                context.SaveChanges();
                return RedirectToAction("Articles", "Home", new { area = "" });
            }
        }





        public ActionResult CreateImageBlock(int articleId)
        {
            using (var context = new SiteContainer())
            {
                var brand = context.Article.First(b => b.Id == articleId);
                int maxSortOrder = context.ArticleItem.Where(b => b.ArticleId == brand.Id).Max(c => (int?)c.SortOrder) ?? 0;
                return View(new ArticleItem { SortOrder = maxSortOrder + 1, CurrentLang = CurrentLang.Id, ArticleId = articleId });
            }
        }


        [HttpPost]
        public ActionResult CreateImageBlock(ArticleItem model)
        {
            try
            {
                using (var context = new SiteContainer())
                {
                    var article = context.Article.Include("ArticleItems").First(b => b.Id == model.ArticleId);

                    int maxSortOrder = article.ArticleItems.Max(c => (int?)c.SortOrder) ?? 0;


                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        maxSortOrder++;
                        var file = Request.Files[i];

                        if (file == null) continue;
                        if (string.IsNullOrEmpty(file.FileName)) continue;

                        var ai = new ArticleItem {SortOrder = maxSortOrder, ContentType = 2};
                        string fileName = IOHelper.GetUniqueFileName("~/Content/Images", file.FileName);
                        string filePath = Server.MapPath("~/Content/Images");

                        filePath = Path.Combine(filePath, fileName);
                        GraphicsHelper.SaveOriginalImage(filePath, fileName, file, 1500);
                        
                        ai.ImageSource = fileName;

                        article.ArticleItems.Add(ai);
                    }

                    context.SaveChanges();


                    return RedirectToAction("Articles", "Home", new { area = "" });
                }
            }
            catch
            {
                return View();
            }
        }



        public ActionResult DeleteImageBlock(int id)
        {
            using (var context = new SiteContainer())
            {
                var articleItem = context.ArticleItem.Include("Article").First(b => b.Id == id);

                ImageHelper.DeleteImage(articleItem.ImageSource);

                context.DeleteObject(articleItem);
                context.SaveChanges();
                return RedirectToAction("Articles", "Home", new { area = "" });
            }
        }




        private void CreateOrChangeContentLang(SiteContainer context, ArticleItem instance, ArticleItem cache, Language lang)
        {

            ArticleItemLang contenttLang = null;
            if (cache != null)
            {
                contenttLang = context.ArticleItemLang.FirstOrDefault(p => p.ArticleItemId == cache.Id && p.LanguageId == lang.Id);
            }
            if (contenttLang == null)
            {
                var newPostLang = new ArticleItemLang
                {
                    ArticleItemId = instance.Id,
                    LanguageId = lang.Id,
                    Text = HttpUtility.HtmlDecode(instance.Text)
                };
                context.AddToArticleItemLang(newPostLang);
            }
            else
            {
                contenttLang.Text = HttpUtility.HtmlDecode(instance.Text);
            }
            context.SaveChanges();

        }
    }
}
