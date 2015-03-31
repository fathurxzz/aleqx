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
    public class MainBannerController : AdminController
    {
        private readonly SiteContext _context;

        public MainBannerController(SiteContext context)
        {
            _context = context;
        }


        public ActionResult Index()
        {
            var tags = _context.MainBanners.ToList();
            return View(tags);
        }

        public ActionResult Create()
        {

            return View(new MainBanner());
        }

        //
        // POST: /Admin/ContentItem/Create

        [HttpPost]
        public ActionResult Create(MainBanner model, HttpPostedFileBase file)
        {
            try
            {
                var contentItem = new MainBanner
                {
                    Url = model.Url,
                };

                if (file != null)
                {
                    string fileName = IOHelper.GetUniqueFileName("~/Content/Images", file.FileName);
                    string filePath = Server.MapPath("~/Content/Images");
                    filePath = Path.Combine(filePath, fileName);
                    //file.SaveAs(filePath);
                    GraphicsHelper.SaveOriginalImageWithDefinedDimentions(filePath, fileName, file, 556, 0, ScaleMode.FixedWidth);
                    contentItem.ImageSrc = fileName;
                }

                _context.MainBanners.Add(contentItem);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Admin/ContentItem/Edit/5

        public ActionResult Edit(int id)
        {
            var contentItem = _context.MainBanners.First(p => p.Id == id);
            return View(contentItem);
        }

        //
        // POST: /Admin/ContentItem/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, MainBanner model, HttpPostedFileBase file)
        {
            try
            {
                var contentItem = _context.MainBanners.First(p => p.Id == id);

                contentItem.Url = model.Url;

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
                    GraphicsHelper.SaveOriginalImageWithDefinedDimentions(filePath, fileName, file, 556, 0, ScaleMode.FixedWidth);
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


        public ActionResult Delete(int id)
        {
            var contentItem = _context.MainBanners.First(p => p.Id == id);
            if (!string.IsNullOrEmpty(contentItem.ImageSrc))
            {
                ImageHelper.DeleteImage(contentItem.ImageSrc);
            }
            _context.MainBanners.Remove(contentItem);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
