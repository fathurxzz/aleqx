using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using ViaCon.Models;
using System.Web.Script.Serialization;
using ViaCon.Helpers;
using System.IO;
using System.Data;

namespace ViaCon.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        //
        // GET: /Admin/


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Content()
        {
            return View();
        }

        public ActionResult Gallery()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AddImageToGallery(int id,string title, string material, string location)
        {
            string file = Request.Files["image"].FileName;
            if (!string.IsNullOrEmpty(file))
            {
                string newFileName = IOHelper.GetUniqueFileName("~/Content/GalleryImages", file);
                string filePath = Path.Combine(Server.MapPath("~/Content/GalleryImages"), newFileName);
                Request.Files["image"].SaveAs(filePath);
                using (var context = new ContentStorage())
                {
                    //var content = context.Content.Where(c => c.Id == id).Select(c => c).FirstOrDefault();
                    //if (content == null) 
                        //return RedirectToAction("ManageGalleryPictures", new { id = id });

                    var galleryItem = new Gallery();
                    //galleryItem.Content = content;
                    galleryItem.ContentReference.EntityKey = new EntityKey("ContentStorage.Content", "Id", id);
                    galleryItem.ImageSource = newFileName;
                    galleryItem.Title = title;
                    galleryItem.Material = material;
                    galleryItem.Location = location;
                    context.AddToGallery(galleryItem);
                    context.SaveChanges();
                }
            }
            return RedirectToAction("ManageGalleryPictures", new { id = id });
        }

        public ActionResult DeleteImage(int contentId, int id)
        {
            using (var context = new ContentStorage())
            {
                var galleryItem = context.Gallery.Where(c => c.Id == id).Select(c => c).First();
                context.DeleteObject(galleryItem);
                context.SaveChanges();
            }
            return RedirectToAction("ManageGalleryPictures", new { id = contentId });
        }

        public ActionResult ManageGalleryPictures(int id)
        {
            ViewData["id"] = id;
            using (var context = new ContentStorage())
            {
                var pictures = context.Gallery.Where(c=>c.Content.Id==id).Select(c => c).ToList();
                return View(pictures);
            }
        }

        public ActionResult EditContentItem(int id, int? parentId, bool? horisontal, bool? collapsible)
        {
            ViewData["parentId"] = parentId;
            ViewData["horisontal"] = horisontal;
            ViewData["collapsible"] = collapsible;
            using (var context = new ContentStorage())
            {
                var contentItem = context.Content.Where(c => c.Id == id).Select(c => c).FirstOrDefault();
                return View(contentItem);
            }
        }

        public ActionResult EditGalleryItem(int id, int? parentId)
        {
            ViewData["parentId"] = parentId;

            using (var context = new ContentStorage())
            {
                var contentItem = context.Content.Where(c => c.Id == id).Select(c => c).FirstOrDefault();
                return View(contentItem);
            }
        }



        public ActionResult AddContentItem(int? parentId, bool? horisontal, bool? collapsible)
        {
            ViewData["parentId"] = parentId;
            ViewData["horisontal"] = horisontal;
            ViewData["collapsible"] = collapsible;
            return View();
        }

        public ActionResult AddGalleryItem(int? parentId)
        {
            ViewData["parentId"] = parentId;
            return View();
        }





        public ActionResult ContentList(int? id, int level)
        {
            using (var context = new ContentStorage())
            {
                List<Content> contentList = context.Content.Where(c=>!c.IsGalleryItem).Select(c => c).ToList();
                if (id == null)
                    contentList = contentList.Select(c => c).Where(c => c.Parent == null).ToList();
                else
                    contentList = contentList.Select(c => c).Where(c => c.Parent != null && c.Parent.Id == id.Value).ToList();
                ViewData["level"] = level;
                if (id != null)
                    ViewData["id"] = id.Value;
                return View(contentList);
            }
        }

        public ActionResult GalleryList(int? id, int level)
        {
            using (var context = new ContentStorage())
            {
                List<Content> galleryList = context.Content.Where(c => c.IsGalleryItem).Select(c => c).ToList();
                if (id == null)
                    galleryList = galleryList.Select(c => c).Where(c => c.Parent == null).ToList();
                else
                    galleryList = galleryList.Select(c => c).Where(c => c.Parent != null && c.Parent.Id == id.Value).ToList();
                ViewData["level"] = level;
                if (id != null)
                    ViewData["id"] = id.Value;
                return View(galleryList);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UpdateContent(int id, bool isGalleryItem, int? parentId, string contentId, string title, string description, string keywords, string text, bool collapsible, bool? horisontal)
        {
            using (var context = new ContentStorage())
            {

                Content parent = null;
                if(parentId!=null)
                    parent = context.Content.Select(c => c).Where(c => c.Id == parentId).First();
                Content content;
                if (id != int.MinValue)
                    content = context.Content.Select(c => c).Where(c => c.Id == id).First();
                else
                    content = new Content();
                content.Parent = parent;
                content.ContentId = contentId;
                content.Title = title;
                content.Description = description;
                content.Keywords = keywords;
                content.Text = text;
                content.IsGalleryItem = isGalleryItem;
                content.Collapsible=collapsible;
                if (horisontal.HasValue)
                    content.Horisontal = horisontal.Value;
                if (content.Id == 0)
                    context.AddToContent(content);
                context.SaveChanges();
            }
            string returnUrl = isGalleryItem ? "Gallery" : "Content";
            return RedirectToAction(returnUrl);
        }

        /*
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult InsertContentItem(FormCollection form)
        {
            using (var context = new ContentStorage())
            {
                int parentId = int.Parse(form["parentId"]);
                Content parent = null;
                if (parentId >= 0)
                    parent = context.Content.Select(c => c).Where(c => c.Id == parentId).First();
                Content content = new Content();
                content.Parent = parent;
                content.ContentId = form["contentId"];
                content.Title = form["title"];
                context.AddToContent(content);
                context.SaveChanges();
            }
            return RedirectToAction("Content");
        }
        */


        public ActionResult DeleteContentItem(int id)
        {
            using (ContentStorage context = new ContentStorage())
            {
                Content content = context.Content.Include("Children").Where(c => c.Id == id).FirstOrDefault();
                if (content.Children.Count == 0)
                {
                    context.DeleteObject(content);
                    context.SaveChanges();
                }
            }
            return RedirectToAction("Content");
        }

        public ActionResult DeleteGalleryItem(int id)
        {
            /*
            using (ContentStorage context = new ContentStorage())
            {
                Content content = context.Content.Include("Children").Where(c => c.Id == id).FirstOrDefault();
                if (content.Children.Count == 0)
                {
                    context.DeleteObject(content);
                    context.SaveChanges();
                }
            }*/
            return RedirectToAction("Gallery");
        }
    }
}
