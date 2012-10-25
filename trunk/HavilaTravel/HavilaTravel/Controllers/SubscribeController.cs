using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using HavilaTravel.Helpers;
using HavilaTravel.Models;

namespace HavilaTravel.Controllers
{
    public class SubscribeController : Controller
    {
        //
        // GET: /Subscribe/


        public ActionResult Index()
        {
            using (var context = new ContentStorage())
            {
                var model = new SiteViewModel(null, context, false);
                return View(model);
            }
        }



        [HttpPost]
        public ActionResult Subscribe(FormCollection form)
        {
            using (var context = new ContentStorage())
            {
                var subscriber = new Customers();
                TryUpdateModel(subscriber, new[] { "Name", "Email", "SubscribeType" });
                context.AddToCustomers(subscriber);
                context.SaveChanges();
            }

            if (!string.IsNullOrEmpty(form["redirectUrl"]) && form["redirectUrl"] == "sbscrList")
                return RedirectToAction("Subscribers", "Subscribe");

            return RedirectToAction("ThankYou");
        }

        public ActionResult Unsubscribe(int id)
        {
            using (var context = new ContentStorage())
            {
                var customer = context.Customers.First(c => c.Id == id);
                context.DeleteObject(customer);
                context.SaveChanges();
            }
            return RedirectToAction("Unsubscribed");
        }

        public ActionResult Unsubscribed()
        {
            using (var context = new ContentStorage())
            {
                var model = new SiteViewModel(null, context, false);
                return View(model);
            }
        }

        public ActionResult ThankYou()
        {
            using (var context = new ContentStorage())
            {
                var model = new SiteViewModel(null, context, false);
                return View(model);
            }
        }

        [Authorize]
        public ActionResult Subscribers(int? s, int? f, int? templateId)
        {
            using (var context = new ContentStorage())
            {
                ViewBag.SentEmails = s.HasValue ? s.Value : 0;

                var model = new SiteViewModel(null, context, false)
                    {
                        Customers = context
                        .Customers
                        //.Take(200)
                        .ToList()
                    };

                ViewBag.MailTemplates = context.MailTemplate.ToList();
                if(templateId.HasValue)
                {
                    ViewBag.MailText = context.MailTemplate.First(t => t.Id == templateId.Value).Text;
                }
                return View(model);
            }
        }


        public ActionResult EditSubscriber(int id)
        {
            using (var context = new ContentStorage())
            {
                var subscriber = context.Customers.First(c => c.Id == id);
                return View(subscriber);    
            }
        }

        [HttpPost]
        public ActionResult EditSubscriber(int id, FormCollection form)
        {
            using (var context = new ContentStorage())
            {
                var subscriber = context.Customers.First(c => c.Id == id);
                TryUpdateModel(subscriber, new[] { "Name", "Email", "SubscribeType" });
                context.SaveChanges();
                return RedirectToAction("Subscribers", "Subscribe");
            }
        }


        [Authorize]
        public ActionResult DeleteSubscriber(int id)
        {
            using (var context = new ContentStorage())
            {
                var subscriber = context.Customers.First(c => c.Id == id);
                context.DeleteObject(subscriber);
                context.SaveChanges();
            }

            return RedirectToAction("Subscribers");
        }

        [Authorize]
        public ActionResult SendEmail(FormCollection form)
        {

            string mailSubject = "Рассылка Havila-Travel";
            if (!string.IsNullOrEmpty(form["mailSubject"]))
            {
                mailSubject = form["mailSubject"];
            }

            

            int successedSentEmails = 0;
            int failedSentEmails = 0;
            
            



            
                using (var context = new ContentStorage())
                {

                    PostData agentsData = form.ProcessPostData("agent");
                    PostData touristData = form.ProcessPostData("tourist");

                    int[] agentIds = agentsData.Where(d => bool.Parse(d.Value["agent"])).Select(id => Convert.ToInt32(id.Key)).ToArray();
                    int[] touristIds = touristData.Where(d => bool.Parse(d.Value["tourist"])).Select(id => Convert.ToInt32(id.Key)).ToArray();

                    var customers = context.Customers.ToList();
                    var agents = (from customer in customers from id in agentIds where customer.Id == id select customer).ToList();
                    var tourists = (from customer in customers from id in touristIds where customer.Id == id select customer).ToList();

                    IEnumerable<Customers> all = agents.Concat(tourists);

                    string kkey = form.Keys.Cast<string>().Where(key => key.StartsWith("fa")).FirstOrDefault();
                    if (kkey == "faSaveDefaultSubscribers")
                    {
                        foreach (var customer in customers)
                        {
                            customer.IsActive = 0;
                        }

                        foreach (var customer in customers)
                        {
                            if (agentIds.Contains(Convert.ToInt32(customer.Id)) || touristIds.Contains(Convert.ToInt32(customer.Id)))
                                customer.IsActive = 1;
                        }
                        context.SaveChanges();
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(form["MailText"]))
                        {
                            foreach (var customer in all)
                            {

                                string formMailText = form["MailText"];

                                formMailText += "<br/><br/> Для того, чтобы отписаться от рассылке перейдите пожалуйста по следующей ссылке ссылке <br/>";
                                formMailText += "<a href=\"http://havila-travel.com/unsubscribe/" + customer.Id + "\">http://havila-travel.com/unsubscribe/"+customer.Id+"</a>";

                                var mailText = HttpUtility.HtmlDecode(formMailText).Replace("src=\"", "src=\"http://havila-travel.com/");


                                if (MailHelper.SendMessage(new MailAddress(customer.Email), mailText,
                                                           mailSubject, true))
                                    successedSentEmails++;
                                else
                                    failedSentEmails++;
                            }
                        }
                    }
                }
            

            return RedirectToAction("Subscribers", new { s = successedSentEmails, f = failedSentEmails });
        }
    }
}
