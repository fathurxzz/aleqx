using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Rvk.Helpers;
using Rvk.Models;

namespace Rvk.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Рывок";
            return View();
            //using (var context = new ModelContainer())
            //{
            //    var subscribers = context.Subscriber.ToList();
            //    ViewBag.Title = "Рывок "+subscribers.Count;
            //    return View();
            //}
        }


        public ActionResult FeedBack()
        {
            using (var context = new ModelContainer())
            {
                var feedbacks = context.Feedback.ToList();
                return View(feedbacks);
            }
        }

        public ActionResult Subscribers()
        {
            using (var context = new ModelContainer())
            {
                var subscr = context.Subscriber.ToList();
                return View(subscr);
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
            using (var context = new ModelContainer())
            {
                var feedback = new Feedback
                                   {
                                       Email = feedbackFormModel.Email,
                                       Text = feedbackFormModel.Text,
                                       Title = feedbackFormModel.Name
                                   };
                context.AddToFeedback(feedback);
                context.SaveChanges();


                var responseData = MailHelper.SendTemplate(
                    new List<MailAddress>
                        {
                            new MailAddress("maxim@eugene-miller.com"),
                            new MailAddress("kushko.alex@gmail.com")
                        },
                    "Форма обратной связи RVK", "FeedbackTemplate.htm", null, true, feedbackFormModel.Name,
                    feedbackFormModel.Email, feedbackFormModel.Text);


                var responseFeedback = new Feedback{Email = "",Text = responseData.ErrorMessage,Title = responseData.EmailSent.ToString()};
                context.AddToFeedback(responseFeedback);
                context.SaveChanges();
            }
        }

        [HttpPost]
        [OutputCache(NoStore = true, Duration = 1, VaryByParam = "*")]
        public void Subscribe(string subscr)
        {
            using (var context = new ModelContainer())
            {
                var exist = context.Subscriber.FirstOrDefault(s => s.Email == subscr);
                if (exist != null) return;

                var subscriber = new Subscriber {Email = subscr};
                context.AddToSubscriber(subscriber);
                context.SaveChanges();
            }
        }
    }
}
