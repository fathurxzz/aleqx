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
    public class ContentController : Controller
    {
        //
        // GET: /Admin/Content/

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

                return RedirectToAction("Index", "Home", new { area = "", id = "" });
            }
        }

        public ActionResult Edit(int id)
        {
            var content = new Content();

            return View(content);
        }

    }
}
