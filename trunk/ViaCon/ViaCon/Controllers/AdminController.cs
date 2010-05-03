using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using ViaCon.Models;
using System.Web.Script.Serialization;

namespace ViaCon.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        //
        // GET: /Admin/


        public ActionResult EditContent()
        {
            return View();
        }

        public ActionResult EditContentItem(int id, int? parentId, bool? horisontal)
        {
            ViewData["parentId"] = parentId;
            ViewData["horisontal"] = horisontal;

            using (var context = new ContentStorage())
            {
                var contentItem = context.Content.Where(c => c.Id == id).Select(c => c).FirstOrDefault();
                return View(contentItem);
            }
        }

        public ActionResult AddContentItem(int? parentId, bool? horisontal)
        {
            ViewData["parentId"] = parentId;
            ViewData["horisontal"] = horisontal;
            return View();
        }



        public ActionResult ContentList(int? id, int level)
        {
            using (var context = new ContentStorage())
            {
                List<Content> contentList = context.Content.Select(c => c).ToList();
                if (id == null)
                    contentList = contentList.Select(c => c).Where(c => c.Parent == null).ToList();
                else
                    contentList = contentList.Select(c => c).Where(c => c.Parent != null && c.Parent.Id == id.Value).ToList();
                ViewData["level"] = level;
                if (id != null)
                    ViewData["id"] = id.Value;
                return View(contentList);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UpdateContent(int id, int? parentId, string contentId, string title, string description, string keywords, string text, /*bool collapsible,*/ bool? horisontal)
        {
            using (var context = new ContentStorage())
            {

                Content parent = null;
                if(parentId!=null)
                    parent = context.Content.Select(c => c).Where(c => c.Id == parentId).First();
                Content content;
                if (id != int.MinValue)
                    content = context.Content.Select(c => c).Where(c => c.Id == id).First();
                else
                    content = new Content();
                content.Parent = parent;
                content.ContentId = contentId;
                content.Title = title;
                content.Description = description;
                content.Keywords = keywords;
                content.Text = text;
                //content.Collapsible=collapsible;
                if (horisontal.HasValue)
                    content.Horisontal = horisontal.Value;
                if (content.Id == 0)
                    context.AddToContent(content);
                context.SaveChanges();
            }
            return RedirectToAction("EditContent");
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult InsertContentItem(FormCollection form)
        {
            using (var context = new ContentStorage())
            {
                int parentId = int.Parse(form["parentId"]);
                Content parent = null;
                if (parentId >= 0)
                    parent = context.Content.Select(c => c).Where(c => c.Id == parentId).First();
                Content content = new Content();
                content.Parent = parent;
                content.ContentId = form["contentId"];
                content.Title = form["title"];
                context.AddToContent(content);
                context.SaveChanges();
            }
            return RedirectToAction("EditContent");
        }

        public ActionResult DeleteContentItem(int id)
        {
            using (ContentStorage context = new ContentStorage())
            {
                Content content = context.Content.Include("Children").Where(c => c.Id == id).FirstOrDefault();
                if (content.Children.Count == 0)
                {
                    context.DeleteObject(content);
                    context.SaveChanges();
                }
            }
            return RedirectToAction("EditContent");
        }
    }
}
