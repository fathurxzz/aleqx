using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects.DataClasses;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using AvenueGreen.Helpers;
using AvenueGreen.Models;

namespace AvenueGreen.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        public ActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }

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
                    var galleryItem = new GalleryItems();
                    galleryItem.GalleryReference.EntityKey = new EntityKey("ContentStorage.Gallery", "Id", parentId);
                    galleryItem.ImageSource = newFileName;
                    context.AddToGalleryItems(galleryItem);
                    context.SaveChanges();
                }
            }
            return RedirectToAction("Index", "Content", new { id = contentId });
            /*return RedirectToAction("Index", "Galleries", new { id = contentId });*/
        }

        public ActionResult AddGallery(int parentId, string contentId,string galleryName)
        {
            using (var context = new ContentStorage())
            {
                var galleryItem = new Gallery();
                galleryItem.ContentReference.EntityKey = new EntityKey("ContentStorage.Content", "Id", parentId);
                galleryItem.Title = galleryName;
                galleryItem.ImageSource = "";
                context.AddToGallery(galleryItem);
                context.SaveChanges();
            }
            return RedirectToAction("Index", "Content", new { id = contentId });
        }

        public ActionResult DeleteGalleryItem(int id, string contentId)
        {
            using (var context = new ContentStorage())
            {
                GalleryItems galleryItem = context.GalleryItems.Select(g => g).Where(g => g.Id == id).FirstOrDefault();
                context.DeleteObject(galleryItem);
                context.SaveChanges();
                return RedirectToAction("Index", "Content", new { id = contentId });
            }
        }

        #endregion

        #region Content
        public ActionResult Content()
        {
            return View();
        }

        public ActionResult AddContentItem(int? parentId, int contentLevel, bool isGalleryItem)
        {
            ViewData["parentId"] = parentId;
            ViewData["contentLevel"] = contentLevel;
            ViewData["isGalleryItem"] = isGalleryItem;
            return View();
        }

        public ActionResult EditContentItem(int id, int? parentId, bool? horisontal, bool isGalleryItem, int contentLevel)
        {
            ViewData["parentId"] = parentId;
            ViewData["horisontal"] = horisontal;
            ViewData["isGalleryItem"] = isGalleryItem;
            ViewData["contentLevel"] = contentLevel;
            using (var context = new ContentStorage())
            {
                var contentItem = context.Content.Where(c => c.Id == id).Select(c => c).FirstOrDefault();
                return View(contentItem);
            }
        }


     

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UpdateContent(int id, bool isGalleryItem, int? parentId, string contentId, string title, string description, string keywords, string text, bool? horisontal, int sortOrder, int contentLevel)
        {
            using (var context = new ContentStorage())
            {

                Content parent = null;
                if (parentId != null)
                    parent = context.Content.Select(c => c).Where(c => c.Id == parentId).First();
                Content content = id != int.MinValue ? context.Content.Select(c => c).Where(c => c.Id == id).First() : new Content();
                content.Parent = parent;
                content.ContentId = contentId;
                content.Title = title;
                content.Description = description;
                content.Keywords = keywords;
                content.Text = HttpUtility.HtmlDecode(text);
                content.ContentLevel = contentLevel;
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
        #endregion

        #region News
        public ActionResult AddEditArticle(string id)
        {
            string title = "Создание новости";
            ViewData["isNew"] = string.IsNullOrEmpty(id);
            ViewData["id"] = id;
            if (!string.IsNullOrEmpty(id))
            {
                using (ContentStorage context = new ContentStorage())
                {
                    Article article = context.Article.Where(a => a.Name == id).First();
                    title = string.Format("Редактирование новости \"{0}\"", article.Title);
                    ViewData["title"] = article.Title;
                    ViewData["date"] = article.Date.ToString("dd.MM.yyyy");
                    ViewData["text"] = article.Text;
                    ViewData["description"] = article.Description;
                    ViewData["keywords"] = article.Keywords;
                }
            }
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
                    article = context.Article.Where(a => a.Name == id).First();
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

        public ActionResult DeleteArticle(string id)
        {
            using (ContentStorage context = new ContentStorage())
            {
                List<Article> articles = context.Article.Where(a => a.Name == id).ToList();

                foreach (var item in articles)
                {
                    context.DeleteObject(item);
                }
                context.SaveChanges();
            }
            return RedirectToAction("Index", "News");
        }
        #endregion

    }
}
