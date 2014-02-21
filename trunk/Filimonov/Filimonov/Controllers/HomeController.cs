using System;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web.Mvc;
using Filimonov.Models;
using SiteExtensions;

namespace Filimonov.Controllers
{
    [HandleError]
    [OutputCache(NoStore = true, VaryByParam = "*", Duration = 1)]
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
            using (var context = new SiteContainer())
            {
                var model = new SiteModel(context);
                this.SetSeoContent(model);
                return View(model);
            }
        }

        public ActionResult Projects(string id)
        {
            using (var context = new SiteContainer())
            {
                var model = new ProjectModel(context, id);
                return View(model);
            }
        }

        public ActionResult ProjectDetails(string id)
        {
            using (var context = new SiteContainer())
            {
                var model = new ProjectModel(context, id);
                return View(model.Project);
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

                    var emailFrom = new MailAddress(defaultMailAddressFrom, "fil-interiors");

                    var emailsTo = defaultMailAddresses
                        .Split(new[] { ";", " ", "," }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(s => new MailAddress(s))
                        .ToList();

                    var result = Helpers.MailHelper.SendTemplate(emailFrom, emailsTo, "Форма обратной связи", "FeedbackTemplate.htm", null, true, feedbackFormModel.Name, feedbackFormModel.Email, feedbackFormModel.Text);
                    if (result.EmailSent)
                        return PartialView("Success");
                    feedbackFormModel.ErrorMessage = "Ошибка: " + result.ErrorMessage;
                }
                catch (Exception ex)
                {
                    feedbackFormModel.ErrorMessage = ex.Message;
                }
            }
            return PartialView("FeedbackForm", feedbackFormModel);
        }

       
    }
}
