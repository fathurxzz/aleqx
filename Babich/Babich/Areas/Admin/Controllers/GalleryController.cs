﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Babich.Models;
using Dev.Mvc.Helpers;

namespace Babich.Areas.Admin.Controllers
{
    public class GalleryController : Controller
    {
        //
        // GET: /Admin/Gallery/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add(int id)
        {
            ViewData["id"] = id;
            using (var context = new ContentStorage())
            {
                var content = context.Content.Include("Galleries").Where(c => c.Id == id).First();
                int sortOrder = content.Galleries.Count > 0 ? content.Galleries.Max(c => c.SortOrder) : -1;
                ViewData["SortOrder"] = (sortOrder + 1).ToString();
                return View(new Gallery {SortOrder = sortOrder + 1});
            }
        }

        [HttpPost]
        public ActionResult Add(int id, FormCollection form)
        {
            ViewData["id"] = id;

            using (var context = new ContentStorage())
            {
                var content = context.Content.Where(c => c.Id == id).First();

                var gallery = new Gallery
                {
                    Content = content,
                };

                TryUpdateModel(gallery, new string[] { "Description", "DescriptionEng", "SortOrder" });

                context.AddToGallery(gallery);
                context.SaveChanges();

                return RedirectToAction("Index", "Home", new { area = "", id = content.Name });
            }

        }

        public ActionResult Edit(int id)
        {
            using (var context = new ContentStorage())
            {

                var gallery = context.Gallery.Include("Content").Where(g => g.Id == id).First();
                ViewData["contentName"] = gallery.Content.Name;
                return View(gallery);
            }
        }


        [HttpPost]
        public ActionResult Edit(int id, FormCollection form)
        {
            using (var context = new ContentStorage())
            {
                var gallery = context.Gallery.Include("Content").Where(g => g.Id == id).First();
                var content = gallery.Content;



                return RedirectToAction("Index", "Home", new { area = "", id = content.Name });
            }
        }

        public ActionResult Delete(int id)
        {
            using (var context = new ContentStorage())
            {

                var gallery = context.Gallery.Include("GalleryItems").Include("Content").Where(g => g.Id == id).First();
                var content = gallery.Content;

                while (gallery.GalleryItems.Any())
                {
                    var photo = gallery.GalleryItems.First();
                    if (!string.IsNullOrEmpty(photo.ImageSource))
                    {
                        IOHelper.DeleteFile("~/Content/Photos", photo.ImageSource);
                        IOHelper.DeleteFile("~/ImageCache/mainView", photo.ImageSource);
                        IOHelper.DeleteFile("~/ImageCache/thumbnail", photo.ImageSource);
                        IOHelper.DeleteFile("~/ImageCache/galleryThumbnail", photo.ImageSource);
                    }
                    context.DeleteObject(gallery.GalleryItems.First());
                }

                context.DeleteObject(gallery);

                context.SaveChanges();

                return RedirectToAction("Index", "Home", new { area = "", id = content.Name });
            }
        }


        public ActionResult AddPhoto(int id)
        {
            ViewData["galleryId"] = id;
            using (var context = new ContentStorage())
            {
                var gallery = context.Gallery.Include("Content").Where(g => g.Id == id).First();
                ViewData["contentName"] = gallery.Content.Name;
            }
            return View();
        }

        [HttpPost]
        public ActionResult AddPhoto(int galleryId, string contentName, FormCollection form)
        {
            using (var context = new ContentStorage())
            {
                var gallery = context.Gallery.Include("GalleryItems").Where(g => g.Id == galleryId).FirstOrDefault();
                
                if (Request.Files["logo"] != null && !string.IsNullOrEmpty(Request.Files["logo"].FileName))
                {
                    string fileName = IOHelper.GetUniqueFileName("~/Content/Photos", Request.Files["logo"].FileName);
                    string filePath = Server.MapPath("~/Content/Photos");
                    filePath = Path.Combine(filePath, fileName);
                    Request.Files["logo"].SaveAs(filePath);

                    GraphicsHelper.SaveCachedImage("~/Content/Photos", fileName, "mainView");
                    GraphicsHelper.SaveCachedImage("~/Content/Photos", fileName, "galleryThumbnail");


                    gallery.GalleryItems.Add(new GalleryItem { ImageSource = fileName });

                    if (gallery.GalleryItems.Count == 1)
                        gallery.ImageSource = fileName;

                    context.SaveChanges();
                }

                return RedirectToAction("Index", "Home", new { area = "", id = contentName, galleryId=galleryId });
            }
        }

        public ActionResult DeletePhoto(int id)
        {
            using (var context = new ContentStorage())
            {
                var photo = context.GalleryItem.Include("Gallery").Where(p => p.Id == id).First();
                long galleryId = photo.Gallery.Id;
                var gallery = context.Gallery.Include("Content").Where(g => g.Id == galleryId).First();
                context.DeleteObject(photo);
                context.SaveChanges();

                if (!string.IsNullOrEmpty(photo.ImageSource))
                {
                    IOHelper.DeleteFile("~/Content/Photos", photo.ImageSource);
                    IOHelper.DeleteFile("~/ImageCache/mainView", photo.ImageSource);
                    IOHelper.DeleteFile("~/ImageCache/thumbnail", photo.ImageSource);
                    IOHelper.DeleteFile("~/ImageCache/galleryThumbnail", photo.ImageSource);
                }

                return RedirectToAction("Index", "Home", new { area = "", id = gallery.Content.Name, galleryId = galleryId });
            }
        }


        public ActionResult SetPreviewPicture(int id)
        {
            using (var context = new ContentStorage())
            {
                var photo = context.GalleryItem.Include("Gallery").Where(p => p.Id == id).First();
                long galleryId = photo.Gallery.Id;
                var gallery = context.Gallery.Include("Content").Where(g => g.Id == galleryId).First();

                gallery.ImageSource = photo.ImageSource;
                context.SaveChanges();

                return RedirectToAction("Index", "Home", new { area = "", id = gallery.Content.Name, galleryId = galleryId });
            }
        }
    }
}
