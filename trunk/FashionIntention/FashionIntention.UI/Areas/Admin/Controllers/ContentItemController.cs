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
    public class ContentItemController : AdminController
    {
        //
        // GET: /Admin/ContentItem/

        private readonly SiteContext _context;

        public ContentItemController(SiteContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            var contentItems = _context.ContentItems.ToList();
            return View(contentItems);
        }

        //
        // GET: /Admin/ContentItem/Create

        public ActionResult Create()
        {

            return View(new ContentItem());
        }

        //
        // POST: /Admin/ContentItem/Create

        [HttpPost]
        public ActionResult Create(ContentItem model, HttpPostedFileBase file)
        {
            try
            {
                var contentItem = new ContentItem
                {
                    Text = model.Text == null ? "" : HttpUtility.HtmlDecode(model.Text),
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

                _context.ContentItems.Add(contentItem);
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
            var contentItem = _context.ContentItems.First(p => p.Id == id);
            return View(contentItem);
        }

        //
        // POST: /Admin/ContentItem/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, ContentItem model, HttpPostedFileBase file)
        {
            try
            {
                var contentItem = _context.PostItems.First(p => p.Id == id);

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
            var contentItem = _context.ContentItems.First(p => p.Id == id);
            if (!string.IsNullOrEmpty(contentItem.ImageSrc))
            {
                ImageHelper.DeleteImage(contentItem.ImageSrc);
            }
            _context.ContentItems.Remove(contentItem);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
