using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HavilaTravel.Models;

namespace HavilaTravel.Areas.Admin.Controllers
{
    [Authorize]
    public class ArticlesController : Controller
    {
        //
        // GET: /Admin/Article/

        public ActionResult Add()
        {

            return View(new Article{Date = DateTime.Now.Date});
        }

        [HttpPost]
        public ActionResult Add(FormCollection form)
        {
            using (var context = new ContentStorage())
            {
                var article = new Article();
                TryUpdateModel(article, new[] { "Title", "Date" });
                article.Text = HttpUtility.HtmlDecode(form["Text"]);
                context.AddToArticle(article);
                context.SaveChanges();
            }
            return RedirectToAction("Index", "Articles", new { Area = "" });
        }

        public ActionResult Edit(int id)
        {
            using (var context = new ContentStorage())
            {
                var article = context.Article.Where(a => a.Id == id).First();
                return View(article);
            }
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection form)
        {
            using (var context = new ContentStorage())
            {
                var article = context.Article.Where(a => a.Id == id).First();
                TryUpdateModel(article, new[] { "Title", "Date" });
                article.Text = HttpUtility.HtmlDecode(form["Text"]);
                context.SaveChanges();
            }
            return RedirectToAction("Index", "Articles", new { Area = "" });
        }

        public ActionResult Delete(int id)
        {
            using (var context = new ContentStorage())
            {
                var article = context.Article.Where(a => a.Id == id).First();
                context.DeleteObject(article);
                context.SaveChanges();
            }

            return RedirectToAction("Index", "Articles", new { Area = "" });
        }

    }
}
