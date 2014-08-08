using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shop.DataAccess.Entities;
using Shop.DataAccess.Repositories;

namespace Shop.WebSite.Areas.Admin.Controllers
{
    public class ArticleItemController : AdminController
    {
        private readonly IShopRepository _repository;

        public ArticleItemController(IShopRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public ActionResult Create(int id)
        {
            _repository.LangId = CurrentLangId;
            return View(new ArticleItem{ArticleId = id, CurrentLang = CurrentLangId});
        }

        [HttpPost]
        public ActionResult Create(ArticleItem model)
        {
            _repository.LangId = CurrentLangId;
            var article = _repository.GetArticle(model.ArticleId);

            var articleItem = new ArticleItem()
            {
                Text = model.Text,
                Article = article
            };
            _repository.AddArticleItem(articleItem);

            return RedirectToAction("Details", "Article", new {id = model.ArticleId});
        }
    }
}
