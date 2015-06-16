using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewVision.UI.Helpers;
using NewVision.UI.Models;

namespace NewVision.UI.Areas.Admin.Controllers
{
    public class MediaController : AdminController
    {
        private readonly SiteContext _context;

        public MediaController(SiteContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            var media = _context.Media.ToList();
            return View(media);
        }

       
       
        public ActionResult Create()
        {
            return View(new Media{SortOrder = 0});
        }

      

        [HttpPost]
        public ActionResult Create(Media model, HttpPostedFileBase file)
        {
            try
            {
                var media = new Media
                {
                    Title = model.Title,
                    TitleEn = model.TitleEn,
                    TitleUa = model.TitleUa,
                    ContentType = model.ContentType,
                    SortOrder = model.SortOrder,
                    VideoSrc = model.VideoSrc
                };

                media.Text = model.Text == null ? "" : HttpUtility.HtmlDecode(model.Text);
                media.TextEn = model.TextEn == null ? "" : HttpUtility.HtmlDecode(model.TextEn);
                media.TextUa = model.TextUa == null ? "" : HttpUtility.HtmlDecode(model.TextUa);

                if (file != null)
                {
                    string fileName = IOHelper.GetUniqueFileName("~/Content/Images", file.FileName);
                    string filePath = Server.MapPath("~/Content/Images");
                    filePath = Path.Combine(filePath, fileName);
                    GraphicsHelper.SaveOriginalImageWithDefinedDimentions(filePath, fileName, file, 560, 338, ScaleMode.Crop);
                    media.ImageSrc = fileName;
                }


                _context.Media.Add(media);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        public ActionResult Edit(int id)
        {
            var article = _context.Media.First(e => e.Id == id);
            return View(article);
        }

    

        [HttpPost]
        public ActionResult Edit(Media model, int id, HttpPostedFileBase file)
        {
            try
            {
                var article = _context.Media.First(e => e.Id == id);
                TryUpdateModel(article, new[] { "Title", "TitleEn", "TitleUa", "ContentType", "SortOrder", "VideoSrc" });
                article.Text = model.Text == null ? "" : HttpUtility.HtmlDecode(model.Text);
                article.TextEn = model.TextEn == null ? "" : HttpUtility.HtmlDecode(model.TextEn);
                article.TextUa = model.TextUa == null ? "" : HttpUtility.HtmlDecode(model.TextUa);
                if (file != null)
                {
                    if (!string.IsNullOrEmpty(article.ImageSrc))
                    {
                        ImageHelper.DeleteImage(article.ImageSrc);
                    }

                    string fileName = IOHelper.GetUniqueFileName("~/Content/Images", file.FileName);
                    string filePath = Server.MapPath("~/Content/Images");
                    filePath = Path.Combine(filePath, fileName);
                    GraphicsHelper.SaveOriginalImageWithDefinedDimentions(filePath, fileName, file, 560, 338, ScaleMode.Crop);
                    article.ImageSrc = fileName;
                }
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

      

        public ActionResult Delete(int id)
        {
            var article = _context.Media.First(e => e.Id == id);
            ImageHelper.DeleteImage(article.ImageSrc);
            _context.Media.Remove(article);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
