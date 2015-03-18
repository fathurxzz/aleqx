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
        public ActionResult Create(Article model, HttpPostedFileBase file)
        {
            try
            {
                var article = new Article
                {
                    Title = model.Title,
                    Date = model.Date,
                    Text = model.Text,
                    TitlePosition = model.TitlePosition,
                };

                if (file != null)
                {
                    string fileName = IOHelper.GetUniqueFileName("~/Content/Images", file.FileName);
                    string filePath = Server.MapPath("~/Content/Images");
                    filePath = Path.Combine(filePath, fileName);
                    GraphicsHelper.SaveOriginalImageWithDefinedDimentions(filePath, fileName, file, 340, 290, ScaleMode.Crop);
                    article.ImageSrc = fileName;
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
        public ActionResult Edit(int id, HttpPostedFileBase file)
        {
            try
            {
                var article = _context.Articles.First(a => a.Id == id);

                TryUpdateModel(article, new[] {"Title", "Date", "Text", "TitlePosition"});

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
            _context.Articles.Remove(article);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

     
    }
}
