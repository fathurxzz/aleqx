using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Poggen.Models;
using SiteExtensions;

namespace Poggen.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string id)
        {
            using (var context = new SiteContainer())
            {
                var model = new SiteViewModel(context, id ?? "");
                this.SetSeoContent(model);
                ViewBag.isHomePage = model.IsHomePage;
                if (model.Content != null)
                    ViewBag.ContentName = model.Content.Name;
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult FeedbackForm(FeedbackFormModel feedbackFormModel)
        {
            if (ModelState.IsValid)
            {
                var emails = new List<MailAddress>
                                 {
                                     new MailAddress(ConfigurationManager.AppSettings["feedbackEmail"])
                                 };
                var responseData = MailHelper.SendTemplate(null, emails, "Форма обратной связи", null, null, true, feedbackFormModel.Name, feedbackFormModel.Email, feedbackFormModel.Text);
                if (responseData.EmailSent)
                    return PartialView("Success");
                feedbackFormModel.ErrorMessage = "Ошибка: "+ responseData.ErrorMessage;
            }
            return PartialView("_FeedbackForm", feedbackFormModel);
        }
    }
}
