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
        public ActionResult Index(string category, string product)
        {
            using (var context = new SiteContext())
            {
                var model = new CatalogueViewModel(context, category ?? "", product);
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
                    string defaultMailAddress = ConfigurationManager.AppSettings["feedbackEmail"];
                    var emails = new List<MailAddress>
                                 {
                                     new MailAddress(defaultMailAddress)
                                 };
                    ResponseData responseData = MailHelper.SendTemplate(new MailAddress("m@m-brand.com.ua"), emails, "Форма обратной связи", null, null, true, feedbackFormModel.Name, feedbackFormModel.Email, feedbackFormModel.Text);
                    if (responseData.EmailSent)
                        return PartialView("Success");
                    feedbackFormModel.ErrorMessage = "Ошибка: " + responseData.ErrorMessage;
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
