using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Penetron.Models;

namespace Penetron.Areas.Admin.Controllers
{
    [Authorize]
    public class UserArticleController : Controller
    {
        private SiteContext _context;

        public UserArticleController(SiteContext context)
        {
            _context = context;
        }


        public ActionResult Index()
        {
            return View(_context.UserArticle.ToList());
        }

        public ActionResult Create()
        {
            return View(new UserArticle());
        }

        [HttpPost]
        public ActionResult Create(UserArticle model)
        {
            var article = new UserArticle
                          {
                              Name =Utils.Transliterator.Transliterate( model.Name),
                              Title = model.Title,
                              Text = HttpUtility.HtmlDecode(model.Text)
                          };
            _context.AddToUserArticle(article);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var article = _context.UserArticle.First(c => c.Id == id);
            return View(article);
        }

        [HttpPost]
        public ActionResult Edit(Content model)
        {
            var article = _context.UserArticle.First(c => c.Id == model.Id);
            article.Name = Utils.Transliterator.Transliterate( model.Name);
            article.Title = model.Title;
            article.Text = HttpUtility.HtmlDecode(model.Text);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var article = _context.UserArticle.First(a => a.Id == id);
            _context.DeleteObject(article);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
