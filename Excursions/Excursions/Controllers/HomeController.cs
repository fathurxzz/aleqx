﻿using System;
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
            string emailFrom = ConfigurationManager.AppSettings["emailFrom"];
            string emailsTo = ApplicationData.FeedbackNotificationEmail;
            string subject = "walking.kiev.ua - Новый отзыв";
            string[] replacements = { DateTime.Now.ToString(), feedbackFormModel.Name, feedbackFormModel.Email, feedbackFormModel.Text };
            MailHelper.SendTemplate(emailFrom, emailsTo, subject, "newFeedback", false, replacements);
            
            //Response.Write("<script>window.top.$.fancybox.close()</script>");
            //Response.Write("<script>alert('Thank you! Your message has been sent.')</script>");
            Response.Write("<div style=\"width:100%; text-align:center; margin-top:100px;\">Thank you! Your message has been sent.</div>");
            
        }
    }
}

namespace Excursions.Models
{
    public class FeedbackFormModel
    {
        
        [DisplayName("Name")]
        [RegularExpression("^[^<>]*$", ErrorMessage = "Message should not contain any of the following symbols: <, >.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "* Required")]
        [DisplayName("Email")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "* Required")]
        [DisplayName("Message")]
        [RegularExpression("^[^<>]*$", ErrorMessage = "Message should not contain any of the following symbols: <, >.")]
        public string Text { get; set; }
        
        [Captcha("ValidateCaptcha", "Captcha", "value", ErrorMessage = "Wrong symbols!")]
        [Required(ErrorMessage = "Enter symbols")]
        [DisplayName("")]
        public string Captcha { get; set; }
    }

    public class AddCommentFormModel
    {
        [Required(ErrorMessage = "* Required")]
        [DisplayName("Name")]
        [RegularExpression("^[^<>]*$", ErrorMessage = "Message should not contain any of the following symbols: <, >.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "* Required")]
        [DisplayName("Comment")]
        [RegularExpression("^[^<>]*$", ErrorMessage = "Message should not contain any of the following symbols: <, >.")]
        public string Text { get; set; }

        [Captcha("ValidateCaptcha", "Captcha", "value", ErrorMessage = "Wrong symbols!")]
        [Required(ErrorMessage = "Enter symbols")]
        [DisplayName("")]
        public string Captcha { get; set; }
    }
}
