using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Che.Models;
using Dev.Mvc.Helpers;

namespace Che.Areas.Admin.Controllers
{
    [Authorize]
    public class ContentController : Controller
    {
        public ActionResult Add(int id)
        {
            using (var context = new ContentStorage())
            {
                var content = context.Content.Where(c => c.Id == id).First();

                return View(new Content { ContentType = content.ContentType, ContentLevel = 1 });
            }
        }

        [HttpPost]
        public ActionResult Add(int id, FormCollection form)
        {
            using (var context = new ContentStorage())
            {
                var content = new Content();
                var parent = context.Content.Where(c => c.Id == id).First();
                content.Parent = parent;

                string parentName = parent.Name;

                TryUpdateModel(content,
                               new[]
                                   {
                                       "Name", "Title", "PageTitle", "ContentType", "ContentLevel", "Description",
                                       "SortOrder", "SeoDescription", "SeoKeywords"
                                   });


                if (Request.Files["logo"] != null && !string.IsNullOrEmpty(Request.Files["logo"].FileName))
                {
                    string fileName = IOHelper.GetUniqueFileName("~/Content/Photos", Request.Files["logo"].FileName);
                    string filePath = Server.MapPath("~/Content/Photos");
                    filePath = Path.Combine(filePath, fileName);
                    Request.Files["logo"].SaveAs(filePath);
                    content.ImageSource = fileName;
                }

                context.AddToContent(content);
                context.SaveChanges();

                return RedirectToAction("Index", "Home", new {area = "", id = parentName});
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
                var content = context.Content.Include("Parent").Where(c => c.Id == id).First();
                string parentName = "";
                if (content.Parent != null)
                    parentName = content.Parent.Name;
                string name = content.Name;
                TryUpdateModel(content, new[]
                                   {
                                       "Name", 
                                       "Title", 
                                       "PageTitle", 
                                       "Description", 
                                       "SortOrder", 
                                       "SeoDescription",
                                       "SeoKeywords"
                                   });
                content.Text = HttpUtility.HtmlDecode(form["Text"]);
                context.SaveChanges();

                return RedirectToAction("Index", "Home", new { area = "", id = parentName!=""?parentName: name });
            }
        }




        public ActionResult AddDetailsItem(int id)
        {
            using (var context = new ContentStorage())
            {
                var content = context.Content.Where(c => c.Id == id).First();

                return View(new Content { ContentType = content.ContentType, ContentLevel = 2 });
            }
        }

        [HttpPost]
        public ActionResult AddDetailsItem(int id, FormCollection form)
        {
            using (var context = new ContentStorage())
            {
                var parent = context.Content.Where(c => c.Id == id).First();
                string parentName = parent.Name;
                var content = new Content {Parent = parent, Name = "", ContentLevel = 3};

                TryUpdateModel(content, new[] { "SortOrder"});
                content.Description = HttpUtility.HtmlDecode(form["Description"]);

                if (Request.Files["logo"] != null && !string.IsNullOrEmpty(Request.Files["logo"].FileName))
                {
                    string fileName = IOHelper.GetUniqueFileName("~/Content/Photos", Request.Files["logo"].FileName);
                    string filePath = Server.MapPath("~/Content/Photos");
                    filePath = Path.Combine(filePath, fileName);
                    Request.Files["logo"].SaveAs(filePath);
                    content.ImageSource = fileName;
                }
                context.AddToContent(content);
                context.SaveChanges();

                return RedirectToAction("Content", "Home", new {area = "", id = parentName});
            }
        }

        public ActionResult EditDetailsItem(int id)
        {
            using (var context = new ContentStorage())
            {
                var content = context.Content.Where(c => c.Id == id).First();
                return View(content);
            }
        }
        [HttpPost]
        public ActionResult EditDetailsItem(int id, FormCollection form)
        {
            using (var context = new ContentStorage())
            {
                
                var content = context.Content.Include("Parent").Where(c => c.Id == id).First();
                string parentName = content.Parent.Name;
                TryUpdateModel(content, new[] { "Description", "SortOrder" });

                if (Request.Files["logo"] != null && !string.IsNullOrEmpty(Request.Files["logo"].FileName))
                {
                    if(!string.IsNullOrEmpty(content.ImageSource))
                    {
                        IOHelper.DeleteFile("~/Content/Photos", content.ImageSource);
                        IOHelper.DeleteFile("~/ImageCache/thumbnail1", content.ImageSource);
                        IOHelper.DeleteFile("~/ImageCache/thumbnail2", content.ImageSource);
                    }

                    string fileName = IOHelper.GetUniqueFileName("~/Content/Photos", Request.Files["logo"].FileName);
                    string filePath = Server.MapPath("~/Content/Photos");
                    filePath = Path.Combine(filePath, fileName);
                    //Request.Files["logo"].SaveAs(filePath);
                    content.ImageSource = fileName;
                }
                //context.SaveChanges();

                return RedirectToAction("Content", "Home", new { area = "", id = parentName });
            }
        }

        public ActionResult Delete(int id)
        {
            using (var context = new ContentStorage())
            {
                var content = context.Content.Include("Children").Where(c => c.Id == id).First();

                while (content.Children.Any())
                {
                    var child = content.Children.First();
                    if (!string.IsNullOrEmpty(child.ImageSource))
                    {
                        IOHelper.DeleteFile("~/Content/Photos", child.ImageSource);
                        IOHelper.DeleteFile("~/ImageCache/thumbnail1", child.ImageSource);
                        IOHelper.DeleteFile("~/ImageCache/thumbnail2", child.ImageSource);
                        context.DeleteObject(child);
                    }
                }


                if (!string.IsNullOrEmpty(content.ImageSource))
                {
                    IOHelper.DeleteFile("~/Content/Photos", content.ImageSource);
                    IOHelper.DeleteFile("~/ImageCache/thumbnail1", content.ImageSource);
                    IOHelper.DeleteFile("~/ImageCache/thumbnail2", content.ImageSource);
                }
                context.DeleteObject(content);
                context.SaveChanges();
            }
            return RedirectToAction("Index", "Home", new { area = "", id = "" });
        }




    }
}
