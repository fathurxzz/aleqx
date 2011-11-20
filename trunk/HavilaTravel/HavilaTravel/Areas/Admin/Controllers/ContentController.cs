using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HavilaTravel.Helpers;
using HavilaTravel.Models;

namespace HavilaTravel.Areas.Admin.Controllers
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
                                                "ContentModel",
                                                "Title",
                                                "MenuTitle",
                                                "PageTitle",
                                                "SortOrder",
                                                "SeoDescription",
                                                "SeoKeywords"
                                            });
                content.Text = HttpUtility.HtmlDecode(form["Text"]);
                context.SaveChanges();

                if (content.PlaceKind > 0)
                    return RedirectToAction("Index", "Place", new { id = content.Name, area = "" });

                return RedirectToAction("Index", "Home", new { id = content.Name, area = "" });
            }
        }

        public ActionResult AddSubmenu(int id, int? placeKind)
        {
            using (var context = new ContentStorage())
            {
                var content = context.Content.Where(c => c.Id == id).First();
                ViewBag.parentId = id;
                return View(new Content
                                {
                                    ContentLevel = content.ContentLevel + 1,
                                    ContentType = content.ContentType,
                                    PlaceKind = placeKind.HasValue ? placeKind.Value : 0
                                });
            }
        }

        [HttpPost]
        public ActionResult AddSubmenu(int parentId, FormCollection form)
        {
            using (var context = new ContentStorage())
            {
                var parentContent = context.Content.Where(c => c.Id == parentId).First();
                var content = new Content();
                TryUpdateModel(content, new[]
                                            {
                                                "Name",
                                                "ContentModel",
                                                "Title",
                                                "MenuTitle",
                                                "PageTitle",
                                                "SortOrder",
                                                "SeoDescription",
                                                "SeoKeywords",
                                                //"ContentType",
                                                "ContentLevel",
                                                "PlaceKind"
                                            });
                content.Text = HttpUtility.HtmlDecode(form["Text"]);
                content.Parent = parentContent;
                context.AddToContent(content);
                context.SaveChanges();

                if (content.PlaceKind > 0)
                    return RedirectToAction("Index", "Place", new { id = content.Name, area = "" });

                return RedirectToAction("Index", "Home", new { id = content.Name, area = "" });
            }
        }

        public ActionResult Add()
        {
            return View(new Content { ContentType = 10, ContentLevel = 1 });
        }

        [HttpPost]
        public ActionResult Add(FormCollection form)
        {
            using (var context = new ContentStorage())
            {
                var content = new Content();
                TryUpdateModel(content, new[]
                                            {
                                                "Name",
                                                "Title",
                                                "ContentModel",
                                                "MenuTitle",
                                                "PageTitle",
                                                "SortOrder",
                                                "SeoDescription",
                                                "SeoKeywords",
                                                "ContentType",
                                                "ContentLevel"
                                            });
                content.Text = HttpUtility.HtmlDecode(form["Text"]);
                context.AddToContent(content);
                context.SaveChanges();

                return RedirectToAction("Index", "Home", new { id = content.Name, area = "" });
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
                    child.Accordions.Load();
                    while (child.Accordions.Any())
                    {
                        var accordion = child.Accordions.First();
                        accordion.AccordionImages.Load();
                        while (accordion.AccordionImages.Any())
                        {
                            var image = accordion.AccordionImages.First();
                            IOHelper.DeleteFile("~/Content/Photos", image.ImageSource);
                            context.DeleteObject(image);
                        }
                        context.DeleteObject(accordion);
                    }
                    context.DeleteObject(child);
                }
                context.DeleteObject(content);
                context.SaveChanges();
            }
            return RedirectToAction("Index", "Home", new { area = "" });
        }


        public ActionResult AddRegion()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddRegion(FormCollection form)
        {
            using (var context = new ContentStorage())
            {
                var content = new Content { PlaceKind = (int)PlaceKind.Region, ContentLevel = 1 };
                TryUpdateModel(content,
                               new[]{
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


        /*
        public ActionResult EditRegion(int id)
        {
            using (var context = new ContentStorage())
            {
                var content = context.Content.Where(c => c.Id == id).First();
                return View(content);
            }
        }

        public ActionResult EditRegion(int id,FormCollection form)
        {

        }
        */

        //public ActionResult AddSpa()
        //{


        //    return View("AddSubmenu", new Content { PlaceKind = 5 });
        //}

        //public ActionResult EditSpa(int id)
        //{
        //    return View("Edit", new Content { PlaceKind = 5 });
        //}

    }
}
