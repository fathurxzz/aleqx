using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Leo.Models;

namespace Leo.Areas.Admin.Controllers
{
    public class ArticleController : Controller
    {
        public ActionResult Create()
        {
            Article article = new Article { Date = DateTime.Now };
            return View(article);
        }


        [HttpPost]
        public ActionResult Create(FormCollection form)
        {
            try
            {
                using (var context = new SiteContainer())
                {
                    Article article = new Article();
                    TryUpdateModel(article, new[] { "Title", "Date" });
                    article.Text = HttpUtility.HtmlDecode(form["Text"]);
                    context.AddToArticle(article);
                    context.SaveChanges();
                }
                return RedirectToAction("Index", "Home", new { Area = "", id = "" });
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
                Article article = context.Article.First(a => a.Id == id);
                return View(article);
            }
        }


        [HttpPost]
        public ActionResult Edit(int id, FormCollection form)
        {
            try
            {
                using (var context = new SiteContainer())
                {
                    Article article = context.Article.First(a => a.Id == id);
                    TryUpdateModel(article, new[] { "Title", "Date" });
                    article.Text = HttpUtility.HtmlDecode(form["Text"]);
                    context.SaveChanges();
                }
                return RedirectToAction("Index", "Home", new { Area = "", id = "" });
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
                Article article = context.Article.First(a => a.Id == id);
                context.DeleteObject(article);
                context.SaveChanges();
            }
            return RedirectToAction("Index", "Home", new { Area = "", id = "" });
        }


    }
}
