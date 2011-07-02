using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Babich.Models;

namespace Babich.Areas.Admin.Controllers
{
    public class ContentController : Controller
    {
        //
        // GET: /Admin/Content/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add(int id)
        {
            ViewData["id"] = id;
            return View(new Content());
        }

        [HttpPost]
        public ActionResult Add(int id, FormCollection form)
        {
            using (var context = new ContentStorage())
            {
                var parentContent = context.Content.Where(c => c.Id == id).First();

                var content = new Content {ContentLevel = 1};
                TryUpdateModel(content, new string[] { "Name", "PageTitle", "PageTitleEng", "Title", "TitleEng", "SeoKeywords", "SeoKeywordsEng", "SeoDescription", "SeoDescriptionEng", "SortOrder" });             
                content.Parent = parentContent;
                context.AddToContent(content);
                context.SaveChanges();

                return RedirectToAction("Index", "Home", new {area="", id = parentContent.Name});
            }
        }
        
        
        public ActionResult Edit(int id)
        {
            using (var context = new ContentStorage())
            {
                var content = context.Content.Where(c => c.Id == id).First();
                return View(content);
            }
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection form)
        {
            using (var context = new ContentStorage())
            {
                Content content = context.Content.Where(c => c.Id == id).FirstOrDefault();
                TryUpdateModel(content, new string[] { "Name", "PageTitle", "PageTitleEng", "Title", "TitleEng", "SeoKeywords", "SeoKeywordsEng", "SeoDescription", "SeoDescriptionEng", "SortOrder" });
                content.Text = HttpUtility.HtmlDecode(form["Text"]);
                content.TextEng = HttpUtility.HtmlDecode(form["TextEng"]);
                content.Description = HttpUtility.HtmlDecode(form["Description"]);
                content.DescriptionEng = HttpUtility.HtmlDecode(form["DescriptionEng"]);
                context.SaveChanges();
                return RedirectToAction("Index", "Home", new { area = "", id = content.Name });
            }
        }


        public ActionResult Delete(int id)
        {
            using (var context = new ContentStorage())
            {
                var content = context.Content.Include("Parent").Where(c => c.Id == id).First();
                string parentName = content.Parent.Name;
                context.DeleteObject(content);
                context.SaveChanges();
                return RedirectToAction("Index", "Home", new { area = "", id = parentName });
            }
        }

    }
}
