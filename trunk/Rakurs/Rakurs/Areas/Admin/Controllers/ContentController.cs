using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rakurs.Models;

namespace Rakurs.Areas.Admin.Controllers
{
    [Authorize]
    public class ContentController : Controller
    {

        public ActionResult Add()
        {
            return View(new Content { SortOrder = 0 });
        }

        [HttpPost]
        public ActionResult Add(FormCollection form)
        {
            using (var context = new StructureContainer())
            {
                var content = new Content { MainPage = false, IsGallery = false };

                TryUpdateModel(content, new[]
                                            {
                                                "Name",
                                                "Title",
                                                "TitleEng",
                                                "PageTitle",
                                                "PageTitleEng",
                                                "SortOrder",
                                                "SeoDescription",
                                                "SeoKeywords"
                                            });
                content.Text = HttpUtility.HtmlDecode(form["Text"]);
                content.TextEng = HttpUtility.HtmlDecode(form["TextEng"]);

                context.AddToContent(content);

                context.SaveChanges();

                return RedirectToAction("Index", "Home", new { id = content.Name, area = "" });
            }
        }



        public ActionResult Edit(int id)
        {
            using (var context = new StructureContainer())
            {
                var content = context.Content.First(c => c.Id == id);
                return View(content);
            }
        }

        [HttpPost]
        public ActionResult Edit(Content model, FormCollection form)
        {
            using (var context = new StructureContainer())
            {
                var content = context.Content.First(c => c.Id == model.Id);

                TryUpdateModel(content, new[]
                                            {
                                               "Name",
                                                "Title",
                                                "TitleEng",
                                                "PageTitle",
                                                "PageTitleEng",
                                                "SortOrder",
                                                "SeoDescription",
                                                "SeoKeywords"
                                            });
                content.Text = HttpUtility.HtmlDecode(form["Text"]);
                content.TextEng = HttpUtility.HtmlDecode(form["TextEng"]);

                context.SaveChanges();

                return RedirectToAction("Index", "Home", new { id = content.Name, area = "" });
            }
        }


        public ActionResult Delete(int id)
        {
            using (var context = new StructureContainer())
            {
                var content = context.Content.First(c => c.Id == id);
                context.DeleteObject(content);
                context.SaveChanges();
                return RedirectToAction("Index", "Home", new {area = "", id = ""});
            }
        }
    }
}
