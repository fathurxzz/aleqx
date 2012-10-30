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
                var subscriber = new Customers {Guid = Guid.NewGuid().ToString()};
                TryUpdateModel(subscriber, new[] { "Name", "Email", "SubscribeType" });

                var email = form["Email"];
                byte subscrType = Convert.ToByte(form["SubscribeType"]);

                var existedEmail = context.Customers.FirstOrDefault(c => c.Email == email && c.SubscribeType == subscrType);
                if (existedEmail == null)
                {
                    context.AddToCustomers(subscriber);
                    context.SaveChanges();
                }
            }

            if (!string.IsNullOrEmpty(form["redirectUrl"]) && form["redirectUrl"] == "sbscrList")
                return RedirectToAction("Subscribers", "Subscribe");

            return RedirectToAction("ThankYou");
        }

        public ActionResult Unsubscribe(string id)
        {
            using (var context = new ContentStorage())
            {
                var customer = context.Customers.FirstOrDefault(c => c.Guid == id);
                if (customer != null)
                {
                    context.DeleteObject(customer);
                    context.SaveChanges();
                }
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
        public ActionResult SubscribersList()
        {
            using (var context = new ContentStorage())
            {
                var model = new SiteViewModel(null, context, false);
                var customers = context
                    .Customers
                    //.Take(200)
                    .ToList();

                model.Customers = customers;

                return View(model);
            }
        }

        [Authorize]
        public ActionResult Subscribers(int? s, int? f, int? templateId)
        {
            using (var context = new ContentStorage())
            {
                ViewBag.SentEmails = s.HasValue ? s.Value : 0;
                var model = new SiteViewModel(null, context, false);
                ViewBag.MailTemplates = context.MailTemplate.ToList();
                if (templateId.HasValue)
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
        public ActionResult SaveSubscribersSettings(FormCollection form)
        {
            using (var context = new ContentStorage())
            {
                PostData agentsData = form.ProcessPostData("agent");
                PostData touristData = form.ProcessPostData("tourist");

                int[] agentIds =
                    agentsData.Where(d => bool.Parse(d.Value["agent"])).Select(id => Convert.ToInt32(id.Key)).ToArray();
                int[] touristIds =
                    touristData.Where(d => bool.Parse(d.Value["tourist"])).Select(id => Convert.ToInt32(id.Key)).ToArray
                        ();

                var customers = context.Customers.ToList();
                var agents =
                    (from customer in customers from id in agentIds where customer.Id == id select customer).ToList();
                var tourists =
                    (from customer in customers from id in touristIds where customer.Id == id select customer).ToList();
                IEnumerable<Customers> all = agents.Concat(tourists);


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
            return RedirectToAction("SubscribersList");
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

                bool sendToTourists = form["tourists"] == "true,false";
                var sendToAgents = form["agents"] == "true,false";

                string formMailText = form["MailText"];
                if (sendToAgents)
                {
                    var agents = context.Customers.Where(c => c.SubscribeType == 1);
                    SendEmails(agents, formMailText, mailSubject, ref successedSentEmails, ref failedSentEmails);
                }

                if (sendToTourists)
                {
                    var tourists = context.Customers.Where(c => c.SubscribeType == 2).ToList();
                    SendEmails(tourists, formMailText, mailSubject, ref successedSentEmails, ref failedSentEmails);
                }
            }
            return RedirectToAction("Subscribers", new { s = successedSentEmails, f = failedSentEmails });
        }

        public void SendEmails(IEnumerable<Customers> customerses, string formMailText, string mailSubject, ref int successedSentEmails, ref int failedSentEmails)
        {
            if (string.IsNullOrEmpty(formMailText)) return;

            foreach (var customer in customerses)
            {
                var txt = "<br/><br/> Для того, чтобы отписаться от рассылке перейдите пожалуйста по следующей ссылке ссылке <br/>";
                txt += "<a href=\"http://havila-travel.com/unsubscribe/" + customer.Guid + "\">http://havila-travel.com/unsubscribe/" + customer.Guid + "</a>";

                var mailText = HttpUtility.HtmlDecode(formMailText + txt).Replace("src=\"",
                                                                            "src=\"http://havila-travel.com/");

                if (MailHelper.SendMessage(new MailAddress(customer.Email), mailText,
                                           mailSubject, true))
                    successedSentEmails++;
                else
                    failedSentEmails++;
            }
        }
    }
}
