using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Honda.Models;
using Honda.Helpers;
using System.IO;
using System.Data;

namespace Honda.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        public ActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }

        #region Content

        public ActionResult Content()
        {
            return View();
        }

        public ActionResult AddContentItem(int? parentId, bool? horisontal, bool isGalleryItem)
        {
            ViewData["parentId"] = parentId;
            ViewData["horisontal"] = (horisontal.HasValue) ? horisontal.Value : false;
            ViewData["isGalleryItem"] = isGalleryItem;
            return View();
        }

        public ActionResult EditContentItem(int id, int? parentId, bool? horisontal, bool? isGalleryItem)
        {
            ViewData["parentId"] = parentId;
            ViewData["horisontal"] = horisontal;
            ViewData["isGalleryItem"] = isGalleryItem ?? false;
            using (var context = new ContentStorage())
            {
                var contentItem = context.Content.Where(c => c.Id == id).Select(c => c).FirstOrDefault();
                ViewData["contentId"] = contentItem.ContentId;
                return View(contentItem);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UpdateContent(int id, bool isGalleryItem, int? parentId, string contentId, string title, string description, string keywords, string text, bool? horisontal, int sortOrder)
        {
            using (var context = new ContentStorage())
            {

                Content parent = null;
                if (parentId != null)
                    parent = context.Content.Select(c => c).Where(c => c.Id == parentId).First();
                Content content = id != int.MinValue ? context.Content.Select(c => c).Where(c => c.Id == id).First() : new Content();
                content.Parent = parent;
                content.ContentId = contentId;
                if (horisontal.HasValue)
                    content.Horisontal = horisontal.Value;
                content.Title = title;
                content.Description = description;
                content.Keywords = keywords;
                content.Text = HttpUtility.HtmlDecode(text);
                content.IsGalleryItem = isGalleryItem;
                content.SortOrder = sortOrder;
                if (content.Id == 0)
                    context.AddToContent(content);
                context.SaveChanges();

                return RedirectToAction("Index", "Content", new { id = contentId });
            }
        }

        public ActionResult DeleteContentItem(int id)
        {
            using (var context = new ContentStorage())
            {
                Content content = context.Content.Include("Children").Where(c => c.Id == id).FirstOrDefault();
                string contentId = content.ContentId;
                if (content.Children.Count == 0)
                {
                    context.DeleteObject(content);
                    context.SaveChanges();
                }
                return RedirectToAction("Index", "Content", new { id = "About" });
            }

        }

        #endregion;

        #region Gallery

        public ActionResult AddGalleryItem(int parentId, string contentId)
        {
            string file = Request.Files["image"].FileName;
            if (!string.IsNullOrEmpty(file))
            {
                string newFileName = IOHelper.GetUniqueFileName("~/Content/GalleryImages", file);
                string filePath = Path.Combine(Server.MapPath("~/Content/GalleryImages"), newFileName);
                Request.Files["image"].SaveAs(filePath);
                using (var context = new ContentStorage())
                {
                    var galleryItem = new Gallery();
                    galleryItem.ContentReference.EntityKey = new EntityKey("ContentStorage.Content", "Id", parentId);
                    galleryItem.ImageSource = newFileName;
                    context.AddToGallery(galleryItem);
                    context.SaveChanges();
                }
            }
            return RedirectToAction("Index", "Content", new { id = contentId });
        }

        public ActionResult DeleteGalleryItem(int id, string contentId)
        {
            using (var context = new ContentStorage())
            {
                Gallery galleryItem = context.Gallery.Select(g => g).Where(g => g.Id == id).FirstOrDefault();
                context.DeleteObject(galleryItem);
                context.SaveChanges();
                return RedirectToAction("Index", "Content", new { id = contentId });
            }
        }

        #endregion

        #region News

        public ActionResult AddEditArticle(string id)
        {
            string title = "�������� �������";
            ViewData["isNew"] = string.IsNullOrEmpty(id);
            ViewData["id"] = id;
            if (!string.IsNullOrEmpty(id))
            {
                int newsId = Convert.ToInt32(id);
                using (ContentStorage context = new ContentStorage())
                {
                    Article article = context.Article.Where(a => a.Id == newsId).First();
                    title = string.Format("�������������� ������� \"{0}\"", article.Title);
                    ViewData["title"] = article.Title;
                    ViewData["date"] = article.Date.ToString("dd.MM.yyyy");
                    ViewData["text"] = article.Text;
                    ViewData["description"] = article.Description;
                    ViewData["keywords"] = article.Keywords;
                }
            }
            else
            { ViewData["date"] = DateTime.Now.Date.ToString("dd.MM.yyyy"); }
            ViewData["cTitle"] = title;
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AddEditArticle(string id,
            string title,
            string date,
            string keywords,
            string description,
            string text,
            bool isNew) 
        {

            using (ContentStorage context = new ContentStorage())
            {
                Article article;
                if (isNew)
                {
                    article = new Article();
                    article.Name = id;
                    context.AddToArticle(article);
                }
                else
                {
                    int newsId = Convert.ToInt32(id);
                    article = context.Article.Where(a => a.Id == newsId).First();
                }

                article.Title = title;
                article.Date = DateTime.Parse(date);
                article.Text = HttpUtility.HtmlDecode(text);
                article.Description = description;
                article.Keywords = keywords;
                context.SaveChanges();
            }
            return RedirectToAction("Index", "News");
        }

        public ActionResult DeleteArticle(int id)
        {
            using (ContentStorage context = new ContentStorage())
            {
                List<Article> articles = context.Article.Where(a => a.Id == id).ToList();

                foreach (var item in articles)
                {
                    context.DeleteObject(item);
                }
                context.SaveChanges();
            }
            return RedirectToAction("Index", "News");
        }
        #endregion;
    }
}
