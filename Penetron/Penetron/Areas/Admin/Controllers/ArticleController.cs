using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Penetron.Models;

namespace Penetron.Areas.Admin.Controllers
{
    [Authorize]
    public class ArticleController : Controller
    {
        private SiteContext _context;

        public ArticleController(SiteContext context)
        {
            _context = context;
        }

        public ActionResult Create()
        {
            return View(new Article { Date = DateTime.Now });
        }

        [HttpPost]
        public ActionResult Create(FormCollection form)
        {
            var article = new Article
                          {
                              Name = Utils.Transliterator.Transliterate(form["Name"]),

                              Date = Convert.ToDateTime(form["ArtDate"]),

                              Text = HttpUtility.HtmlDecode(form["Text"])
                          };
            TryUpdateModel(article, new[] {"Description", "Title", "Published" });

            _context.AddToArticle(article);
            _context.SaveChanges();

            return RedirectToAction("About", "Home", new { area = "" });
        }

        public ActionResult Edit(int id)
        {
            var article = _context.Article.First(a => a.Id == id);
            return View(article);
        }
        [HttpPost]
        public ActionResult Edit(FormCollection form, int id)
        {
            var article = _context.Article.First(a => a.Id == id);
            TryUpdateModel(article, new[] { "Description", "Title", "Published" });
            article.Name = Utils.Transliterator.Transliterate(form["Name"]);
            article.Date = Convert.ToDateTime(form["ArtDate"]);
            article.Text = HttpUtility.HtmlDecode(form["Text"]);
            _context.SaveChanges();
            return RedirectToAction("About", "Home", new { area = "" });
        }

        public ActionResult Delete(int id)
        {
            var article = _context.Article.First(a => a.Id == id);
            _context.DeleteObject(article);
            _context.SaveChanges();
            return RedirectToAction("About", "Home", new { area = "" });
        }

    }
}
