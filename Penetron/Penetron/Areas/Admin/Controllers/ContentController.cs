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
            return RedirectToAction("SiteContent", "Home", new {area = "", id = content.Name});
        }

    }
}
