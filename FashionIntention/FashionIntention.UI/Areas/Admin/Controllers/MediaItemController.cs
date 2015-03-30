using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FashionIntention.UI.Helpers;
using FashionIntention.UI.Models;

namespace FashionIntention.UI.Areas.Admin.Controllers
{
    public class MediaItemController : AdminController
    {
        private readonly SiteContext _context;

        public MediaItemController(SiteContext context)
        {
            _context = context;
        }



        public ActionResult Index()
        {
            var mediaItems = _context.MediaItems.ToList();
            return View(mediaItems);
        }

     
        //
        // GET: /Admin/MediaItem/Create

        public ActionResult Create()
        {
            return View(new MediaItem());
        }

        //
        // POST: /Admin/MediaItem/Create

        [HttpPost]
        public ActionResult Create(MediaItem model, HttpPostedFileBase file)
        {
            try
            {
                var mediaItem = new MediaItem
                {
                    Text = model.Text == null ? "" : HttpUtility.HtmlDecode(model.Text),
                    VideoSrc = model.VideoSrc
                };

                if (file != null)
                {
                    string fileName = IOHelper.GetUniqueFileName("~/Content/Images", file.FileName);
                    string filePath = Server.MapPath("~/Content/Images");
                    filePath = Path.Combine(filePath, fileName);
                    //file.SaveAs(filePath);
                    GraphicsHelper.SaveOriginalImageWithDefinedDimentions(filePath, fileName, file, 556, 0, ScaleMode.FixedWidth);
                    mediaItem.ImageSrc = fileName;
                }

                _context.MediaItems.Add(mediaItem);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Admin/MediaItem/Edit/5

        public ActionResult Edit(int id)
        {
            var contentItem = _context.MediaItems.First(p => p.Id == id);
            return View(contentItem);
        }

        //
        // POST: /Admin/MediaItem/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, MediaItem model, HttpPostedFileBase file)
        {
            try
            {
                var contentItem = _context.MediaItems.First(p => p.Id == id);
                contentItem.VideoSrc = model.VideoSrc;


                contentItem.Text = model.Text == null ? "" : HttpUtility.HtmlDecode(model.Text);

                if (file != null)
                {
                    if (!string.IsNullOrEmpty(contentItem.ImageSrc))
                    {
                        ImageHelper.DeleteImage(contentItem.ImageSrc);
                    }

                    string fileName = IOHelper.GetUniqueFileName("~/Content/Images", file.FileName);
                    string filePath = Server.MapPath("~/Content/Images");
                    filePath = Path.Combine(filePath, fileName);
                    //file.SaveAs(filePath);
                    GraphicsHelper.SaveOriginalImageWithDefinedDimentions(filePath, fileName, file, 556, 0,
                        ScaleMode.FixedWidth);
                    contentItem.ImageSrc = fileName;

                }

                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Admin/MediaItem/Delete/5

        public ActionResult Delete(int id)
        {
            var contentItem = _context.MediaItems.First(p => p.Id == id);
            if (!string.IsNullOrEmpty(contentItem.ImageSrc))
            {
                ImageHelper.DeleteImage(contentItem.ImageSrc);
            }
            _context.MediaItems.Remove(contentItem);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
