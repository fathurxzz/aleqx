using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Excursions.Helpers;
using Excursions.Helpers.Validation;
using Excursions.Models;

namespace Excursions.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewData["Message"] = "Welcome to ASP.NET MVC!";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
        
        public ActionResult FeedbackForm()
        {

            return View();
        }

        [HttpPost]
        public void FeedbackForm(FeedbackFormModel feedbackFormModel)
        {

            /*
            SiteSettings settings = Configurator.LoadSettings();
            MailHelper.SendTemplate(new List<MailAddress> { new MailAddress(settings.ReceiverMail) },
                "Форма обратной связи", "FeedbackTemplate.htm",
                null, true, feedbackFormModel.Name, feedbackFormModel.Email, feedbackFormModel.Text);
            */


            string emailFrom = ConfigurationManager.AppSettings["emailFrom"];
            string emailsTo = ApplicationData.NotificationEmail;
            string subject = "testours.1gb.ua - Новый отзыв";
            string[] replacements = { DateTime.Now.ToString(), feedbackFormModel.Name, feedbackFormModel.Email, feedbackFormModel.Text };
            MailHelper.SendTemplate(emailFrom, emailsTo, subject, "newFeedback", false, replacements);
            
            //return RedirectToAction("Index", "Contacts", new { message = "Сообщение отправлено" });



            Response.Write("<script>window.top.$.fancybox.close()</script>");
        }
    }
}

namespace Excursions.Models
{
    public class FeedbackFormModel
    {
        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Имя, фамилия")]
        public string Name { get; set; }
        [DisplayName("Электропочта")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Неверно введен адрес почты. Формат: name@domain.com")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Текст запроса")]
        public string Text { get; set; }
        [Captcha("ValidateCaptcha", "Captcha", "value", ErrorMessage = "Неправильно введены символы с картинки!")]
        [Required(ErrorMessage = "Введите символы с картинки")]
        [DisplayName("")]
        public string Captcha { get; set; }
    }
}
