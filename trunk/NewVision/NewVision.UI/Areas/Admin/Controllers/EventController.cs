using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewVision.UI.Helpers;
using NewVision.UI.Models;
using NewVision.UI.Models.SiteViewModels;

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
            return View(new Event { Date = DateTime.Now });
        }

        //
        // POST: /Admin/Event/Create

        [HttpPost]
        public ActionResult Create(Event model, IEnumerable<HttpPostedFileBase> files, IEnumerable<HttpPostedFileBase> filesAnother)
        {
            try
            {
                var ev = new Event
                {
                    Title = model.Title,
                    TitleDescription = model.TitleDescription,
                    Date = model.Date,
                    HighlightedText = model.HighlightedText,
                    LocationAddress  = model.LocationAddress,
                    LocationTitle = model.LocationTitle,
                    TicketOrderType = model.TicketOrderType,
                    PreviewContentType = model.PreviewContentType,
                    ArtGroup = model.ArtGroup,
                    Action = model.Action,
                    Location = model.Location,
                    Duration = model.Duration,
                    Description = model.Description,
                    IntervalQuantity = model.IntervalQuantity,
                    PreviewContentVideoSrc = model.PreviewContentVideoSrc,
                    Price = model.Price
                };

                ev.IsHighlighted = !string.IsNullOrEmpty(ev.HighlightedText);

                foreach (var file in files)
                {
                    string fileName = IOHelper.GetUniqueFileName("~/Content/Images", file.FileName);
                    string filePath = Server.MapPath("~/Content/Images");

                    filePath = Path.Combine(filePath, fileName);
                    GraphicsHelper.SaveOriginalImage(filePath, fileName, file, 1500);

                    var ci = new ContentImage
                    {
                        ImageSrc = fileName
                    };

                    ev.ContentImages.Add(ci);
                }

                foreach (var file in filesAnother)
                {
                    string fileName = IOHelper.GetUniqueFileName("~/Content/Images", file.FileName);
                    string filePath = Server.MapPath("~/Content/Images");

                    filePath = Path.Combine(filePath, fileName);
                    GraphicsHelper.SaveOriginalImage(filePath, fileName, file, 1500);

                    var ci = new PreviewContentImage
                    {
                        ImageSrc = fileName
                    };

                    ev.PreviewContentImages.Add(ci);
                }


                _context.Events.Add(ev);
                _context.SaveChanges();
                
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
            var ev = _context.Events.First(e => e.Id == id);
            return View(ev);
        }

        //
        // POST: /Admin/Event/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, IEnumerable<HttpPostedFileBase> files, IEnumerable<HttpPostedFileBase> filesAnother)
        {
            try
            {
                var ev = _context.Events.First(e => e.Id == id);
                TryUpdateModel(ev, new[]
                {
                    "Title",
                    "TitleDescription",
                    "Date",
                    "HighlightedText",
                    "LocationAddress",
                    "HighlightedText",
                    "LocationAddress",
                    "LocationTitle",
                    "TicketOrderType",
                    "PreviewContentType",
                    "ArtGroup",
                    "Action",
                    "Location",
                    "Duration",
                    "Description",
                    "IntervalQuantity",
                    "PreviewContentVideoSrc",
                    "Price"
                });

                ev.IsHighlighted = !string.IsNullOrEmpty(ev.HighlightedText);


                foreach (var file in files)
                {
                    if (file == null) continue;
                    string fileName = IOHelper.GetUniqueFileName("~/Content/Images", file.FileName);
                    string filePath = Server.MapPath("~/Content/Images");

                    filePath = Path.Combine(filePath, fileName);
                    GraphicsHelper.SaveOriginalImage(filePath, fileName, file, 1500);

                    var ci = new ContentImage
                    {
                        ImageSrc = fileName
                    };

                    ev.ContentImages.Add(ci);
                }

                foreach (var file in filesAnother)
                {
                    if (file == null) continue;
                    string fileName = IOHelper.GetUniqueFileName("~/Content/Images", file.FileName);
                    string filePath = Server.MapPath("~/Content/Images");

                    filePath = Path.Combine(filePath, fileName);
                    GraphicsHelper.SaveOriginalImage(filePath, fileName, file, 1500);

                    var ci = new PreviewContentImage
                    {
                        ImageSrc = fileName
                    };

                    ev.PreviewContentImages.Add(ci);
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
        // GET: /Admin/Event/Delete/5

        public ActionResult Delete(int id)
        {
            var ev = _context.Events.First(e => e.Id == id);

            foreach (var image in ev.ContentImages)
            {
                ImageHelper.DeleteImage(image.ImageSrc);
            }

            foreach (var image in ev.PreviewContentImages)
            {
                ImageHelper.DeleteImage(image.ImageSrc);
            }

            _context.Events.Remove(ev);
            _context.SaveChanges();
            return RedirectToAction("Index");
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
