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


        public ActionResult Create()
        {
            return View(new UserArticle());
        }

        [HttpPost]
        public ActionResult Create(UserArticle model)
        {
            var article = new UserArticle
                          {
                              Name = model.Name,
                              Title = model.Title,
                              Text = HttpUtility.HtmlDecode(model.Text)
                          };
            _context.AddToUserArticle(article);
            _context.SaveChanges();
            return RedirectToAction("UserArticle", "Home", new {area = "", id = article.Name});
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
            article.Name = model.Name;
            article.Title = model.Title;
            article.Text = HttpUtility.HtmlDecode(model.Text);
            _context.SaveChanges();
            return RedirectToAction("UserArticle", "Home", new { area = "", id = article.Name });
        }

    }
}
