using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kiki.DataAccess.Entities;
using Kiki.DataAccess.Repositories;
using Kiki.WebSite.Helpers;
using Kiki.WebSite.Helpers.Graphics;

namespace Kiki.WebSite.Areas.Admin.Controllers
{
    public class ContentController : AdminController
    {
        //
        // GET: /Admin/Content/

        public ContentController(ISiteRepository repository) : base(repository)
        {
        }

        public ActionResult Index()
        {
            var contents = _repository.GetContents();
            return View(contents);
        }

        public ActionResult Edit(int id)
        {
            try
            {
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
            try
            {
                var content = _repository.GetContent(model.Id);
                content.Name = string.IsNullOrEmpty(model.Name)
                    ? SiteHelper.UpdatePageWebName(model.Name, model.Title)
                    : SiteHelper.UpdatePageWebName(model.Name);
                TryUpdateModel(content, new[] { "Title", "MenuTitle", "SeoDescription", "ContentType", "SeoKeywords", "Seotext", "SortOrder" });
                content.Text = model.Text == null ? "" : HttpUtility.HtmlDecode(model.Text);

                var file = Request.Files[0];
                if (file != null && !string.IsNullOrEmpty(file.FileName))
                {
                    if (!string.IsNullOrEmpty(content.ImageSource))
                    {
                        ImageHelper.DeleteImage(content.ImageSource);
                    }

                    string fileName = IOHelper.GetUniqueFileName("~/Content/Images", file.FileName);
                    string filePath = Server.MapPath("~/Content/Images");

                    filePath = Path.Combine(filePath, fileName);
                    GraphicsHelper.SaveOriginalImage(filePath, fileName, file, 1500);
                    content.ImageSource = fileName;
                }
                else
                {
                    content.ImageSource = content.ImageSource ?? "";
                }

                _repository.SaveContent(content);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View(model);
            }
            return RedirectToAction("Index");
        }

    }
}
