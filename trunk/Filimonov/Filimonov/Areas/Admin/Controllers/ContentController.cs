using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Filimonov.Models;

namespace Filimonov.Areas.Admin.Controllers
{
    [Authorize]
    public class ContentController : Controller
    {
        //
        // GET: /Admin/Content/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Admin/Content/Create

        [HttpPost]
        public ActionResult Create(FormCollection form)
        {
            try
            {
                using (var context  = new SiteContainer())
                {
                    var content = new Content();
                    TryUpdateModel(content, new[] { "Title", "SeoDescription", "SeoKeywords" });
                    content.Text = HttpUtility.HtmlDecode(form["Text"]);
                    context.AddToContent(content);
                    context.SaveChanges();
                    return RedirectToAction("Index", "Home", new {area = ""});
                }
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /Admin/Content/Edit/5
 
        public ActionResult Edit(int id)
        {
            using (var context = new SiteContainer())
            {
                var content = context.Content.First(c => c.Id == id);
                return View(content);
            }
        }

        //
        // POST: /Admin/Content/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection form)
        {
            try
            {
                using (var context = new SiteContainer())
                {
                    var content = context.Content.First(c => c.Id == id);
                    TryUpdateModel(content, new[] { "Title", "SeoDescription", "SeoKeywords" });
                    content.Text = HttpUtility.HtmlDecode(form["Text"]);
                    context.SaveChanges();
                    return RedirectToAction("Index", "Home", new { area = "" });
                }
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Admin/Content/Delete/5
 
        public ActionResult Delete(int id)
        {
            using (var context = new SiteContainer())
            {
                var content = context.Content.First(c => c.Id == id);
                context.DeleteObject(content);
                context.SaveChanges();
                return RedirectToAction("Index", "Home", new { area = "" });
            }
        }
        
    }
}
