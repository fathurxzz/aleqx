using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using EM2013.Models;
using SiteExtensions;

namespace EM2013.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string category, string product, int? page)
        {
            using (var context = new SiteContext())
            {
                var model = new CatalogueViewModel(context, category ?? "", product, page);
                ViewBag.Page = page ?? 0;
                this.SetSeoContent(model);
                ViewBag.PageTitle = model.PageTitle;
                ViewBag.isHomePage = model.IsHomePage;

                if (model.Content != null)
                {
                    return View("Content", model);
                }

                if(model.Product!=null)
                {
                    return View("Product", model);
                }

                return View(model);
            }
        }


        public ActionResult SiteContent(string id)
        {
            using (var context = new SiteContext())
            {
                var model = new SiteViewModel(context, id);
                this.SetSeoContent(model);
                ViewBag.PageTitle = model.PageTitle;
                ViewBag.isHomePage = false;
                return View("Content", model);
            }
        }

        [HttpPost]
        public ActionResult Feedback(FeedbackFormModel feedbackFormModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string defaultMailAddressFrom = ConfigurationManager.AppSettings["feedbackEmailFrom"];
                    string defaultMailAddresses = ConfigurationManager.AppSettings["feedbackEmailsTo"];
                    
                    var emailFrom = new MailAddress(defaultMailAddressFrom);
                    
                    var emailsTo = defaultMailAddresses
                        .Split(new[] { ";", " ", "," }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(s => new MailAddress(s))
                        .ToList();

                    ResponseData responseData = MailHelper.SendTemplate(emailFrom, emailsTo, "Форма обратной связи", null, null, true, feedbackFormModel.Name, feedbackFormModel.Email, feedbackFormModel.Text);
                    if (responseData.EmailSent)
                        return PartialView("Success");
                    feedbackFormModel.ErrorMessage = "Ошибка: " + responseData.ErrorMessage;
                    
                    
                    //Helpers.MailHelper.SendTemplate(new List<MailAddress> { new MailAddress("miller.kak.miller@gmail.com") }, "Форма обратной связи", "FeedbackTemplate.htm", null, true, feedbackFormModel.Name, feedbackFormModel.Email, feedbackFormModel.Text);

                    //Helpers.MailHelper.SendTemplate(new List<MailAddress> { new MailAddress("kushko.alex@gmail.com") }, "Форма обратной связи", "FeedbackTemplate.htm", null, true, feedbackFormModel.Name, feedbackFormModel.Email, feedbackFormModel.Text);
                    //return PartialView("Success");
                }
                catch (Exception ex)
                {
                    feedbackFormModel.ErrorMessage = ex.Message;
                }
            }
            return PartialView("FeedbackForm", feedbackFormModel);
        }

        public ActionResult English()
        {
            using (var context = new SiteContext())
            {
                var model = new SiteViewModel(context, null);
                ViewBag.PageTitle = model.PageTitle;
                ViewBag.isHomePage = false;
                return View(model);
            }
        }
    }
}
