using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dev.Mvc.Helpers;
using DjSzk.Models;

namespace DjSzk.Areas.Admin.Controllers
{
    [Authorize]
    public class ContentController : Controller
    {
        //
        // GET: /Admin/Content/

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
                var content = context.Content.Where(c => c.Id == id).First();
                TryUpdateModel(content, new[]
                                   {
                                       "Name", 
                                       "Title", 
                                       "MenuTitle", 
                                       "PageTitle", 
                                       "SortOrder", 
                                       "SeoDescription",
                                       "SeoKeywords"
                                   });
                content.Text = HttpUtility.HtmlDecode(form["Text"]);
                context.SaveChanges();
                return RedirectToAction("Index", "Home", new {id = content.Name, area=""});
            }
        }

        public ActionResult AddMusicContent(int id)
        {
            using (var context = new ContentStorage())
            {
                var content = context.Content.Include("MusicContent").Where(c => c.Id == id).First();
                ViewData["contentId"] = id;
                int sortOrder = 0;
                if (content.MusicContent.Count > 0)
                    sortOrder = content.MusicContent.Max(c => c.SortOrder);
                return View(new MusicContent{SortOrder = sortOrder+1});
            }
        }

        [HttpPost]
        public ActionResult AddMusicContent(int id, FormCollection form)
        {
            using (var context = new ContentStorage())
            {
                var content = context.Content.Where(c => c.Id == id).First();
                if (Request.Files["logo"] != null && !string.IsNullOrEmpty(Request.Files["logo"].FileName))
                {
                    string fileName = IOHelper.GetUniqueFileName("~/Content/Files", Request.Files["logo"].FileName);
                    string filePath = Server.MapPath("~/Content/Files");
                    filePath = Path.Combine(filePath, fileName);
                    Request.Files["logo"].SaveAs(filePath);
                    content.MusicContent.Add(new MusicContent{ Description = form["description"], FileSource = fileName ,Title = form["Title"],SortOrder = Convert.ToInt32(form["SortOrder"])});
                    context.SaveChanges();
                }
                return RedirectToAction("Index", "Home", new {id = content.Name, area = ""});
            }
        }





        public ActionResult DeleteMusicContent(int id)
        {
            using (var context = new ContentStorage())
            {
                var mc = context.MusicContent.Include("Content").Where(c => c.Id == id).First();
                var contentName = mc.Content.Name;
                if(!string.IsNullOrEmpty(mc.FileSource))
                {
                    IOHelper.DeleteFile("~/Content/Files", mc.FileSource);
                    context.DeleteObject(mc);
                }
                context.SaveChanges();
                return RedirectToAction("Index", "Home", new { id = contentName, area = "" });
            }
        }


    }
}
