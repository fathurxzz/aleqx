﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Excursions.Helpers;
using Excursions.Models;
using Excursions.Models.Captcha;


namespace Excursions.Controllers
{
    public class ExcursionsController : Controller
    {
        //
        // GET: /Excursions/

        [HttpGet]
        public ActionResult Index()
        {
            using (var context = new ContentStorage())
            {
                var excursionList = context.Excursion.Select(e => e).OrderBy(e => e.SortOrder).ToList();
                return View(excursionList);
            }
        }


        public ActionResult Details(int id)
        {
            using (var context = new ContentStorage())
            {
                var excursion = context.Excursion.Include("Comments").Select(e => e).Where(e => e.Id == id).First();
                return View(excursion);
            }
        }


        [HttpPost]
        [CaptchaValidation("captcha")]
        public ActionResult CreateComment(Comments comment, int excursionId, string author, string commentText, bool captchaValid)
        {
            using (var context = new ContentStorage())
            {
                Excursion excursion = context.Excursion.Select(e => e).Where(e => e.Id == excursionId).First();

                if (captchaValid)
                {


                    comment.Excursion = excursion;
                    comment.Text = commentText;
                    comment.Author = author;
                    comment.Date = DateTime.Now;
                    context.AddToComments(comment);
                    context.SaveChanges();

                    string linkBase = ConfigurationManager.AppSettings["linkBase"];
                    string emailFrom = ConfigurationManager.AppSettings["emailFrom"];
                    string emailTo = ApplicationData.NotificationEmail;
                    string subject = "testours.1gb.ua - Новый комментарий";

                    string[] replacements = {
                                                linkBase + "/Excursions/Details/" + excursionId, comment.Date.ToString()
                                                ,
                                                comment.Author, comment.Text
                                            };

                    MailHelper.SendTemplate(emailFrom, new List<MailAddress> {new MailAddress(emailTo)}, subject,
                                            "newComment", false, replacements);


                    return RedirectToAction("Details", "Excursions", new {area = "", id = excursionId});
                }
                else
                {
                    ViewData["author"] = author;
                    ViewData["commentText"] = commentText;
                    
                    
                    return View("Details", excursion);
                }
            }

        }

    }
}
