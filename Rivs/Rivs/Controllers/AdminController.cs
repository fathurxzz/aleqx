using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rivs.Models;

namespace Rivs.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Content()
        {
            return View();
        }

        public ActionResult AddContentItem(int? parentId)
        {
            ViewData["parentId"] = parentId;
            return View();
        }

        
        public ActionResult EditContentItem(int id)
        {
            using (var context = new ContentStorage())
            {
                var contentItem = context.Content.Where(c => c.Id == id).Select(c => c).FirstOrDefault();
                return View(contentItem);
            }
        }
        
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UpdateContent(int id, int? parentId, string contentId, string title, string description, string keywords, string text, int sortOrder)
        {
            using (var context = new ContentStorage())
            {
                Content parent = null;
                if (parentId != null)
                {
                    parent = context.Content.Select(c => c).Where(c => c.Id == parentId).First();
                }

                Content content = id != int.MinValue ? context.Content.Select(c => c).Where(c => c.Id == id).First() : new Content();
                content.Parent = parent;
                content.ContentId = contentId;
                content.Title = title;
                content.Description = description;
                content.Keywords = keywords;
                content.Text = HttpUtility.HtmlDecode(text);
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
                Content content = context.Content.Where(c => c.Id == id).FirstOrDefault();
                context.DeleteObject(content);
                context.SaveChanges();
                return RedirectToAction("Index", "Content", new { id = "About" });
            }

        }


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
            else
            {
                ViewData["date"] = DateTime.Now.ToString("dd.MM.yyyy");
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
                    article = new Article {Name = id};
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
