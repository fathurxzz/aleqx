using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
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

        public ActionResult Content()
        {
            return View();
        }

        public ActionResult EditContentItem(int id, int? parentId, bool? horisontal, bool? isGalleryItem, int contentLevel)
        {
            ViewData["parentId"] = parentId;
            ViewData["horisontal"] = horisontal;
            ViewData["isGalleryItem"] = isGalleryItem ?? false;
            ViewData["contentLevel"] = contentLevel;
            using (var context = new ContentStorage())
            {
                var contentItem = context.Content.Where(c => c.Id == id).Select(c => c).FirstOrDefault();
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
                content.Title = title;
                content.Description = description;
                content.Keywords = keywords;
                content.Text = HttpUtility.HtmlDecode(text);
                //content.IsGalleryItem = isGalleryItem;
                //content.Collapsible = collapsible;
                content.SortOrder = sortOrder;
                //if (horisontal.HasValue)
                    //content.Horisontal = horisontal.Value;
                if (content.Id == 0)
                    context.AddToContent(content);
                context.SaveChanges();

                return RedirectToAction("Index", "Content", new { id = contentId });
            }
        }

    }
}
