using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
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

        public ActionResult AddContentItem()
        {
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
        public ActionResult UpdateContent(int id, string contentId, string title, string description, string keywords, string text, int sortOrder)
        {
            using (var context = new ContentStorage())
            {
                Content content = id != int.MinValue ? context.Content.Select(c => c).Where(c => c.Id == id).First() : new Content();
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
                Content content = context.Content.Include("Children").Where(c => c.Id == id).FirstOrDefault();
                context.DeleteObject(content);
                context.SaveChanges();
                return RedirectToAction("Index", "Content", new { id = "About" });
            }

        }
        
    }
}
