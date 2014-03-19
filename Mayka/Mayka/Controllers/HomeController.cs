using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Mayka.Helpers;
using Mayka.Models;
using SiteExtensions;

namespace Mayka.Controllers
{
    public class HomeController : BaseController
    {
        private readonly SiteContext _context;

        public HomeController(SiteContext context)
        {
            _context = context;
        }

        public ActionResult Index(string id)
        {
            var model = new SiteModel(_context, id ?? "");
            ViewBag.isHomePage = model.Content.ContentType == (int)ContentType.HomePage;
            this.SetSeoContent(model);
            return View(model);
        }

        public ActionResult Gallery(string id)
        {
            var model = new SiteModel(_context, id);
            this.SetSeoContent(model);
            return View(model);
        }

        public ActionResult Products(string id)
        {
            var model = new SiteModel(_context, id);
            this.SetSeoContent(model);
            return View(model);
        }

        public ActionResult ProductDetails(int id)
        {
            var model = new SiteModel(_context, null, id);
            return View(model);
        }

        public void NotifyMiller(string phone, string url)
        {
            if (string.IsNullOrEmpty(url) || string.IsNullOrEmpty(phone))
                throw new HttpException(400, "Phone and url are required");
            SmtpClient client = new SmtpClient();
            MailMessage message = new MailMessage();
            message.To.Add("kushko.alex@gmail.com");
            message.Subject = "m-j - Перезвони по футболке";
            message.Body = string.Format("<div>Телефон: {0}</div><div><img src=\"{1}\" /></div>", phone, url);
            message.IsBodyHtml = true;
            client.Send(message);
            message.Dispose();
        }
    }
}
