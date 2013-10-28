using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Wi_Fi_Pass.Models;

namespace Wi_Fi_Pass.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Edit()
        {
            using (var context = new SiteContainer())
            {
                MapContent content = context.MapContent.First();
                return View(content);
            }
            
        }

        [HttpPost]
        public ActionResult Edit(FormCollection form)
        {
            using (var context = new SiteContainer())
            {
                MapContent content = context.MapContent.First();
                content.Text = HttpUtility.HtmlDecode(form["Text"]);
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }


        public ActionResult Index()
        {
            using (var context = new SiteContainer())
            {
                MapContent content = context.MapContent.First();
                return View(content);
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
            try
            {
                string defaultMailAddressFrom = ConfigurationManager.AppSettings["feedbackEmailFrom"];
                string defaultMailAddresses = ConfigurationManager.AppSettings["feedbackEmailsTo"];

                var emailFrom = new MailAddress(defaultMailAddressFrom, "wi-fi pass");

                var emailsTo = defaultMailAddresses
                    .Split(new[] { ";", " ", "," }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => new MailAddress(s))
                    .ToList();
                Helpers.MailHelper.SendTemplate(emailFrom, emailsTo, "wi-fi pass", "FeedbackTemplate.htm", null, true, feedbackFormModel.Text);
            }
            catch
            {
            }
        }
    }
}
