using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FashionIntention.UI.Helpers;
using FashionIntention.UI.Models;

namespace FashionIntention.UI.Areas.Admin.Controllers
{
    public class ArticleItemController : AdminController
    {

        private readonly SiteContext _context;

        public ArticleItemController(SiteContext context)
        {
            _context = context;
        }

        public ActionResult Create(int id)
        {
            var post = _context.Articles.First(p => p.Id == id);
            return View(new ArticleItem { Article = post, ArticleId = post.Id });
        }

        [HttpPost]
        public ActionResult Create(int articleid, ArticleItem model, HttpPostedFileBase file)
        {
            try
            {
                var post = _context.Articles.First(p => p.Id == articleid);
                var postItem = new ArticleItem
                {
                    Text = model.Text == null ? "" : HttpUtility.HtmlDecode(model.Text),
                    Article = post
                };

                if (file != null)
                {
                    string fileName = IOHelper.GetUniqueFileName("~/Content/Images", file.FileName);
                    string filePath = Server.MapPath("~/Content/Images");
                    filePath = Path.Combine(filePath, fileName);
                    //file.SaveAs(filePath);
                    GraphicsHelper.SaveOriginalImageWithDefinedDimentions(filePath, fileName, file, 556, 0, ScaleMode.FixedWidth);
                    postItem.ImageSrc = fileName;
                }

                _context.ArticleItems.Add(postItem);
                _context.SaveChanges();

                return RedirectToAction("Details", "Article", new { id = articleid });
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            var postItem = _context.ArticleItems.First(p => p.Id == id);
            return View(postItem);
        }

        [HttpPost]
        public ActionResult Edit(int id, ArticleItem model, HttpPostedFileBase file)
        {
            try
            {
                var postItem = _context.ArticleItems.First(p => p.Id == id);

                postItem.Text = model.Text == null ? "" : HttpUtility.HtmlDecode(model.Text);

                if (file != null)
                {
                    if (!string.IsNullOrEmpty(postItem.ImageSrc))
                    {
                        ImageHelper.DeleteImage(postItem.ImageSrc);
                    }

                    string fileName = IOHelper.GetUniqueFileName("~/Content/Images", file.FileName);
                    string filePath = Server.MapPath("~/Content/Images");
                    filePath = Path.Combine(filePath, fileName);
                    //file.SaveAs(filePath);
                    GraphicsHelper.SaveOriginalImageWithDefinedDimentions(filePath, fileName, file, 556, 0, ScaleMode.FixedWidth);
                    postItem.ImageSrc = fileName;

                }

                _context.SaveChanges();

                return RedirectToAction("Details", "Article", new { id = postItem.ArticleId });
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            var postItem = _context.ArticleItems.First(p => p.Id == id);
            var postId = postItem.ArticleId;
            if (!string.IsNullOrEmpty(postItem.ImageSrc))
            {
                ImageHelper.DeleteImage(postItem.ImageSrc);
            }
            _context.ArticleItems.Remove(postItem);
            _context.SaveChanges();
            return RedirectToAction("Details", "Article", new { id = postId });
        }
    }
}
