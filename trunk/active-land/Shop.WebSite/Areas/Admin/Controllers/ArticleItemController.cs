using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shop.DataAccess.Entities;
using Shop.DataAccess.Repositories;
using Shop.WebSite.Helpers;
using Shop.WebSite.Helpers.Graphics;

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
                //Text = model.Text,
                Article = article
            };

            articleItem.Text = model.Text == null ? "" : HttpUtility.HtmlDecode(model.Text);


            for (int i = 0; i < Request.Files.Count; i++)
            {
                var file = Request.Files[i];
                if (file == null) continue;
                if (string.IsNullOrEmpty(file.FileName)) continue;

                string fileName = IOHelper.GetUniqueFileName("~/Content/Images", file.FileName);
                string filePath = Server.MapPath("~/Content/Images");

                filePath = Path.Combine(filePath, fileName);
                GraphicsHelper.SaveOriginalImage(filePath, fileName, file, 1500);
                var articleItemImage = new ArticleItemImage() { ImageSource = fileName };
                articleItem.ArticleItemImages.Add(articleItemImage);
            }

            _repository.AddArticleItem(articleItem);

            return RedirectToAction("Details", "Article", new {id = model.ArticleId});
        }

        public ActionResult Edit(int id)
        {
            _repository.LangId = CurrentLangId;
            var articleItem = _repository.GetArticleItem(id);
            return View(articleItem);
        }
        
        [HttpPost]
        public ActionResult Edit(ArticleItem model)
        {
            _repository.LangId = CurrentLangId;
            var articleItem = _repository.GetArticleItem(model.Id);

            articleItem.Text = model.Text == null ? "" : HttpUtility.HtmlDecode(model.Text);

            for (int i = 0; i < Request.Files.Count; i++)
            {
                var file = Request.Files[i];
                if (file == null) continue;
                if (string.IsNullOrEmpty(file.FileName)) continue;

                string fileName = IOHelper.GetUniqueFileName("~/Content/Images", file.FileName);
                string filePath = Server.MapPath("~/Content/Images");

                filePath = Path.Combine(filePath, fileName);
                GraphicsHelper.SaveOriginalImage(filePath, fileName, file, 1500);
                var articleItemImage = new ArticleItemImage() { ImageSource = fileName };
                articleItem.ArticleItemImages.Add(articleItemImage);
            }


            _repository.SaveArticleItem(articleItem);
            return RedirectToAction("Details", "Article", new { id = articleItem.Article.Id });
        }

        public ActionResult Delete(int id)
        {
            _repository.LangId = CurrentLangId;
            var articleId = _repository.GetArticleItem(id).ArticleId;
            _repository.DeleteArticleItem(id, ImageHelper.DeleteImage);
            return RedirectToAction("Details", "Article", new { id = articleId });
        }

        public ActionResult DeleteImage(int id)
        {
            _repository.LangId = CurrentLangId;
            var articleId = _repository.GetArticleItemImage(id).ArticleItem.Article.Id;
            _repository.DeleteArticleItemImage(id,ImageHelper.DeleteImage);
            return RedirectToAction("Details", "Article", new { id = articleId });
        }
    }
}
