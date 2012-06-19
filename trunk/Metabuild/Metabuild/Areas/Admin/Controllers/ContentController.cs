using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Metabuild.Helpers;
using Metabuild.Models;

namespace Metabuild.Areas.Admin.Controllers
{
    [Authorize]
    public class ContentController : Controller
    {
        public ActionResult Add(int? parentId)
        {
            ViewData["parentId"] = parentId;
            return View(new Content { SortOrder = 0 });
        }

        [HttpPost]
        public ActionResult Add(FormCollection form)
        {
            using (var context = new StructureContainer())
            {
                var content = new Content { MainPage = false, ContentLevel = 1 };

                if (!string.IsNullOrEmpty(form["parentId"]))
                {
                    int parentId = Convert.ToInt32(form["parentId"]);
                    var parent = context.Content.Where(c => c.Id == parentId).First();
                    content.Parent = parent;
                    content.ContentLevel = parent.ContentLevel + 1;
                }

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
                
                context.AddToContent(content);

                context.SaveChanges();

                return RedirectToAction("Index", "Home", new { id = content.Name, area = "" });
            }
        }

        public ActionResult Edit(int id)
        {
            using (var context = new StructureContainer())
            {
                var content = context.Content.Where(c => c.Id == id).First();
                return View(content);
            }
        }

        [HttpPost]
        public ActionResult Edit(Content model, FormCollection form)
        {
            using (var context = new StructureContainer())
            {
                var content = context.Content.Where(c => c.Id == model.Id).First();

                TryUpdateModel(content, new[]
                                            {
                                                "Name",
                                                "Title",
                                                "PageTitle",
                                                "SortOrder",
                                                "SeoDescription",
                                                "SeoKeywords",
                                            });
                content.Text = HttpUtility.HtmlDecode(form["Text"]);


                context.SaveChanges();

                return RedirectToAction("Index", "Home", new { id = content.Name, area = "" });
            }
        }

        public ActionResult Delete(int id)
        {

            return RedirectToAction("Index", "Home", new { id = "", area = "" });
        }


        public ActionResult AddImage(int id)
        {
            using (var context = new StructureContainer())
            {
                ViewBag.ContentName = context.Content.First(c => c.Id == id).Name;
                ViewBag.parentId = id;
                return View();
            }
        }

        [HttpPost]
        public ActionResult AddImage(int parentId, IEnumerable<HttpPostedFileBase> fileUpload, IList<string> fileTitles)
        {
            using (var context = new StructureContainer())
            {
                var content = context.Content.Where(a => a.Id == parentId).First();


                foreach (var file in fileUpload)
                {

                    if (file == null) continue;

                    string fileName = IOHelper.GetUniqueFileName("~/Content/Images", file.FileName);
                    string filePath = Server.MapPath("~/Content/Images");
                    filePath = Path.Combine(filePath, fileName);
                    file.SaveAs(filePath);

                    content.ContentImages.Add(new ContentImage
                    {
                        ImageSource = fileName,
                    });
                    context.SaveChanges();
                }


                return RedirectToAction("Index", "Home", new { id = content.Name, area = "" });
            }
        }

        
    }
}
