using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewVision.UI.Helpers;
using NewVision.UI.Models;

namespace NewVision.UI.Areas.Admin.Controllers
{
    public class EventController : Controller
    {
        private readonly SiteContext _context;
        //
        // GET: /Admin/Event/

        public EventController(SiteContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            var events = _context.Events.ToList();
            return View(events);
        }

        //
        // GET: /Admin/Event/Create

        public ActionResult Create()
        {
            return View(new Event());
        }

        //
        // POST: /Admin/Event/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Admin/Event/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Admin/Event/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Admin/Event/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Admin/Event/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult DeletePreviewContentImage(int id)
        {
            var eventPreviewContentImage = _context.PreviewContentImages.First(ea => ea.Id == id);
            ImageHelper.DeleteImage(eventPreviewContentImage.ImageSrc);
            _context.PreviewContentImages.Remove(eventPreviewContentImage);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteContentImage(int id)
        {
            var eventContentImage = _context.ContentImages.First(ea => ea.Id == id);
            ImageHelper.DeleteImage(eventContentImage.ImageSrc);
            _context.ContentImages.Remove(eventContentImage);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
