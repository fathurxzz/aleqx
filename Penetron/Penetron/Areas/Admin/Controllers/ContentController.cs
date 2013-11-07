using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Penetron.Models;

namespace Penetron.Areas.Admin.Controllers
{
    public class ContentController : Controller
    {
        private SiteContext _context;

        public ContentController(SiteContext context)
        {
            _context = context;
        }

        public ActionResult Edit(int id)
        {
            var content = _context.Content.First(c => c.Id == id);
            return View(content);
        }

        [HttpPost]
        public ActionResult Edit(Content model)
        {
            var content = _context.Content.First(c => c.Id == model.Id);
            content.Title = model.Title;
            content.Text = HttpUtility.HtmlDecode(model.Text);
            content.SeoDescription = model.SeoDescription;
            content.SeoKeywords = model.SeoKeywords;
            _context.SaveChanges();
            return RedirectToAction("SiteContent", "Home", new { area = "", id = content.Name });
        }

        public ActionResult AddContentItem(int id)
        {
            var content = _context.Content.First(c => c.Id == id);

            ViewBag.ContentId = content.Id;
            ViewBag.ContentName = content.Name;

            return View(new ContentItem());
        }

        [HttpPost]
        public ActionResult AddContentItem(int contentId, ContentItem model)
        {
            var content = _context.Content.First(c => c.Id == contentId);

            var ci = new ContentItem
                     {
                         SortOrder = model.SortOrder,
                         Text = HttpUtility.HtmlDecode(model.Text)
                     };
            content.ContentItems.Add(ci);
            _context.SaveChanges();
            return RedirectToAction("SiteContent", "Home", new { area="", id = content.Name });
        }


        public ActionResult EditContentItem(int id)
        {
            var ci = _context.ContentItem.Include("Content").First(c => c.Id == id);
            return View(ci);
        }

        [HttpPost]
        public ActionResult EditContentItem(ContentItem model)
        {
            var ci = _context.ContentItem.Include("Content").First(c => c.Id == model.Id);
            ci.SortOrder = model.SortOrder;
            ci.Text = HttpUtility.HtmlDecode(model.Text);
            _context.SaveChanges();
            return RedirectToAction("SiteContent", "Home", new { area = "", id = ci.Content.Name });
        }

        public ActionResult DeleteContentItem(int id)
        {
            var ci = _context.ContentItem.Include("Content").First(c => c.Id == id);
            var cName = ci.Content.Name;
            _context.DeleteObject(ci);
            _context.SaveChanges();
            return RedirectToAction("SiteContent", "Home", new { area = "", id = cName });
        }

    }
}
