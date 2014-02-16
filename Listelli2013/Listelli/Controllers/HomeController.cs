using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Listelli.Helpers;
using Listelli.Models;
using SiteExtensions;
using MailHelper = Listelli.Helpers.MailHelper;

namespace Listelli.Controllers
{
    public class HomeController : DefaultController
    {
        public ActionResult Index(string id)
        {

            //Thread tr = (Thread)HttpContext.Application["mailSender"];
            //var aaa = tr.ThreadState;

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
            try
            {
                if (ModelState.IsValid)
                {
                    using (var context = new CustomerContainer())
                    {
                        var subscriber = context.Subscriber.FirstOrDefault(s => s.Email == subscribeForm.SubscribeEmail);
                        if (subscriber != null)
                        {
                            subscribeForm.ErrorMessage = "Этот email уже есть в базе подписчиков";
                            return PartialView("SubscribeForm", subscribeForm);
                        }

                        subscriber = new Subscriber
                                         {
                                             Guid = Guid.NewGuid().ToString(),
                                             Email = subscribeForm.SubscribeEmail,
                                             Active = false,
                                             SentConfirmation = false
                                         };

                        context.AddToSubscriber(subscriber);




                        //string subscribeEmailFrom = ConfigurationManager.AppSettings["subscribeEmailFrom"];
                        //var emailFrom = new MailAddress(subscribeEmailFrom, "Listelli");
                        //var subscriberEmail = new MailAddress(subscriber.Email);
                        //var result = MailHelper.SendTemplate(emailFrom, new List<MailAddress> { subscriberEmail },
                        //                                             "Подтверждение регистрации", "ConfirmSubscribe.htm",
                        //                                             null, true, subscriber.Guid);

                        //if (!result.EmailSent)
                        //{
                        //    subscribeForm.ErrorMessage = "Ошибка: " + result.ErrorMessage;
                        //    return PartialView("SubscribeForm", subscribeForm);
                        //}

                        context.SaveChanges();

                        return PartialView("SubscribeSuccess");
                    }
                }
            }

            catch (Exception ex)
            {
                if (!String.IsNullOrEmpty(ex.Message))
                    subscribeForm.ErrorMessage = ex.Message;
                else if (!String.IsNullOrEmpty(ex.InnerException.Message))
                    subscribeForm.ErrorMessage = ex.InnerException.Message;
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



        public static void ProcessSendEmail()
        {
            while (true)
            {
                using (var context = new CustomerContainer())
                {
                    var emailStatus = context.SendEmailStatus.FirstOrDefault(s => s.Status == 0 && s.Attempt < 3);
                    var mailSubscriber = context.Subscriber.FirstOrDefault(s => s.SentConfirmation == false && s.SentConfirmationAttempt < 3);
                    if (emailStatus != null)
                    {
                        try
                        {
                            using (var siteContext = new SiteContainer())
                            {
                                var article = siteContext.Article.Include("ArticleItems").FirstOrDefault(c => c.Id == emailStatus.ArticleId);
                                if (article != null)
                                {

                                    var subscriber = context.Subscriber.First(s => s.Id == emailStatus.SubscriberId);

                                    var lng = siteContext.Language.FirstOrDefault(p => p.Code == "ru");
                                    if (lng != null)
                                    {
                                        article.CurrentLang = lng.Id;

                                        foreach (var item in article.ArticleItems)
                                        {
                                            item.CurrentLang = lng.Id;
                                        }

                                    }


                                    StringBuilder sb = new StringBuilder();

                                    sb.Append(HttpUtility.HtmlDecode(article.Description).Replace("src=\"", "src=\"http://listelli.ua"));

                                    foreach (var item in article.ArticleItems.OrderBy(a => a.SortOrder))
                                    {
                                        sb.Append("<div class=\"articleItem\">");

                                        if (item.ContentType == 1)
                                        {
                                            sb.Append(HttpUtility.HtmlDecode(item.Text));
                                        }
                                        else
                                        {
                                            sb.Append(SiteExtensions.Graphics2.GraphicsHelper.CachedImage(item.ImageSource, SiteSettings.GetThumbnail("articleImage")));
                                        }

                                        sb.Append("</div>");
                                    }

                                    string emailText = sb.ToString();

                                    string subscribeEmailFrom = ConfigurationManager.AppSettings["subscribeEmailFrom"];
                                    var emailFrom = new MailAddress(subscribeEmailFrom, "Listelli");

                                    MailSendingResult result = MailHelper.SendTemplate(emailFrom,
                                                            new List<MailAddress> { new MailAddress(subscriber.Email) },
                                                            article.Title, "Newsletter.htm", null, true, emailText);

                                    emailStatus.SendDate = DateTime.Now;
                                    emailStatus.Attempt++;

                                    if (result.EmailSent)
                                    {
                                        emailStatus.Status = 1;
                                        emailStatus.ErrorMessage = "ok";
                                    }
                                    else
                                    {
                                        emailStatus.ErrorMessage = "error sending email: " + result.ErrorMessage;
                                    }

                                    context.SaveChanges();
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                        catch
                        {
                            break;
                        }
                        //var test = new TestTable { Date = DateTime.Now };
                        //context.AddToTestTable(test);
                        //context.SaveChanges();
                    }
                    else if (mailSubscriber != null)
                    {
                        try
                        {
                            string subscribeEmailFrom = ConfigurationManager.AppSettings["subscribeEmailFrom"];
                            var emailFrom = new MailAddress(subscribeEmailFrom, "Listelli");
                            var subscriberEmail = new MailAddress(mailSubscriber.Email);
                            var result = MailHelper.SendTemplate(emailFrom, new List<MailAddress> { subscriberEmail }, "Подтверждение регистрации", "ConfirmSubscribe.htm", null, true, mailSubscriber.Guid);
                            if (result.EmailSent)
                            {
                                mailSubscriber.SentConfirmation = true;
                            }
                        }
                        catch
                        {

                        }

                        mailSubscriber.SentConfirmationAttempt++;
                        context.SaveChanges();
                    }
                    else
                    {
                        break;
                    }
                }

                Thread.Sleep(2000);
            }
        }
    }
}
