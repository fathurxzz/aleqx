using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shop.Models;

namespace Shop.Areas.Admin.Controllers
{
    [Authorize]
    public class ArticleController : Controller
    {
        //
        // GET: /Admin/Article/

        public ActionResult Index()
        {
            using (var context = new ShopContainer())
            {
                var articles = context.Article.ToList();
                return View(articles);
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection form)
        {
            try
            {
                using (var context = new ShopContainer())
                {
                    var article = new Article { Date = DateTime.Now };
                    TryUpdateModel(article, new[]
                                                {
                                                    "Name", 
                                                    "Title", 
                                                    "Published", 
                                                    "SeoDescription", 
                                                    "SeoKeywords"
                                                });
                    article.Text = HttpUtility.HtmlDecode(form["Text"]);

                    context.AddToArticle(article);
                    context.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            using (var context = new ShopContainer())
            {
                var article = context.Article.First(c => c.Id == id);
                return View(article);
            }
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection form)
        {
            using (var context = new ShopContainer())
            {
                var article = context.Article.First(c => c.Id == id);
                TryUpdateModel(article,
                              new[]
                                   {
                                       "Name",
                                       "Title",
                                       "Published",
                                       "SeoDescription",
                                       "SeoKeywords"
                                   });
                article.Text = HttpUtility.HtmlDecode(form["Text"]);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(int id)
        {
            using (var context = new ShopContainer())
            {
                var article = context.Article.First(c => c.Id == id);
                context.DeleteObject(article);
                return RedirectToAction("Index");
            }
        }
    }
}
