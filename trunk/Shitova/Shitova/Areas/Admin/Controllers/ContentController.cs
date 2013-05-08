using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shitova.Helpers;
using Shitova.Models;
using SiteExtensions;
using SiteExtensions.Graphics;

namespace Shitova.Areas.Admin.Controllers
{
    public class ContentController : Controller
    {
        public ActionResult Edit(int id)
        {
            using (var context = new SiteContainer())
            {
                var content = context.Content.First(c => c.Id == id);
                return View(content);
            }
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection form)
        {
            using (var context = new SiteContainer())
            {
                var content = context.Content.First(c => c.Id == id);
                TryUpdateModel(content, new[] { "Title", "SortOrder", "SeoDescription", "SeoKeywords" });
                content.Text = HttpUtility.HtmlDecode(form["Text"]);
                context.SaveChanges();
                return RedirectToAction("Index", "Home", new { area = "", id = content.Name });
            }
        }


        public ActionResult AddTextBlock()
        {
            using (var context = new SiteContainer())
            {
                int maxSortOrder = context.ContentItem.Max(c => (int?) c.SortOrder) ?? 0;
                return View(new ContentItem { SortOrder = maxSortOrder + 1 });
            }
        }

        [HttpPost]
        public ActionResult AddTextBlock(FormCollection form)
        {
            using (var context = new SiteContainer())
            {
                var contentItem = new ContentItem
                                      {
                                          ContentType = 1,
                                          Text = HttpUtility.HtmlDecode(form["Text"])
                                      };
                TryUpdateModel(contentItem, new[] { "SortOrder" });
                context.AddToContentItem(contentItem);
                context.SaveChanges();
                return RedirectToAction("Index", "Home", new { area = "", id = "look" });
            }
        }

        public ActionResult EditTextBlock(int id)
        {
            using (var context = new SiteContainer())
            {
                var contentItem = context.ContentItem.First(c => c.Id == id);
                return View(contentItem);
            }
        }
        [HttpPost]
        public ActionResult EditTextBlock(int id, FormCollection form)
        {
            using (var context = new SiteContainer())
            {
                var contentItem = context.ContentItem.First(c => c.Id == id);
                TryUpdateModel(contentItem, new[] { "SortOrder" });
                contentItem.Text = HttpUtility.HtmlDecode(form["Text"]);
                context.SaveChanges();
                return RedirectToAction("Index", "Home", new { area = "", id = "look" });
            }
        }

        public ActionResult DeleteTextBlock(int id)
        {
            using (var context = new SiteContainer())
            {
                var contentItem = context.ContentItem.First(c => c.Id == id);
                context.DeleteObject(contentItem);
                context.SaveChanges();
                return RedirectToAction("Index", "Home", new { area = "", id = "look" });
            }
        }

        public ActionResult AddImagesBlock()
        {
            using (var context = new SiteContainer())
            {
                int maxSortOrder = context.ContentItem.Max(c => (int?)c.SortOrder) ?? 0;
                return View(new ContentItem { SortOrder = maxSortOrder + 1 });
            }
        }

        [HttpPost]
        public ActionResult AddImagesBlock(FormCollection form)
        {
            using (var context = new SiteContainer())
            {
                var contentItem = new ContentItem
                {
                    ContentType = 2
                };

                TryUpdateModel(contentItem, new[] { "SortOrder" });

                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var file = Request.Files[i];

                    if (file == null) continue;
                    if (string.IsNullOrEmpty(file.FileName)) continue;

                    var ci = new ContentItemImage();
                    string fileName = IOHelper.GetUniqueFileName("~/Content/Images", file.FileName);
                    string filePath = Server.MapPath("~/Content/Images");

                    filePath = Path.Combine(filePath, fileName);
                    GraphicsHelper.SaveOriginalImage(filePath, fileName, file, 1500);

                    ci.ImageSource = fileName;
                    contentItem.ContentItemImages.Add(ci);
                }

                context.AddToContentItem(contentItem);
                context.SaveChanges();

                return RedirectToAction("Index", "Home", new { area = "", id = "look" });
            }
        }

        public ActionResult EditImagesBlock(int id)
        {
            using (var context = new SiteContainer())
            {
                var contentItem = context.ContentItem.First(c => c.Id == id);
                return View(contentItem);
            }
        }
        [HttpPost]
        public ActionResult EditImagesBlock(int id, FormCollection form)
        {
            using (var context = new SiteContainer())
            {
                var contentItem = context.ContentItem.First(c => c.Id == id);
                TryUpdateModel(contentItem, new[] { "SortOrder" });
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var file = Request.Files[i];

                    if (file == null) continue;
                    if (string.IsNullOrEmpty(file.FileName)) continue;

                    var ci = new ContentItemImage();
                    string fileName = IOHelper.GetUniqueFileName("~/Content/Images", file.FileName);
                    string filePath = Server.MapPath("~/Content/Images");

                    filePath = Path.Combine(filePath, fileName);
                    GraphicsHelper.SaveOriginalImage(filePath, fileName, file, 1500);

                    ci.ImageSource = fileName;
                    contentItem.ContentItemImages.Add(ci);
                }

                context.SaveChanges();

                return RedirectToAction("Index", "Home", new { area = "", id = "look" });
            }
        }

        public ActionResult DeleteImagesBlock(int id)
        {
            using (var context = new SiteContainer())
            {
                var contentItem = context.ContentItem.Include("ContentItemImages").First(c => c.Id == id);

                while (contentItem.ContentItemImages.Any())
                {
                    var ci = contentItem.ContentItemImages.First();
                    ImageHelper.DeleteImage(ci.ImageSource);
                    context.DeleteObject(ci);
                }

                context.DeleteObject(contentItem);

                context.SaveChanges();



                return RedirectToAction("Index", "Home", new { area = "", id = "look" });
            }
        }

    }
}
