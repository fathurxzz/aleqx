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
    public class ContentItemController : AdminController
    {
        public ContentItemController(IShopRepository repository) : base(repository)
        {

        }

        public ActionResult Create(int id)
        {
            _repository.LangId = CurrentLangId;
            return View(new ContentItem { ContentId = id, CurrentLang = CurrentLangId });
        }

        [HttpPost]
        public ActionResult Create(ContentItem model)
        {
            _repository.LangId = CurrentLangId;
            var content = _repository.GetContent(model.ContentId);

            var contentItem = new ContentItem()
            {
                //Text = model.Text,
                Content = content
            };

            contentItem.Text = model.Text == null ? "" : HttpUtility.HtmlDecode(model.Text);


            for (int i = 0; i < Request.Files.Count; i++)
            {
                var file = Request.Files[i];
                if (file == null) continue;
                if (string.IsNullOrEmpty(file.FileName)) continue;

                string fileName = IOHelper.GetUniqueFileName("~/Content/Images", file.FileName);
                string filePath = Server.MapPath("~/Content/Images");

                filePath = Path.Combine(filePath, fileName);
                GraphicsHelper.SaveOriginalImage(filePath, fileName, file, 1500);
                var contentItemImage = new ContentItemImage{ ImageSource = fileName };
                contentItem.ContentItemImages.Add(contentItemImage);
            }

            _repository.AddContentItem(contentItem);

            return RedirectToAction("Details", "Content", new { id = model.ContentId });
        }




        public ActionResult Edit(int id)
        {
            _repository.LangId = CurrentLangId;
            var contentItem = _repository.GetContentItem(id);
            return View(contentItem);
        }

        [HttpPost]
        public ActionResult Edit(ContentItem model)
        {
            _repository.LangId = CurrentLangId;
            var contentItem = _repository.GetContentItem(model.Id);

            contentItem.Text = model.Text == null ? "" : HttpUtility.HtmlDecode(model.Text);

            for (int i = 0; i < Request.Files.Count; i++)
            {
                var file = Request.Files[i];
                if (file == null) continue;
                if (string.IsNullOrEmpty(file.FileName)) continue;

                string fileName = IOHelper.GetUniqueFileName("~/Content/Images", file.FileName);
                string filePath = Server.MapPath("~/Content/Images");

                filePath = Path.Combine(filePath, fileName);
                GraphicsHelper.SaveOriginalImage(filePath, fileName, file, 1500);
                var contentItemImage = new ContentItemImage() { ImageSource = fileName };
                contentItem.ContentItemImages.Add(contentItemImage);
            }


            _repository.SaveContentItem(contentItem);
            return RedirectToAction("Details", "Content", new { id = contentItem.Content.Id });
        }

        public ActionResult Delete(int id)
        {
            _repository.LangId = CurrentLangId;
            var contentId = _repository.GetContentItem(id).ContentId;
            _repository.DeleteContntItem(id, ImageHelper.DeleteImage);
            return RedirectToAction("Details", "Content", new { id = contentId });
        }

        public ActionResult DeleteImage(int id)
        {
            _repository.LangId = CurrentLangId;
            var contentId = _repository.GetContentItemImage(id).ContentItem.Content.Id;
            _repository.DeleteContentItemImage(id, ImageHelper.DeleteImage);
            return RedirectToAction("Details", "Content", new { id = contentId });
        }

    }
}
