using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kiki.DataAccess;
using Kiki.DataAccess.Entities;
using Kiki.DataAccess.Repositories;
using Kiki.WebSite.Helpers;
using Kiki.WebSite.Helpers.Graphics;

namespace Kiki.WebSite.Areas.Admin.Controllers
{
    public class AttentionController : AdminController
    {
        public AttentionController(ISiteRepository repository) : base(repository)
        {

        }

        public ActionResult Index()
        {
            var siteImages = _repository.GetSiteImages(ImageType.Attention);
            return View(siteImages);
        }

        public ActionResult Create()
        {
            return View(new SiteImage { ImageType = (int)ImageType.Attention });
        }

        [HttpPost]
        public ActionResult Create(SiteImage model)
        {
            try
            {
                model.Id = 0;
                var siteImage = new SiteImage
                {
                    Text = model.Text == null ? "" : HttpUtility.HtmlDecode(model.Text),
                    TextEng = model.TextEng == null ? "" : HttpUtility.HtmlDecode(model.TextEng),
                    ImageType = model.ImageType
                };

                var file = Request.Files[0];
                if (file != null && !string.IsNullOrEmpty(file.FileName))
                {
                    string fileName = IOHelper.GetUniqueFileName("~/Content/Images", file.FileName);
                    string filePath = Server.MapPath("~/Content/Images");

                    filePath = Path.Combine(filePath, fileName);
                    GraphicsHelper.SaveOriginalImage(filePath, fileName, file, 1500);
                    siteImage.ImageSource = fileName;
                }
                else
                {
                    siteImage.ImageSource = siteImage.ImageSource ?? "";
                }

                _repository.AddSiteImage(siteImage);
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
                var siteImage = _repository.GetSiteImage(id);
                return View(siteImage);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Edit(SiteImage model)
        {
            try
            {
                var article = _repository.GetSiteImage(model.Id);
                article.Text = model.Text == null ? "" : HttpUtility.HtmlDecode(model.Text);
                article.TextEng = model.TextEng == null ? "" : HttpUtility.HtmlDecode(model.TextEng);

                var file = Request.Files[0];
                if (file != null && !string.IsNullOrEmpty(file.FileName))
                {
                    if (!string.IsNullOrEmpty(article.ImageSource))
                    {
                        ImageHelper.DeleteImage(article.ImageSource);
                    }

                    string fileName = IOHelper.GetUniqueFileName("~/Content/Images", file.FileName);
                    string filePath = Server.MapPath("~/Content/Images");

                    filePath = Path.Combine(filePath, fileName);
                    GraphicsHelper.SaveOriginalImage(filePath, fileName, file, 1500);
                    article.ImageSource = fileName;
                }
                else
                {
                    article.ImageSource = article.ImageSource ?? "";
                }

                _repository.SaveSiteImage(article);
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
                _repository.DeleteSiteImage(id, ImageHelper.DeleteImage);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
            }
            return RedirectToAction("Index");
        }

    }
}
