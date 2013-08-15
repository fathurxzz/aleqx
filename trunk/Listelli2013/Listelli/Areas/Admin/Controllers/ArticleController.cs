using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Listelli.Helpers;
using Listelli.Models;

namespace Listelli.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrators")]
    public class ArticleController : AdminController
    {
        public ActionResult Create()
        {
            return View(new Article {CurrentLang = CurrentLang.Id, Date = DateTime.Now});
        }

        [HttpPost]
        public ActionResult Create(Article model)
        {
            try
            {
                using (var context = new SiteContainer())
                {
                    var cache = new Article
                    {
                        Date = model.Date,
                        Published = model.Published
                    };

                    context.AddToArticle(cache);

                    var lang = context.Language.FirstOrDefault(p => p.Id == model.CurrentLang);
                    if (lang != null)
                    {
                        CreateOrChangeContentLang(context, model, cache, lang);
                    }

                    return RedirectToAction("Articles","Home",new {area=""});
                }
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            using (var context = new SiteContainer())
            {
                var article = context.Article.First(c => c.Id == id);
                article.CurrentLang = CurrentLang.Id;
                return View(article);
            }
        }

        [HttpPost]
        public ActionResult Edit(Article model)
        {
            try
            {
                using (var context = new SiteContainer())
                {
                    var cache = context.Article.FirstOrDefault(p => p.Id == model.Id);
                    if (cache != null)
                    {
                        TryUpdateModel(cache, new[] { "Date", "Published" });

                        var lang = context.Language.FirstOrDefault(p => p.Id == model.CurrentLang);
                        if (lang != null)
                        {
                            CreateOrChangeContentLang(context, model, cache, lang);
                        }
                    }

                    return RedirectToAction("Articles", "Home", new { area = "" });
                }
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            using (var context = new SiteContainer())
            {
                var article = context.Article.Include("ArticleItems").Include("ArticleLangs").First(a => a.Id == id);
                while (article.ArticleLangs.Any())
                {
                    var al = article.ArticleLangs.First();
                    context.DeleteObject(al);
                }


                while (article.ArticleItems.Any())
                {
                    var item = article.ArticleItems.First();
                    var articleItem = context.ArticleItem.Include("ArticleItemLangs").First(ai => ai.Id == item.Id);

                    while (articleItem.ArticleItemLangs.Any())
                    {
                        var ail = articleItem.ArticleItemLangs.First();
                        context.DeleteObject(ail);
                    }

                    ImageHelper.DeleteImage(item.ImageSource);
                    
                    context.DeleteObject(articleItem);
                }


                context.DeleteObject(article);
                context.SaveChanges();

                return RedirectToAction("Articles", "Home", new { area = "" });
            }
        }

        private void CreateOrChangeContentLang(SiteContainer context, Article instance, Article cache, Language lang)
        {
            ArticleLang contenttLang = null;
            if (cache != null)
            {
                contenttLang = context.ArticleLang.FirstOrDefault(p => p.ArticleId == cache.Id && p.LanguageId == lang.Id);
            }
            if (contenttLang == null)
            {
                var newPostLang = new ArticleLang
                {
                    ArticleId = instance.Id,
                    LanguageId = lang.Id,
                    Title = instance.Title,
                    Description = HttpUtility.HtmlDecode(instance.Description)
                };
                context.AddToArticleLang(newPostLang);
            }
            else
            {
                contenttLang.Title = instance.Title;
                contenttLang.Description = HttpUtility.HtmlDecode(instance.Description);
            }
            context.SaveChanges();

        }
    }
}
