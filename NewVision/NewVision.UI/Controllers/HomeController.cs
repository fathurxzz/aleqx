using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using NewVision.UI.App_LocalResources;
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

        private string GenerateMainMenu(int activeMenuItemId, bool active = false)
        {

            var contents = _context.Contents.ToList();

            var result = new List<object>();

            result.Add(new { id = 1, title = GlobalRes.MainMenuSchedule, selected = activeMenuItemId == 1, active = activeMenuItemId == 1 && active, url = "/" + CurrentLangCode + "/events" });
            result.Add(new { id = 2, title = GlobalRes.MainMenuNews, selected = activeMenuItemId == 2, active = activeMenuItemId == 2 && active, url = "/" + CurrentLangCode + "/news" });
            result.Add(new { id = 3, title = GlobalRes.Media, selected = activeMenuItemId == 3, active = activeMenuItemId == 3 && active, url = "/" + CurrentLangCode + "/media" });

            foreach (var content in contents.OrderBy(c => c.SortOrder))
            {
                result.Add(new { id = content.Id, title = content.MenuTitle, selected = activeMenuItemId == content.Id, active = activeMenuItemId == content.Id && active, url = "/" + CurrentLangCode + "/" + content.Name });
            }

            result.Add(new { id = 5, title = GlobalRes.Authors, selected = activeMenuItemId == 5, active = activeMenuItemId == 5 && active, url = "/" + CurrentLangCode + "/artists" });
            result.Add(new { id = 4, title = GlobalRes.Contacts, selected = activeMenuItemId == 4, active = activeMenuItemId == 4 && active, url = "/" + CurrentLangCode + "/contacts" });
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
                    title = CurrentLang == SiteLanguage.en ? mainBanner.TitleEn : CurrentLang == SiteLanguage.ua ? mainBanner.TitleUa : mainBanner.Title,
                    description = CurrentLang == SiteLanguage.en ? mainBanner.DescriptionEn : CurrentLang == SiteLanguage.ua ? mainBanner.DescriptionUa : mainBanner.Description,
                    imageSrc = mainBanner.ImageSrc
                });
            }

            var eventAnnouncements = new List<eventAnnouncement>();

            foreach (var eventAnnouncement in ea)
            {
                var ev = new eventAnnouncement
                {
                    title = CurrentLang == SiteLanguage.en ? eventAnnouncement.TitleEn : CurrentLang == SiteLanguage.ua ? eventAnnouncement.TitleUa : eventAnnouncement.Title,
                    text = CurrentLang == SiteLanguage.en ? eventAnnouncement.TextEn : CurrentLang == SiteLanguage.ua ? eventAnnouncement.TextUa : eventAnnouncement.Text,
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
                title = CurrentLang == SiteLanguage.en ? content.TitleEn : CurrentLang == SiteLanguage.ua ? content.TitleUa : content.Title,
                text = CurrentLang == SiteLanguage.en ? content.TextEn : CurrentLang == SiteLanguage.ua ? content.TextUa : content.Text,
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
                    title = CurrentLang == SiteLanguage.en ? ev.TitleEn : CurrentLang == SiteLanguage.ua ? ev.TitleUa : ev.Title,
                    titleDescription = CurrentLang == SiteLanguage.en ? ev.TitleDescriptionEn : CurrentLang == SiteLanguage.ua ? ev.TitleDescriptionUa : ev.TitleDescription,
                    location = new
                    {
                        title = CurrentLang == SiteLanguage.en ? ev.LocationTitleEn : CurrentLang == SiteLanguage.ua ? ev.LocationTitleUa : ev.LocationTitle,
                        address = CurrentLang == SiteLanguage.en ? ev.LocationAddressEn : CurrentLang == SiteLanguage.ua ? ev.LocationAddressUa : ev.LocationAddress,
                        addressMapUrl = ev.LocationAddressMapUrl
                    },
                    description = CurrentLang == SiteLanguage.en ? ev.DescriptionEn : CurrentLang == SiteLanguage.ua ? ev.DescriptionUa : ev.Description,
                    expired = DateTime.Now > ev.Date,
                    ticketOrderType = SiteContentHelper.TicketOrderTypeKeys[ev.TicketOrderType],
                    highlighted = ev.IsHighlighted,
                    highlightText = CurrentLang == SiteLanguage.en ? ev.HighlightedTextEn : CurrentLang == SiteLanguage.ua ? ev.HighlightedTextUa : ev.HighlightedText,
                    previewContent = new
                    {
                        contentType = SiteContentHelper.PreviewContentTypeKeys[ev.PreviewContentType],
                        videoSrc = ev.PreviewContentVideoSrc,
                        contentImages = ev.PreviewContentImages.Select(image => image.ImageSrc).ToList()
                    },
                    content = new
                    {
                        action = CurrentLang == SiteLanguage.en ? ev.ActionEn : CurrentLang == SiteLanguage.ua ? ev.ActionUa : ev.Action,
                        location = CurrentLang == SiteLanguage.en ? ev.LocationEn : CurrentLang == SiteLanguage.ua ? ev.LocationUa : ev.Location,
                        artGroup = CurrentLang == SiteLanguage.en ? ev.ArtGroupEn : CurrentLang == SiteLanguage.ua ? ev.ArtGroupUa : ev.ArtGroup
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
                title = CurrentLang == SiteLanguage.en ? ev.TitleEn : CurrentLang == SiteLanguage.ua ? ev.TitleUa : ev.Title,
                titleDescription = CurrentLang == SiteLanguage.en ? ev.TitleDescriptionEn : CurrentLang == SiteLanguage.ua ? ev.TitleDescriptionUa : ev.TitleDescription,
                location = new
                {
                    title = CurrentLang == SiteLanguage.en ? ev.LocationTitleEn : CurrentLang == SiteLanguage.ua ? ev.LocationTitleUa : ev.LocationTitle,
                    address = CurrentLang == SiteLanguage.en ? ev.LocationAddressEn : CurrentLang == SiteLanguage.ua ? ev.LocationAddressUa : ev.LocationAddress,
                    highlightedTitlePart = ""
                },
                description = CurrentLang == SiteLanguage.en ? ev.DescriptionEn : CurrentLang == SiteLanguage.ua ? ev.DescriptionUa : ev.Description,
                expired = DateTime.Now > ev.Date,
                ticketOrderType = SiteContentHelper.TicketOrderTypeKeys[ev.TicketOrderType],
                highlighted = ev.IsHighlighted,
                highlightText = CurrentLang == SiteLanguage.en ? ev.HighlightedTextEn : CurrentLang == SiteLanguage.ua ? ev.HighlightedTextUa : ev.HighlightedText,
                previewContent = new
                {
                    contentType = SiteContentHelper.PreviewContentTypeKeys[ev.PreviewContentType],
                    videoSrc = ev.PreviewContentVideoSrc,
                    contentImages = ev.PreviewContentImages.Select(image => image.ImageSrc).ToList()
                },
                content = new
                {
                    action = CurrentLang == SiteLanguage.en ? ev.ActionEn : CurrentLang == SiteLanguage.ua ? ev.ActionUa : ev.Action,
                    location = CurrentLang == SiteLanguage.en ? ev.LocationEn : CurrentLang == SiteLanguage.ua ? ev.LocationUa : ev.Location,
                    artGroup = CurrentLang == SiteLanguage.en ? ev.ArtGroupEn : CurrentLang == SiteLanguage.ua ? ev.ArtGroupUa : ev.ArtGroup
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
            }

            ViewBag.News = "dataModels.news = " + JsonConvert.SerializeObject(result);

            return View();
        }

        public ActionResult NewsDetails(int id)
        {
            ViewBag.MainMenu = GenerateMainMenu(2, true);
            var article = _context.Articles.First(a => a.Id == id);
            ViewBag.Article = "dataModels.newsDetails = " + JsonConvert.SerializeObject(new
            {
                id = article.Id,
                title = CurrentLang == SiteLanguage.en ? article.TitleEn : CurrentLang == SiteLanguage.ua ? article.TitleUa : article.Title,
                date = article.Date.ToShortDateString(),
                images = article.ArticleImages.Select(image => image.ImageSrc).ToList(),
                videoSrc = article.VideoSrc,
                text = CurrentLang == SiteLanguage.en ? article.TextEn : CurrentLang == SiteLanguage.ua ? article.TextUa : article.Text
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
                    title = CurrentLang == SiteLanguage.en ? article.TitleEn : CurrentLang == SiteLanguage.ua ? article.TitleUa : article.Title,
                    text = CurrentLang == SiteLanguage.en ? article.TextEn : CurrentLang == SiteLanguage.ua ? article.TextUa : article.Text,
                    mediaType = article.ContentType == 0 ? "image" : "video",
                    mediaSrc = article.ContentType == 0 ? article.ImageSrc : article.VideoSrc
                });
            }

            ViewBag.Media = "dataModels.media = " + JsonConvert.SerializeObject(result);
            return View();
        }

        public ActionResult Contacts()
        {
            ViewBag.MainMenu = GenerateMainMenu(4);
            return View();
        }


        public ActionResult Authors()
        {
            ViewBag.MainMenu = GenerateMainMenu(5);
            var result = new List<object>();
            var authors = _context.Authors.ToList();
            foreach (var author in authors)
            {
                result.Add(new
                {
                    name = author.Name,
                    title = CurrentLang == SiteLanguage.en ? author.TitleEn : CurrentLang == SiteLanguage.ua ? author.TitleUa : author.Title,
                    tags = CurrentLang == SiteLanguage.en ? author.Tags.Select(t => t.TitleEn).ToArray() : CurrentLang == SiteLanguage.ua ? author.Tags.Select(t => t.TitleUa).ToArray() : author.Tags.Select(t => t.Title).ToArray(),
                    photo = author.Photo
                });
            }
            ViewBag.Authors = "dataModels.authors = " + JsonConvert.SerializeObject(result);
            return View();
        }

        public ActionResult Products()
        {
            ViewBag.MainMenu = GenerateMainMenu(5);
            var result = new List<object>();
            var products = _context.Products.ToList();
            foreach (var product in products)
            {
                result.Add(new
                {
                    title = CurrentLang == SiteLanguage.en ? product.TitleEn : CurrentLang == SiteLanguage.ua ? product.TitleUa : product.Title,
                    tags = CurrentLang == SiteLanguage.en ? product.Tags.Select(t => t.TitleEn).ToArray() : CurrentLang == SiteLanguage.ua ? product.Tags.Select(t => t.TitleUa).ToArray() : product.Tags.Select(t => t.Title).ToArray(),
                    photo = product.ImageSrc,
                    author = new { name = product.Author.Name, title = CurrentLang == SiteLanguage.en ? product.Author.TitleEn : CurrentLang == SiteLanguage.ua ? product.Author.TitleUa : product.Author.Title }
                });
            }
            ViewBag.Products = "dataModels.products = " + JsonConvert.SerializeObject(result);
            return View();
        }

        public ActionResult ArtistAbout(string id)
        {
            ViewBag.MainMenu = GenerateMainMenu(5, true);
            var author = _context.Authors.First(a => a.Name == id);

            ViewBag.Author = "dataModels.author = " + JsonConvert.SerializeObject(new
            {
                name = author.Name,
                title = CurrentLang == SiteLanguage.en ? author.TitleEn : CurrentLang == SiteLanguage.ua ? author.TitleUa : author.Title,
                photo = author.Photo,
                description = CurrentLang == SiteLanguage.en ? author.AboutEn : CurrentLang == SiteLanguage.ua ? author.AboutUa : author.About
            });
            return View();
        }

        public ActionResult ArtistProducts(string id)
        {
            ViewBag.MainMenu = GenerateMainMenu(5, true);
            var author = _context.Authors.First(a => a.Name == id);
            var result = new List<object>();
            var products = author.Products.ToList();
            foreach (var product in products)
            {
                result.Add(new
                {
                    title = CurrentLang == SiteLanguage.en ? product.TitleEn : CurrentLang == SiteLanguage.ua ? product.TitleUa : product.Title,
                    tags = CurrentLang == SiteLanguage.en ? product.Tags.Select(t => t.TitleEn).ToArray() : CurrentLang == SiteLanguage.ua ? product.Tags.Select(t => t.TitleUa).ToArray() : product.Tags.Select(t => t.Title).ToArray(),
                    photo = product.ImageSrc,
                    author = new { name = product.Author.Name, title = CurrentLang == SiteLanguage.en ? product.Author.TitleEn : CurrentLang == SiteLanguage.ua ? product.Author.TitleUa : product.Author.Title }
                });
            }

            ViewBag.Products = "dataModels.products = " + JsonConvert.SerializeObject(result);
            ViewBag.Author = "dataModels.author = " + JsonConvert.SerializeObject(new
            {
                name = author.Name,
                title = CurrentLang == SiteLanguage.en ? author.TitleEn : CurrentLang == SiteLanguage.ua ? author.TitleUa : author.Title,
                photo = author.Photo,
                description = CurrentLang == SiteLanguage.en ? author.AboutEn : CurrentLang == SiteLanguage.ua ? author.AboutUa : author.About
            });
            return View();

        }

        public ActionResult ArtistProductsDetails(string id)
        {
            ViewBag.MainMenu = GenerateMainMenu(5, true);
            var author = _context.Authors.First(a => a.Name == id);
            var result = new List<object>();
            var products = author.Products.ToList();
            foreach (var product in products)
            {
                result.Add(new
                {
                    title = CurrentLang == SiteLanguage.en ? product.TitleEn : CurrentLang == SiteLanguage.ua ? product.TitleUa : product.Title,
                    tags = CurrentLang == SiteLanguage.en ? product.Tags.Select(t => t.TitleEn).ToArray() : CurrentLang == SiteLanguage.ua ? product.Tags.Select(t => t.TitleUa).ToArray() : product.Tags.Select(t => t.Title).ToArray(),
                    photo = product.ImageSrc,
                    author = new { name = product.Author.Name, title = CurrentLang == SiteLanguage.en ? product.Author.TitleEn : CurrentLang == SiteLanguage.ua ? product.Author.TitleUa : product.Author.Title },
                    price = product.Price
                });
            }

            ViewBag.Products = "dataModels.products = " + JsonConvert.SerializeObject(result);
            ViewBag.Author = "dataModels.author = " + JsonConvert.SerializeObject(new
            {
                name = author.Name,
                title = CurrentLang == SiteLanguage.en ? author.TitleEn : CurrentLang == SiteLanguage.ua ? author.TitleUa : author.Title,
                photo = author.Photo,
                description = CurrentLang == SiteLanguage.en ? author.AboutEn : CurrentLang == SiteLanguage.ua ? author.AboutUa : author.About,
                images=products.Select(image => image.ImageSrc).ToList()
            });
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
