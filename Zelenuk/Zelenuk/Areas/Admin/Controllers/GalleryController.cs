using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zelenuk.Helpers;
using Zelenuk.Models;

namespace Zelenuk.Areas.Admin.Controllers
{
    public class GalleryController : Controller
    {
        public ActionResult Add()
        {
            return View(new Gallery());
        }

        [HttpPost]
        public ActionResult Add(FormCollection form, HttpPostedFileBase fileUpload)
        {
            using (var context = new ContentStorage())
            {
                var gallery = new Gallery();
                TryUpdateModel(gallery, new[]
                                            {
                                                "Name",
                                                "Title",
                                                "PageTitle",
                                                "SortOrder"
                                            });

                string fileName = IOHelper.GetUniqueFileName("~/Content/Photos", fileUpload.FileName);
                string filePath = Server.MapPath("~/Content/Photos");
                filePath = Path.Combine(filePath, fileName);
                fileUpload.SaveAs(filePath);

                gallery.ImageSource = fileName;

                context.AddToGallery(gallery);
                context.SaveChanges();

                return RedirectToAction("Index", "Gallery", new { Area = ""});
            }
        }


        public ActionResult AddImageToGallery(int galleryId)
        {
            ViewBag.galleryId = galleryId;
            return View();
        }

        [HttpPost]
        public ActionResult AddImageToGallery(int galleryId, FormCollection form, HttpPostedFileBase fileUpload)
        {
            using (var context = new ContentStorage())
            {
                var galleryItem = new Galleryitem();
                TryUpdateModel(galleryItem, new[] { "SortOrder" });
                string fileName = IOHelper.GetUniqueFileName("~/Content/Photos", fileUpload.FileName);
                string filePath = Server.MapPath("~/Content/Photos");
                filePath = Path.Combine(filePath, fileName);
                fileUpload.SaveAs(filePath);

                galleryItem.ImageSource = fileName;

                var gallery = context.Gallery.Where(g => g.Id == galleryId).First();

                gallery.GalleryItems.Add(galleryItem);

                context.SaveChanges();


                return RedirectToAction("Details", "Gallery", new {Area = "", id = gallery.Name});
            }
        }

        public ActionResult DeleteGallery(int id)
        {
            using (var context = new ContentStorage())
            {
                var gallery = context.Gallery.Include("GalleryItems").Where(g=>g.Id==id).First();

                while (gallery.GalleryItems.Any())
                {
                    var galleryItem = gallery.GalleryItems.First();
                    IOHelper.DeleteFile("~/Content/Photos", galleryItem.ImageSource);
                    IOHelper.DeleteFile("~/ImageCache/thumbnail", galleryItem.ImageSource);
                    context.DeleteObject(galleryItem);
                }

                IOHelper.DeleteFile("~/Content/Photos", gallery.ImageSource);
                IOHelper.DeleteFile("~/ImageCache/thumbnail", gallery.ImageSource);
                context.DeleteObject(gallery);

                context.SaveChanges();

            }
            return RedirectToAction("Index", "Gallery", new { Area = ""});
        }

        public ActionResult DeleteImage(int galleryId, int id)
        {
            using (var context = new ContentStorage())
            {
                var galleryItem = context.Galleryitem.Where(gi => gi.Id == id).First();
                var gallery = context.Gallery.Where(g => g.Id == galleryId).First();

                IOHelper.DeleteFile("~/Content/Photos", galleryItem.ImageSource);
                IOHelper.DeleteFile("~/ImageCache/thumbnail", galleryItem.ImageSource);
                context.DeleteObject(galleryItem);
                context.SaveChanges();

                return RedirectToAction("Details", "Gallery", new {Area = "", id = gallery.Name});
            }
        }
    }
}
