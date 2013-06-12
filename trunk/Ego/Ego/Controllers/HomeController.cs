using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Ego.Models;
using SiteExtensions;

namespace Ego.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string id, int? page)
        {
            using (var context = new SiteContainer())
            {
                var model = new SiteModel(context, id, page);
                ViewBag.IsHomePage = model.IsHomePage;
                ViewBag.Page = page ?? 0;
                ViewBag.TotalCount = model.TotalProductsCount;
                this.SetSeoContent(model);
                return View(model);
            }
        }

        public ActionResult ProductDetails(int id)
        {
            using (var context = new SiteContainer())
            {
                var product = context.Product.First(p => p.Id == id);
                return PartialView(new Order {ProductId = product.Id, ProductImageSource = product.ImageSource});
            }
        }

        public ActionResult Order(FormCollection form)
        {
            using (var context = new SiteContainer())
            {
                var order = new Order {Date = DateTime.Now};
                TryUpdateModel(order, new[] {"ProductId", "ProductImageSource", "Name", "Email", "Phone", "Description","Size"});
                context.AddToOrder(order);
                context.SaveChanges();

                string defaultMailAddressFrom = ConfigurationManager.AppSettings["feedbackEmailFrom"];
                string defaultMailAddresses = ConfigurationManager.AppSettings["feedbackEmailsTo"];

                var emailFrom = new MailAddress(defaultMailAddressFrom, "Студия Евгения Миллера");

                var emailsTo = defaultMailAddresses
                    .Split(new[] { ";", " ", "," }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => new MailAddress(s))
                    .ToList();

                var result = Helpers.MailHelper.SendTemplate(emailFrom, emailsTo, "Я - Эго. Форма заказа", "FeedbackTemplate.htm", null, true, form["Name"], form["Email"], form["ProductId"], form["Phone"],order.Id);




                return RedirectToAction("Index", "Home", new {id = "gallery"});
            }
        }

        
    }
}
