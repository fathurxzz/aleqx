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
    public class ContentController : AdminController
    {
        private readonly IShopRepository _repository;

        public ContentController(IShopRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public ActionResult Index()
        {
            _repository.LangId = CurrentLangId;
            var contents = _repository.GetContents();
            return View(contents);
        }


        public ActionResult Create()
        {
            _repository.LangId = CurrentLangId;
            return View(new Content { CurrentLang = CurrentLangId});
        }

        [HttpPost]
        public ActionResult Create(Content model)
        {
            _repository.LangId = CurrentLangId;
            try
            {
                model.Id = 0;
                var content = new Content
                {
                    Name = string.IsNullOrEmpty(model.Name)
                        ? SiteHelper.UpdatePageWebName(model.Name, model.Title)
                        : SiteHelper.UpdatePageWebName(model.Name),
                    Title = model.Title,
                    SeoDescription = model.SeoDescription,
                    ContentType = model.ContentType,
                    SeoKeywords = model.SeoKeywords,
                    Text = model.Text??"",
                    SeoText = model.SeoText,
                    SortOrder = model.SortOrder
                };

                _repository.AddContent(content);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message + (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message) ? ex.InnerException.Message : "");
                return View(model);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            try
            {
                _repository.LangId = CurrentLangId;
                var content = _repository.GetContent(id);
                return View(content);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Edit(Content model)
        {
            _repository.LangId = CurrentLangId;
            try
            {
                var content = _repository.GetContent(model.Id);
                content.Name = SiteHelper.UpdatePageWebName(model.Name);
                TryUpdateModel(content, new[] { "Name", "Title", "SeoDescription", "ContentType", "SeoKeywords", "Seotext", "SortOrder" });
                content.Text = model.Text ?? "";
                _repository.SaveContent(content);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View(model);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            try
            {
                _repository.DeleteContent(id);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
            }
            return RedirectToAction("Index");
        }
    }
}
