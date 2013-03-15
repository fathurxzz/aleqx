using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kulumu.Models;

namespace Kulumu.Areas.Admin.Controllers
{
    public class ArticleController : Controller
    {
        public ActionResult Create()
        {
            return View(new Article { Date = DateTime.Now });
        }

        [HttpPost]
        public ActionResult Create(FormCollection form)
        {

            using (var context = new SiteContainer())
            {
                var article = new Article { Name = "" };
                TryUpdateModel(article, new[] { "Name", "Title", "Date", "Description" });
                article.Text = HttpUtility.HtmlDecode(form["Text"]);
                context.AddToArticle(article);
                context.SaveChanges();
            }
            return RedirectToAction("Articles", "Home", new { area = "" });

        }

        public ActionResult Edit(int id)
        {
            using (var context = new SiteContainer())
            {
                var article = context.Article.First(a => a.Id == id);
                return View(article);
            }
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection form)
        {
            using (var context = new SiteContainer())
            {
                var article = context.Article.First(a => a.Id == id);
                TryUpdateModel(article, new[] { "Name", "Title", "Date", "Description" });
                article.Text = HttpUtility.HtmlDecode(form["Text"]);
                context.SaveChanges();
                return RedirectToAction("Articles","Home",new{area=""});
            }
        }

        public ActionResult Delete(int id)
        {
            using (var context = new SiteContainer())
            {
                var article = context.Article.First(a => a.Id == id);
                context.DeleteObject(article);
                context.SaveChanges();
                return RedirectToAction("Articles", "Home", new { area = "" });
            }
        }
    }
}
