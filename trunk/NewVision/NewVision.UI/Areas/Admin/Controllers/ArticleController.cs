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
            return View(articles);
        }

        public ActionResult Create()
        {
            return View(new Article { Date = DateTime.Now });
        }

        [HttpPost]
        public ActionResult Create(Article model, HttpPostedFileBase file, IEnumerable<HttpPostedFileBase> files)
        {
            try
            {
                var article = new Article
                {
                    Title = model.Title,
                    TitleEn = model.TitleEn,
                    TitleUa = model.TitleUa,
                    Date = model.Date,
                    TitlePosition = model.TitlePosition,
                    VideoSrc = model.VideoSrc
                };

                article.Text = model.Text == null ? "" : HttpUtility.HtmlDecode(model.Text);
                article.TextEn = model.TextEn == null ? "" : HttpUtility.HtmlDecode(model.TextEn);
                article.TextUa = model.TextUa == null ? "" : HttpUtility.HtmlDecode(model.TextUa);

                if (file != null)
                {
                    string fileName = IOHelper.GetUniqueFileName("~/Content/Images", file.FileName);
                    string filePath = Server.MapPath("~/Content/Images");
                    filePath = Path.Combine(filePath, fileName);
                    GraphicsHelper.SaveOriginalImageWithDefinedDimentions(filePath, fileName, file, 340, 290, ScaleMode.Crop);
                    article.ImageSrc = fileName;
                }

                foreach (var f in files)
                {
                    if (f == null) continue;
                    string fileName = IOHelper.GetUniqueFileName("~/Content/Images", f.FileName);
                    string filePath = Server.MapPath("~/Content/Images");

                    filePath = Path.Combine(filePath, fileName);

                    // h: 283
                    // w: 400
                    GraphicsHelper.SaveOriginalImageWithDefinedDimentions(filePath, fileName, f, 400, 283, ScaleMode.Crop);

                    var ai = new ArticleImage
                    {
                        ImageSrc = fileName
                    };

                    article.ArticleImages.Add(ai);
                }

                _context.Articles.Add(article);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Admin/Article/Edit/5

        public ActionResult Edit(int id)
        {
            var article = _context.Articles.First(a => a.Id == id);
            return View(article);
        }

        //
        // POST: /Admin/Article/Edit/5

        [HttpPost]
        public ActionResult Edit(Article model, int id, HttpPostedFileBase file, IEnumerable<HttpPostedFileBase> files)
        {
            try
            {
                var article = _context.Articles.First(a => a.Id == id);

                TryUpdateModel(article, new[] { "Title", "TitleEn", "TitleUa", "Date", "VideoSrc", "TitlePosition" });
                
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
                    GraphicsHelper.SaveOriginalImageWithDefinedDimentions(filePath, fileName, file, 340, 290, ScaleMode.Crop);
                    article.ImageSrc = fileName;

                }

                foreach (var f in files)
                {
                    if (f == null) continue;
                    string fileName = IOHelper.GetUniqueFileName("~/Content/Images", f.FileName);
                    string filePath = Server.MapPath("~/Content/Images");

                    filePath = Path.Combine(filePath, fileName);
                    //GraphicsHelper.SaveOriginalImage(filePath, fileName, file, 1500);
                    GraphicsHelper.SaveOriginalImageWithDefinedDimentions(filePath, fileName, f, 400, 283, ScaleMode.Crop);

                    var ai = new ArticleImage
                    {
                        ImageSrc = fileName
                    };

                    article.ArticleImages.Add(ai);
                }

                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Admin/Article/Delete/5

        public ActionResult Delete(int id)
        {
            var article = _context.Articles.First(e => e.Id == id);
            ImageHelper.DeleteImage(article.ImageSrc);

            foreach (var image in article.ArticleImages)
            {
                ImageHelper.DeleteImage(image.ImageSrc);
            }

            _context.Articles.Remove(article);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteArticleImage(int id)
        {
            var articleImage = _context.ArticleImages.First(ai => ai.Id == id);
            ImageHelper.DeleteImage(articleImage.ImageSrc);
            _context.ArticleImages.Remove(articleImage);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

     
    }
}
