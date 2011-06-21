using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dev.Mvc.Helpers;
using Klafs.Models;

namespace Klafs.Areas.Admin.Controllers
{
    public class ContentController : Controller
    {
        //
        // GET: /Admin/Content/

        public ActionResult AddContent(int id)
        {
            ViewData["id"] = id;
            return View();
        }


        [HttpPost]
        public ActionResult AddContent(FormCollection form)
        {
            using (var context = new ContentStorage())
            {
                Content content = new Models.Content();

                Int64 parentId = Convert.ToInt64(form["id"]);

                Content parentContent = context.Content.Where(c => c.Id == parentId).FirstOrDefault();

                content.Parent = parentContent;

                TryUpdateModel(content, new string[] { "Name", "PageTitle", "Title", "SeoKeywords", "SeoDescription", "Description", "Sign", "Sign2", "MenuTitle","SortOrder" });
                content.ContentType = 4;
                
                content.Text = HttpUtility.HtmlDecode(form["Text"]);
                content.SeoText = HttpUtility.HtmlDecode(form["SeoText"]);
                context.AddToContent(content);
                context.SaveChanges();

                return RedirectToAction("Index", "Home", new { area = "", id = content.Name });
            }
        }


        public ActionResult DeleteContent(int id)
        {
            using (var context = new ContentStorage())
            {
                Content content = context.Content.Where(c => c.Id == id).FirstOrDefault();
                context.DeleteObject(content);
                context.SaveChanges();
                return RedirectToAction("Index", "Home", new { area = "", id = "" });
            }
        }


        public ActionResult EditContent(int? id)
        {
            using (var context = new ContentStorage())
            {
                Content content;

                if (id == null)
                {
                    content = context.Content.Where(c => c.Id == 1).First();
                }
                else
                {
                    content = context.Content.Where(c => c.Id == id).FirstOrDefault();
                }

                return View(content);
            }
        }


        [HttpPost]
        public ActionResult EditContent(int id, FormCollection form)
        {
            using (var context = new ContentStorage())
            {
                Content content = context.Content.Where(c => c.Id == id).FirstOrDefault();

                TryUpdateModel(content, new string[] { "Name", "PageTitle", "Title", "SeoKeywords", "SeoDescription", "Description", "Sign", "Sign2", "MenuTitle", "SortOrder" });
                content.Text = HttpUtility.HtmlDecode(form["Text"]);
                content.SeoText = HttpUtility.HtmlDecode(form["SeoText"]);
                context.SaveChanges();

                return RedirectToAction("Index", "Home", new { area = "", id = content.Name });
            }
        }

        public ActionResult AddPhoto(int id)
        {
            ViewData["contentId"] = id;
            return View();
        }

        [HttpPost]
        public ActionResult AddPhoto(int contentId, FormCollection form)
        {
            using (var context = new ContentStorage())
            {
                Content content = context.Content.Where(c => c.Id == contentId).FirstOrDefault();

                if (Request.Files["logo"] != null && !string.IsNullOrEmpty(Request.Files["logo"].FileName))
                {
                    string fileName = IOHelper.GetUniqueFileName("~/Content/Photos", Request.Files["logo"].FileName);
                    string filePath = Server.MapPath("~/Content/Photos");
                    filePath = Path.Combine(filePath, fileName);
                    Request.Files["logo"].SaveAs(filePath);
                    content.GalleryItems.Add(new GalleryItem{Description = form["description"],ImageSource = fileName});
                    context.SaveChanges();
                }

                return RedirectToAction("Index", "Home", new {area = "", id = content.Name});
            }
        }

        public ActionResult DeletePhoto(int id)
        {
            using (var context = new ContentStorage())
            {
                var photo = context.GalleryItem.Include("Content").Where(p => p.Id == id).First();
                long dcId = photo.Content.Id;
                var content = context.Content.Where(dc => dc.Id == dcId).First();
                if (!string.IsNullOrEmpty(photo.ImageSource))
                {
                    IOHelper.DeleteFile("~/Content/Photos", photo.ImageSource);
                }
                context.DeleteObject(photo);
                context.SaveChanges();
                return RedirectToAction("Index", "Home", new { area = "", id = content.Name });
            }
        }

    }
}
