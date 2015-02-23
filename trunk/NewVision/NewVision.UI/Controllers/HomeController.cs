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
