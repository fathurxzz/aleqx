using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
            }

            return RedirectToAction("Index", "Home", new { Area = "", id = "" });
        }
    }
}
