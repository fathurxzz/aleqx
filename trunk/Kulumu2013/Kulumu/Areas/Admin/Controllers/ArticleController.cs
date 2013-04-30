using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kulumu.Models;
using SiteExtensions;

namespace Kulumu.Areas.Admin.Controllers
{
    [Authorize]
    public class ArticleController : Controller
    {
        public ActionResult Create()
        {
            return View(new Article { Date = DateTime.Now });
        }

        [HttpPost]
        public ActionResult Create(FormCollection form, HttpPostedFileBase fileUpload)
        {

            using (var context = new SiteContainer())
            {
                var article = new Article { Name = "" };
                TryUpdateModel(article, new[] { "Name", "Title", "Date", "Description","PageTitle"});
                article.Text = HttpUtility.HtmlDecode(form["Text"]);
                article.OldPrice = HttpUtility.HtmlDecode(form["OldPrice"]);
                article.NewPrice = HttpUtility.HtmlDecode(form["NewPrice"]);
                if (fileUpload != null)
                {
                    string fileName = IOHelper.GetUniqueFileName("~/Content/Images", fileUpload.FileName);
                    string filePath = Server.MapPath("~/Content/Images");
                    filePath = Path.Combine(filePath, fileName);
                    fileUpload.SaveAs(filePath);
                    article.ImageSource = fileName;
                }
                context.AddToArticle(article);
                context.SaveChanges();
            }
            return RedirectToAction("Articles", "Home", new { area = "" });

        }

        public ActionResult Edit(int id)
        {
            using (var context = new SiteContainer())
            {
                var article = context.Article.First(a => a.Id == id);
                return View(article);
            }
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection form, HttpPostedFileBase fileUpload)
        {
            using (var context = new SiteContainer())
            {
                var article = context.Article.First(a => a.Id == id);
                TryUpdateModel(article, new[] { "Name", "Title", "Date", "Description", "PageTitle" });
                article.OldPrice = HttpUtility.HtmlDecode(form["OldPrice"]);
                article.NewPrice = HttpUtility.HtmlDecode(form["NewPrice"]);
                article.Text = HttpUtility.HtmlDecode(form["Text"]);

                if (fileUpload != null)
                {
                    if (!string.IsNullOrEmpty(article.ImageSource))
                    {

                        IOHelper.DeleteFile("~/Content/Images", article.ImageSource);
                        foreach (var thumbnail in SiteSettings.Thumbnails)
                        {
                            IOHelper.DeleteFile("~/ImageCache/" + thumbnail.Key, article.ImageSource);
                        }
                    }
                    string fileName = IOHelper.GetUniqueFileName("~/Content/Images", fileUpload.FileName);
                    string filePath = Server.MapPath("~/Content/Images");
                    filePath = Path.Combine(filePath, fileName);
                    fileUpload.SaveAs(filePath);
                    article.ImageSource = fileName;
                }

                context.SaveChanges();
                return RedirectToAction("Articles","Home",new{area=""});
            }
        }

        public ActionResult Delete(int id)
        {
            using (var context = new SiteContainer())
            {
                var article = context.Article.First(a => a.Id == id);
                if (!string.IsNullOrEmpty(article.ImageSource))
                {

                    IOHelper.DeleteFile("~/Content/Images", article.ImageSource);
                    foreach (var thumbnail in SiteSettings.Thumbnails)
                    {
                        IOHelper.DeleteFile("~/ImageCache/" + thumbnail.Key, article.ImageSource);
                    }
                }
                context.DeleteObject(article);
                context.SaveChanges();
                return RedirectToAction("Articles", "Home", new { area = "" });
            }
        }
    }
}
