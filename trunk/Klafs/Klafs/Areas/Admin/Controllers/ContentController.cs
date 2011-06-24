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

                TryUpdateModel(content, new string[] { "Name", "PageTitle", "Title", "SeoKeywords", "SeoDescription", "MenuTitle", "SortOrder" });
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


                while (content.GalleryItems.Any())
                {
                    context.DeleteObject(content.GalleryItems.First());
                }

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

                TryUpdateModel(content, new string[] { "Name", "PageTitle", "Title", "SeoKeywords", "SeoDescription", "Description", "Sign", "MenuTitle", "SortOrder" });
                content.Text = HttpUtility.HtmlDecode(form["Text"]);
                content.SeoText = HttpUtility.HtmlDecode(form["SeoText"]);
                context.SaveChanges();

                return RedirectToAction("Index", "Home", new { area = "", id = content.Name });
            }
        }

        public ActionResult AddPhoto(int id)
        {


            using (var context = new ContentStorage())
            {
                ViewData["contentId"] = id;

                Content content = context.Content.Include("GalleryItems").Where(c => c.Id == id).FirstOrDefault();



                int sortOrder = content.GalleryItems.Max(c => c.SortOrder).HasValue ? content.GalleryItems.Max(c => c.SortOrder).Value : -1;
                ViewData["sortOrder"] = (sortOrder + 1).ToString();
                return View();
            }


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
                    content.GalleryItems.Add(new GalleryItem { Description = form["description"], ImageSource = fileName, SortOrder = Convert.ToInt32(form["sortOrder"]) });
                    context.SaveChanges();
                }

                return RedirectToAction("Index", "Home", new { area = "", id = content.Name });
            }
        }

        public ActionResult EditPhoto(int id, string contentName)
        {
            using (var context = new ContentStorage())
            {
                ViewData["contentName"] = contentName;
                var photo = context.GalleryItem.Include("Content").Where(p => p.Id == id).First();
                return View(photo);
            }
        }

        [HttpPost]
        public ActionResult EditPhoto(FormCollection form)
        {
            int id = Convert.ToInt32(form["id"]);

            using (var context = new ContentStorage())
            {
                GalleryItem gItem = context.GalleryItem.Where(g => g.Id == id).FirstOrDefault();
                gItem.Description = form["description"];
                gItem.SortOrder = Convert.ToInt32(form["sortOrder"]);
                if (Request.Files["logo"] != null && !string.IsNullOrEmpty(Request.Files["logo"].FileName))
                {
                    string fileName = IOHelper.GetUniqueFileName("~/Content/Photos", Request.Files["logo"].FileName);
                    string filePath = Server.MapPath("~/Content/Photos");
                    filePath = Path.Combine(filePath, fileName);
                    Request.Files["logo"].SaveAs(filePath);
                    gItem.ImageSource = fileName;
                }
                context.SaveChanges();
                return RedirectToAction("Index", "Home", new { area = "", id = form["contentName"] });
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
