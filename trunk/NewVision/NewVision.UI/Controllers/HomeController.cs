using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using NewVision.UI.Helpers;
using NewVision.UI.Models;
using NewVision.UI.Models.SiteViewModels;

namespace NewVision.UI.Controllers
{
    public class HomeController : DefaultController
    {
        private readonly SiteContext _context;

        public HomeController(SiteContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            //return RedirectToAction("Index", "MainBanner", new {area = "Admin"});

            //ViewBag.Title = "New Vision Pro";

            var mb = _context.MainBanners.ToList();
            var ea = _context.EventAnnouncements.ToList();


            var mainBanners = new List<mainBanner>();

            foreach (var mainBanner in mb)
            {
                mainBanners.Add(new mainBanner
                {
                    title = mainBanner.Title,
                    description = mainBanner.Description,
                    imageSrc = mainBanner.ImageSrc
                });
            }

            var eventAnnouncements = new List<eventAnnouncement>();

            foreach (var eventAnnouncement in ea)
            {
                var ev = new eventAnnouncement()
                {
                    title = eventAnnouncement.Title,
                    text = eventAnnouncement.Text,
                    images = new List<object>()
                };

                foreach (var image in eventAnnouncement.EventAnnouncementImages)
                {
                    ev.images.Add(image.ImageSrc);
                }

                eventAnnouncements.Add(ev);
            }


            var mainPage = new mainPage {mainBanners = mainBanners, eventAnnouncements = eventAnnouncements};


            ViewBag.MainPage = "dataModels.mainPage = " + JsonConvert.SerializeObject(mainPage);


            return View();
        }


        public ActionResult Events()
        {

            var result = new List<object>();

            var events = _context.Events.ToList();


            foreach (var ev in events.OrderBy(e => e.Date))
            {
                result.Add(new
                {
                    id = ev.Id,
                    date = ev.Date.ToShortDateString(),
                    title = ev.Title,
                    titleDescription = ev.TitleDescription,
                    location = new
                    {
                        title = ev.LocationTitle,
                        address = ev.LocationAddress,
                        addressMapUrl = ev.LocationAddressMapUrl
                    },
                    description = ev.Description,
                    expired = DateTime.Now>ev.Date,
                    ticketOrderType = SiteContentHelper.TicketOrderTypeKeys[ev.TicketOrderType],
                    highlighted = ev.IsHighlighted,
                    highlightText = ev.HighlightedText,
                    previewContent = new
                    {
                        contentType = SiteContentHelper.PreviewContentTypeKeys[ev.PreviewContentType],
                        videoSrc = ev.PreviewContentVideoSrc,
                        contentImages = ev.PreviewContentImages.Select(image => image.ImageSrc).ToList()
                    },
                    content = new
                    {
                        action = ev.Action,
                        location = ev.Location,
                        artGroup = ev.ArtGroup
                    },
                    images = ev.ContentImages.Select(image => image.ImageSrc).ToList(),
                    duration = ev.Duration,
                    intervalQuantity = ev.IntervalQuantity,
                    price = ev.Price
                });
            }

            ViewBag.Events = "dataModels.events = " + JsonConvert.SerializeObject(result);

            return View();
        }

        public ActionResult EventDetails(int id)
        {
            var ev = _context.Events.First(e => e.Id == id);


            ViewBag.Event = "dataModels.event = " + JsonConvert.SerializeObject(new
            {
                id = ev.Id,
                date = ev.Date.ToShortDateString(),
                title = ev.Title,
                titleDescription = ev.TitleDescription,
                location = new
                {
                    title = ev.LocationTitle,
                    address = ev.LocationAddress,
                    highlightedTitlePart = ""
                },
                description = ev.Description,
                expired = DateTime.Now > ev.Date,
                ticketOrderType = SiteContentHelper.TicketOrderTypeKeys[ev.TicketOrderType],
                highlighted = ev.IsHighlighted,
                highlightText = ev.HighlightedText,
                previewContent = new
                {
                    contentType = SiteContentHelper.PreviewContentTypeKeys[ev.PreviewContentType],
                    videoSrc = ev.PreviewContentVideoSrc,
                    contentImages = ev.PreviewContentImages.Select(image => image.ImageSrc).ToList()
                },
                content = new
                {
                    action = ev.Action,
                    location = ev.Location,
                    artGroup = ev.ArtGroup
                },
                images = ev.ContentImages.Select(image => image.ImageSrc).ToList(),
                duration = ev.Duration,
                intervalQuantity = ev.IntervalQuantity,
                price = ev.Price
            });

            return View();
        }

        public ActionResult Partnership()
        {
            return View();
        }
        public ActionResult News()
        {
            return View();
        }
        public ActionResult Media()
        {
            return View();
        }

        public ActionResult Contacts()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Feedback(string name, string email, string question)
        {
            MailHelper.Notify(new FeedbackForm(){Email = email,Name = name, Question = question});
            return Json("1");
        }
    }
}
