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
            return RedirectToAction("Index", "Home", new { Area = "" });
        }
    }
}
