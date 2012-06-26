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

        public ActionResult FeedbackForm()
        {
            return PartialView("FeedbackForm");
        }

        [HttpPost]
        [OutputCache(NoStore = true, Duration = 1, VaryByParam = "*")]
        public void FeedbackForm(FeedbackFormModel feedbackFormModel)
        {
            MailHelper.SendTemplate(new List<MailAddress> { new MailAddress("maxim@eugene-miller.com") }, "Форма обратной связи RVK", "FeedbackTemplate.htm", null, true, feedbackFormModel.Name, feedbackFormModel.Email, feedbackFormModel.Text);
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
