using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Excursions.Helpers;
using Excursions.Models;
using Excursions.Models.Captcha;

namespace Excursions.Controllers
{
    public class ContactsController : Controller
    {
        //
        // GET: /Contacts/

        public ActionResult Index(string message)
        {
            using (var context = new ContentStorage())
            {
                var contactsInfo = context.Content.Select(c => c).Where(c => c.IsContactsPage).First();
                ViewData["message"] = message;
                return View(contactsInfo);
            }
        }


        [HttpPost]
        [CaptchaValidation("captcha")]
        public ActionResult FeedBack(string author, string email, string feedbackText, bool captchaValid)
        {
            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(feedbackText) && captchaValid)
            {
                string emailFrom = ConfigurationManager.AppSettings["emailFrom"];
                string emailsTo = ApplicationData.NotificationEmail;
                string subject = "testours.1gb.ua - Новый отзыв";
                string[] replacements = {DateTime.Now.ToString(), author, email, feedbackText};
                MailHelper.SendTemplate(emailFrom, emailsTo, ";", subject, "newFeedback", false, replacements);
                return RedirectToAction("Index", "Contacts", new { message = "Сообщение отправлено" });
            }

            using (var context = new ContentStorage())
            {
                var contactsInfo = context.Content.Select(c => c).Where(c => c.IsContactsPage).First();
                ViewData["author"] = author;
                ViewData["email"] = email;
                ViewData["feedbackText"] = feedbackText;
                ViewData["message"] = null;
                return View("Index", contactsInfo);
            }
        }

    }
}
