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
    public class ContentController : AdminController
    {
        private readonly SiteContext _context;

        public ContentController(SiteContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            var contents = _context.Contents.ToList();
            return View(contents);
        }

        //
        // GET: /Admin/Partnership/Create

        public ActionResult Create()
        {
            return View(new Content{SortOrder = 0});
        }

        //
        // POST: /Admin/Partnership/Create

        [HttpPost]
        public ActionResult Create(Content model, HttpPostedFileBase file)
        {
            try
            {
                var content = new Content
                {
                    Title = model.Title,
                    TitleEn = model.TitleEn,
                    TitleUa = model.TitleUa,
                    Name = model.Name,
                    MenuTitle = model.MenuTitle,
                    MenuTitleEn = model.MenuTitleEn,
                    MenuTitleUa = model.MenuTitleUa,
                    SortOrder = model.SortOrder
                };

                content.Text = model.Text == null ? "" : HttpUtility.HtmlDecode(model.Text);
                content.TextEn = model.TextEn == null ? "" : HttpUtility.HtmlDecode(model.TextEn);
                content.TextUa = model.TextUa == null ? "" : HttpUtility.HtmlDecode(model.TextUa);

                if (file != null)
                {
                    string fileName = IOHelper.GetUniqueFileName("~/Content/Images", file.FileName);
                    string filePath = Server.MapPath("~/Content/Images");
                    filePath = Path.Combine(filePath, fileName);
                    GraphicsHelper.SaveOriginalImage(filePath, fileName, file, 1500);
                    content.ImageSrc = fileName;
                }
                _context.Contents.Add(content);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Admin/Partnership/Edit/5

        public ActionResult Edit(int id)
        {
            var article = _context.Contents.First(e => e.Id == id);
            return View(article);
        }

        //
        // POST: /Admin/Partnership/Edit/5

        [HttpPost]
        public ActionResult Edit(Content model, int id, HttpPostedFileBase file)
        {
            try
            {
                var article = _context.Contents.First(e => e.Id == id);
                TryUpdateModel(article, new[] { "Title", "TitleEn", "TitleUa", "Name", "MenuTitle", "MenuTitleEn", "MenuTitleUa", "SortOrder" });
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
                    GraphicsHelper.SaveOriginalImage(filePath, fileName, file, 1500);
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
        // GET: /Admin/Partnership/Delete/5

        public ActionResult Delete(int id)
        {
            var article = _context.Contents.First(e => e.Id == id);
            ImageHelper.DeleteImage(article.ImageSrc);
            _context.Contents.Remove(article);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

      
    }
}
