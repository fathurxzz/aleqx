using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Leo.Models;

namespace Leo.Areas.Admin.Controllers
{
    public class ArticleController : AdminController
    {
        private readonly SiteContext _context;

        public ArticleController(SiteContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            var articles = _context.Articles.ToList();
            foreach (var article in articles)
            {
                article.CurrentLang = CurrentLang.Id;
                foreach (var item in article.ArticleItems)
                {
                    item.CurrentLang = CurrentLang.Id;
                }
            }
            return View(articles);
        }


        public ActionResult Create()
        {

            return View(new Article { CurrentLang = CurrentLang.Id });
        }

        [HttpPost]
        public ActionResult Create(Article model)
        {
            
            return RedirectToAction("Index");
        }
    }
}
