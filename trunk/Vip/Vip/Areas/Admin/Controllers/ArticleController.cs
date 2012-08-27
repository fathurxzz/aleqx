using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vip.Models;

namespace Vip.Areas.Admin.Controllers
{
    public class ArticleController : Controller
    {
        public ActionResult Create()
        {
            return View(new Article { Date = DateTime.Now });
        }

        //
        // POST: /Admin/Article/Create

        [HttpPost]
        public ActionResult Create(FormCollection form)
        {
            try
            {
                using (var context = new SiteContainer())
                {
                    var article = new Article();
                    TryUpdateModel(article, new[] { "Title", "Date" });
                    article.Text = HttpUtility.HtmlDecode(form["Text"]);
                    context.AddToArticle(article);
                    context.SaveChanges();
                    return RedirectToAction("Index", "Articles", new { area = "" });
                }
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Admin/Article/Edit/5

        public ActionResult Edit(int id)
        {
            using (var context = new SiteContainer())
            {
                var article = context.Article.First(a => a.Id == id);
                return View(article);
            }
        }

        //
        // POST: /Admin/Article/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection form)
        {
            try
            {
                using (var context = new SiteContainer())
                {
                    var article = context.Article.First(a => a.Id == id);
                    TryUpdateModel(article, new[] { "Title", "Date" });
                    article.Text = HttpUtility.HtmlDecode(form["Text"]);
                    context.SaveChanges();
                    return RedirectToAction("Index", "Articles", new { area = "" });
                }
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Admin/Article/Delete/5

        public ActionResult Delete(int id)
        {
            using (var context = new SiteContainer())
            {
                var article = context.Article.First(a => a.Id == id);
                context.DeleteObject(article);
                context.SaveChanges();
                return RedirectToAction("Index", "Articles", new { area = "" });
            }
        }
    }
}
