using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json.Schema;
using NewVision.UI.Helpers;
using NewVision.UI.Models;

namespace NewVision.UI.Areas.Admin.Controllers
{
    public class EventAnnouncementController : AdminController
    {
        private readonly SiteContext _context;
        //
        // GET: /Admin/Announcement/

        public EventAnnouncementController(SiteContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            var announcements = _context.EventAnnouncements.ToList();
            return View(announcements);
        }

        //
        // GET: /Admin/Announcement/Create

        public ActionResult Create()
        {
            return View(new EventAnnouncement());
        }

        //
        // POST: /Admin/Announcement/Create

        [HttpPost]
        public ActionResult Create(EventAnnouncement model)
        {
            try
            {
                var eventAnnouncement = new EventAnnouncement {Title = model.Title ?? ""};
                eventAnnouncement.Text = model.Text == null ? "" : HttpUtility.HtmlDecode(model.Text);

                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var file = Request.Files[i];
                    if (file != null && !string.IsNullOrEmpty(file.FileName))
                    {
                        string fileName = IOHelper.GetUniqueFileName("~/Content/Images", file.FileName);
                        string filePath = Server.MapPath("~/Content/Images");

                        filePath = Path.Combine(filePath, fileName);
                        GraphicsHelper.SaveOriginalImage(filePath, fileName, file, 1500);

                        var eai = new EventAnnouncementImage()
                        {
                            ImageSrc = fileName
                        };

                        eventAnnouncement.EventAnnouncementImages.Add(eai);
                    }
                }

                _context.EventAnnouncements.Add(eventAnnouncement);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                return View();
            }
        }

        //
        // GET: /Admin/Announcement/Edit/5

        public ActionResult Edit(int id)
        {
            var eventAnnouncement = _context.EventAnnouncements.First(ea => ea.Id == id);
            return View(eventAnnouncement);
        }

        //
        // POST: /Admin/Announcement/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, EventAnnouncement model)
        {
            try
            {
                var eventAnnouncement = _context.EventAnnouncements.First(ea => ea.Id == id);

                eventAnnouncement.Title = model.Title ?? "";
                eventAnnouncement.Text = model.Text == null ? "" : HttpUtility.HtmlDecode(model.Text);


                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var file = Request.Files[i];

                    if (file != null && !string.IsNullOrEmpty(file.FileName))
                    {
                        string fileName = IOHelper.GetUniqueFileName("~/Content/Images", file.FileName);
                        string filePath = Server.MapPath("~/Content/Images");

                        filePath = Path.Combine(filePath, fileName);
                        GraphicsHelper.SaveOriginalImage(filePath, fileName, file, 1500);

                        var eai = new EventAnnouncementImage
                        {
                            ImageSrc = fileName
                        };

                        eventAnnouncement.EventAnnouncementImages.Add(eai);
                    }
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
        // GET: /Admin/Announcement/Delete/5

        public ActionResult Delete(int id)
        {
            var eventAnnouncement = _context.EventAnnouncements.First(ea => ea.Id == id);

            foreach (var image in eventAnnouncement.EventAnnouncementImages)
            {
                ImageHelper.DeleteImage(image.ImageSrc);
            }

            _context.EventAnnouncements.Remove(eventAnnouncement);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }



        public ActionResult DeleteImage(int id)
        {
            var eventAnnouncementImage = _context.EventAnnouncementImages.First(ea => ea.Id == id);
            ImageHelper.DeleteImage(eventAnnouncementImage.ImageSrc);
            _context.EventAnnouncementImages.Remove(eventAnnouncementImage);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
