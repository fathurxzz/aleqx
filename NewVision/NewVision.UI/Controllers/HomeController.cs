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

        private string GenerateMainMenu(int activeMenuItemId, bool active=false)
        {

            var contents = _context.Contents.ToList();

            var result = new List<object>();

            result.Add(new { id = 1, title = "расписание проектов", selected = activeMenuItemId == 1, active = activeMenuItemId == 1 && active, url = "/events" });
            result.Add(new { id = 2, title = "новости и события", selected = activeMenuItemId == 2, active = activeMenuItemId == 2 && active, url = "/news" });
            result.Add(new { id = 3, title = "медиа", selected = activeMenuItemId == 3, active = activeMenuItemId == 3 && active, url = "/media" });

            foreach (var content in contents.OrderBy(c => c.SortOrder))
            {
                result.Add(new { id = content.Id, title = content.MenuTitle, selected = activeMenuItemId == content.Id, active = activeMenuItemId == content.Id && active, url = "/" + content.Name });
            }

            result.Add(new { id = 4, title = "контактная информация", selected = activeMenuItemId == 4, active = activeMenuItemId == 4 && active, url = "/contacts" });
            return "dataModels.mainMenu = " + JsonConvert.SerializeObject(result);

        }

        public ActionResult Index()
        {
            ViewBag.MainMenu = GenerateMainMenu(0);

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
                var ev = new eventAnnouncement
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


            var mainPage = new mainPage { mainBanners = mainBanners, eventAnnouncements = eventAnnouncements };


            ViewBag.MainPage = "dataModels.mainPage = " + JsonConvert.SerializeObject(mainPage);


            return View();
        }


        public ActionResult SiteContent(string id)
        {
            var content = _context.Contents.First(c => c.Name == id);
            ViewBag.SiteContent = "dataModels.siteContent = " + JsonConvert.SerializeObject(new
            {
                id = content.Id,
                title = content.Title,
                text = content.Text,
                imageSrc = content.ImageSrc
            });
            ViewBag.MainMenu = GenerateMainMenu(content.Id);
            return View();
        }


        public ActionResult Events()
        {

            ViewBag.MainMenu = GenerateMainMenu(1);

            var result = new List<object>();

            var events = _context.Events.ToList();



            foreach (var ev in events.OrderByDescending(e => e.Date))
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
            }

            ViewBag.Events = "dataModels.events = " + JsonConvert.SerializeObject(result);




            return View();
        }

        public ActionResult EventDetails(int id)
        {
            ViewBag.MainMenu = GenerateMainMenu(1, true);

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

        public ActionResult News()
        {
            ViewBag.MainMenu = GenerateMainMenu(2);
            var result = new List<object>();

            var articles = _context.Articles.ToList();
            foreach (var article in articles)
            {
                result.Add(new
                {
                    id = article.Id,
                    title = article.Title,
                    date = article.Date.ToShortDateString(),
                    imageSrc = article.ImageSrc,
                    size = "1x1",
                    titlePosition = SiteContentHelper.TitlePositionKeys[article.TitlePosition]
                });
                ViewBag.News = "dataModels.news = " + JsonConvert.SerializeObject(result);
            }

            return View();
        }

        public ActionResult NewsDetails(int id)
        {
            ViewBag.MainMenu = GenerateMainMenu(2, true);
            var article = _context.Articles.First(a => a.Id == id);
            ViewBag.Article = "dataModels.article = " + JsonConvert.SerializeObject(new
            {
                id = article.Id,
                title = article.Title,
                date = article.Date.ToShortDateString(),
                images = article.ArticleImages.Select(image => image.ImageSrc).ToList()
            });
            return View();
        }

        public ActionResult Media()
        {
            ViewBag.MainMenu = GenerateMainMenu(3);
            var result = new List<object>();

            var articles = _context.Media.ToList();
            foreach (var article in articles.OrderBy(c => c.SortOrder))
            {
                result.Add(new
                {
                    id = article.Id,
                    title = article.Title,
                    text = article.Title,
                    mediaType = article.ContentType == 0 ? "image" : "video",
                    mediaSrc = article.ContentType == 0 ? article.ImageSrc : article.VideoSrc
                });
                ViewBag.Media = "dataModels.media = " + JsonConvert.SerializeObject(result);
            }
            return View();
        }

        public ActionResult Contacts()
        {
            ViewBag.MainMenu = GenerateMainMenu(4);
            return View();
        }

        [HttpPost]
        public JsonResult Feedback(string name, string email, string question)
        {
            MailHelper.Notify(new FeedbackForm { Email = email, Name = name, Question = question });
            return Json("1");
        }
    }
}
