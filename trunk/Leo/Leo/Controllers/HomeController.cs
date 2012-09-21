using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Leo.Models;
using SiteExtensions;

namespace Leo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Leo";
            return View();
        }

        [OutputCache(NoStore = true, VaryByParam = "*", Duration = 1)]
        public ActionResult About()
        {
            using (var context = new SiteContainer())
            {
                var content = context.Content.First();
                return PartialView("_About", content);
            }
        }

        public ActionResult FeedbackForm()
        {
            return PartialView("FeedbackForm");
        }

        [HttpPost]
        [OutputCache(NoStore = true, Duration = 1, VaryByParam = "*")]
        public void FeedbackForm(FeedbackFormModel feedbackFormModel)
        {
            using (var context = new SiteContainer())
            {
                var emails = new List<MailAddress>
                                 {
                                     new MailAddress("leo@zaborovskiy.com.ua")
                                 };

                var responseData = MailHelper.SendTemplate(null, emails, "Форма обратной связи", null, null, true, feedbackFormModel.Name, feedbackFormModel.Email, feedbackFormModel.Text);
                var responseFeedback = new Feedback
                                           {
                                               Email = feedbackFormModel.Email,
                                               ErrorMessage = responseData.ErrorMessage,
                                               Text = feedbackFormModel.Text,
                                               Title = feedbackFormModel.Name,
                                               Sent = responseData.EmailSent
                                           };

                context.AddToFeedback(responseFeedback);
                context.SaveChanges();
            }
        }
    }
}
