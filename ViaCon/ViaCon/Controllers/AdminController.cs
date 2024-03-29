using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViaCon.Models;
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
            return RedirectToAction("Index", "Home");
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
        public ActionResult AddImageToGallery(int itemId, string contentId, string materialText, string materialUrl, string location, int sortOrder)
        {
            string file = Request.Files["image"].FileName;
            if (!string.IsNullOrEmpty(file))
            {
                string newFileName = IOHelper.GetUniqueFileName("~/Content/GalleryImages", file);
                string filePath = Path.Combine(Server.MapPath("~/Content/GalleryImages"), newFileName);
                Request.Files["image"].SaveAs(filePath);
                using (var context = new ContentStorage())
                {
                    var galleryItem = new Gallery();
                    galleryItem.ContentReference.EntityKey = new EntityKey("ContentStorage.Content", "Id", itemId);
                    galleryItem.ImageSource = newFileName;
                    galleryItem.MaterialText = materialText;
                    galleryItem.MaterialUrl = materialUrl;
                    galleryItem.Location = location;
                    galleryItem.SortOrder = sortOrder;
                    context.AddToGallery(galleryItem);
                    context.SaveChanges();
                }
            }
            return RedirectToAction("Index", "Content", new { id = contentId });
        }


        public ActionResult EditImageAttributes(int id, string contentId)
        {
            using(var context = new ContentStorage())
            {
                var galleryItem = context.Gallery.Include("Content").Where(c => c.Id == id).Select(c => c).First();
                return View(galleryItem);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UpdateImageAttributes(int id, string contentId,string location,string materialText,string materialUrl,int sortOrder)
        {
            using (var context = new ContentStorage())
            {
                var galleryItem = context.Gallery.Include("Content").Where(c => c.Id == id).Select(c => c).First();
                galleryItem.Location = location;
                galleryItem.MaterialText = materialText;
                galleryItem.MaterialUrl = materialUrl;
                galleryItem.SortOrder = sortOrder;
                context.SaveChanges();
            }
            return RedirectToAction("Index", "Content", new { id = contentId });
        }

        public ActionResult DeleteImage(int id)
        {
            using (var context = new ContentStorage())
            {
                var galleryItem = context.Gallery.Include("Content").Where(c => c.Id == id).Select(c => c).First();
                string contentId = galleryItem.Content.ContentId;
                context.DeleteObject(galleryItem);
                context.SaveChanges();
                return RedirectToAction("Index", "Content", new { id = contentId });
            }
            
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

        public ActionResult EditContentItem(int id, int? parentId, bool? horisontal, bool? collapsible, bool? isGalleryItem)
        {
            ViewData["parentId"] = parentId;
            ViewData["horisontal"] = horisontal;
            ViewData["collapsible"] = collapsible;
            ViewData["isGalleryItem"] = isGalleryItem ?? false;
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
            ViewData["horisontal"] = (horisontal.HasValue) ? horisontal.Value : false;
            ViewData["collapsible"] = (collapsible.HasValue) ? collapsible.Value : false;
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
        public ActionResult UpdateContent(int id, bool isGalleryItem, int? parentId, string contentId, string title, string description, string keywords, string text, bool collapsible, bool? horisontal,int sortOrder)
        {
            using (var context = new ContentStorage())
            {

                Content parent = null;
                int parentHorisontalLevel = 0;
                if (parentId != null)
                {
                    parent = context.Content.Select(c => c).Where(c => c.Id == parentId).First();
                    parentHorisontalLevel = context.Content.Where(c => c.Id == parentId).Select(c => c.HorisontalLevel).First();
                    if (parent.Horisontal)
                        parentHorisontalLevel++;
                }
                Content content = id != int.MinValue ? context.Content.Select(c => c).Where(c => c.Id == id).First() : new Content();
                content.Parent = parent;
                content.ContentId = contentId;
                content.Title = title;
                content.Description = description;
                content.Keywords = keywords;
                content.Text = HttpUtility.HtmlDecode(text);
                content.IsGalleryItem = isGalleryItem;
                content.Collapsible=collapsible;
                content.SortOrder = sortOrder;
                
                content.HorisontalLevel = parentHorisontalLevel;
                if (horisontal.HasValue)
                    content.Horisontal = horisontal.Value;
                if (content.Id == 0)
                    context.AddToContent(content);
                context.SaveChanges();


                if (collapsible && parent != null)
                    contentId = parent.ContentId;

                return RedirectToAction("Index", "Content", new { id = contentId });
            }
        }




        public ActionResult DeleteContentItem(int id)
        {
            using (var context = new ContentStorage())
            {
                Content content = context.Content.Include("Children").Where(c => c.Id == id).FirstOrDefault();
                if (content.Children.Count == 0)
                {
                    context.DeleteObject(content);
                    context.SaveChanges();
                }
                return RedirectToAction("Index", "Content", new { id = "About" });
            }
            
        }

        public ActionResult DeleteGalleryItem(int id)
        {
            return RedirectToAction("Gallery");
        }

        
    }
}
