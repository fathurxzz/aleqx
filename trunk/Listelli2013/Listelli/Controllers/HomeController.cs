using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web.Mvc;
using Listelli.Models;
using SiteExtensions;

namespace Listelli.Controllers
{
    public class HomeController : DefaultController
    {
        public ActionResult Index(string id)
        {
            using (var context = new SiteContainer())
            {
                var model = new SiteModel(CurrentLang, context, id);
                ViewBag.IsHomePage = model.IsHomePage;
                this.SetSeoContent(model);
                ViewBag.CurrentMenuItem = model.Content.Name;
                return View(model);
            }
        }

        public ActionResult Articles()
        {
            using (var context = new SiteContainer())
            {
                var model = new ArticlesModel(CurrentLang, context);
                ViewBag.CurrentMenuItem = "news";
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult Subscribe(SubscribeFormModel subscribeForm)
        {
            if (ModelState.IsValid)
            {
                using (var context = new CustomerContainer())
                {
                    var subscriber = context.Subscriber.FirstOrDefault(s => s.Email == subscribeForm.SubscribeEmail);
                    if (subscriber != null)
                    {
                        subscribeForm.ErrorMessage = "Этот email уже есть в базе подписчиков" ;
                        return PartialView("SubscribeForm", subscribeForm);
                    }

                    subscriber = new Subscriber
                                 {
                                     Guid = Guid.NewGuid().ToString(),
                                     Email = subscribeForm.SubscribeEmail,
                                     Active = false
                                 };

                    context.AddToSubscriber(subscriber);
                    context.SaveChanges();



                    string subscribeEmailFrom = ConfigurationManager.AppSettings["subscribeEmailFrom"];

                    var emailFrom = new MailAddress(subscribeEmailFrom, "Listelli");
                    var subscriberEmail = new MailAddress(subscriber.Email);


                    var result = Helpers.MailHelper.SendTemplate(emailFrom, new List<MailAddress> { subscriberEmail }, "Подтверждение регистрации", "ConfirmSubscribe.htm", null, true, subscriber.Guid);

                    if (result.EmailSent)
                        return PartialView("SubscribeSuccess");
                    else
                    {
                        subscribeForm.ErrorMessage = "Ошибка: " + result.ErrorMessage;
                        return PartialView("SubscribeForm", subscribeForm);
                    }

                    return PartialView("SubscribeSuccess");
                }
            }
            return PartialView("SubscribeForm", subscribeForm);
        }


        public ActionResult ConfirmSubscribe(string id)
        {
            using (var context = new CustomerContainer())
            {
                var subscriber = context.Subscriber.FirstOrDefault(s => s.Guid == id);
                if (subscriber == null)
                {
                    ViewBag.Message = "Неверный код подтверждения рассылки";
                }
                else
                {
                    subscriber.Active = true;
                    context.SaveChanges();
                }

                return View();
            }
        }
    }
}
