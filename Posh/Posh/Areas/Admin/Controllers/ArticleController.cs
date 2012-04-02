using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Posh.Models;

namespace Posh.Areas.Admin.Controllers
{
    public class ArticleController : Controller
    {
        public ActionResult Create()
        {
            return View(new Article());
        }

        [HttpPost]
        public ActionResult Create(FormCollection form)
        {
            using (var context = new ModelContainer())
            {
                var article = new Article();
                TryUpdateModel(article, new[] { "Title", "Name", "SortOrder" });
                article.Text = HttpUtility.HtmlDecode(form["Text"]);
                context.AddToArticle(article);
                context.SaveChanges();
            }
            return RedirectToAction("Index", "Articles", new { Area = "" });
        }

        public ActionResult Edit(int id)
        {
            using (var context = new ModelContainer())
            {
                var article = context.Article.First(n => n.Id == id);
                return View(article);
            }
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection form)
        {
            using (var context = new ModelContainer())
            {
                var article = context.Article.First(n => n.Id == id);
                TryUpdateModel(article, new[] { "Title", "Name", "SortOrder" });
                article.Text = HttpUtility.HtmlDecode(form["Text"]);
                context.SaveChanges();
            }
            return RedirectToAction("Index", "Articles", new { Area = "" });
        }

        public ActionResult Delete(int id)
        {
            using (var context = new ModelContainer())
            {
                var article = context.Article.First(n => n.Id == id);
                context.DeleteObject(article);
                context.SaveChanges();
            }
            return RedirectToAction("Index", "Articles", new { Area = "" });
        }

    }
}
