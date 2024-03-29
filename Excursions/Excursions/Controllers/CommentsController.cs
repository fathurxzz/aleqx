﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Excursions.Helpers;
using Excursions.Models;

namespace Excursions.Controllers
{
    public class CommentsController : Controller
    {
        //
        // GET: /Comments/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Comments/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Comments/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Comments/Create

        //[HttpPost]
        //[CaptchaValidation("captcha")]
        //public ActionResult Create(Comments comment, int excursionId, string author, string commentText, bool captchaValid)
        //{
        //    if (captchaValid)
        //    {
        //        using (var context = new ContentStorage())
        //        {
        //            Excursion excursion = context.Excursion.Select(e => e).Where(e => e.Id == excursionId).First();
        //            comment.Excursion = excursion;
        //            comment.Text = HttpUtility.HtmlDecode(commentText);
        //            comment.Author = author;
        //            comment.Date = DateTime.Now;
        //            context.AddToComments(comment);
        //            context.SaveChanges();

        //            string linkBase = ConfigurationManager.AppSettings["linkBase"];
        //            string emailFrom = ConfigurationManager.AppSettings["emailFrom"];
        //            string emailTo = ConfigurationManager.AppSettings["emailTo"];
        //            string subject = "testours.1gb.ua - Новый комментарий";

        //            string[] replacements = {
        //                                        linkBase + "/Excursions/Details/" + excursionId, comment.Date.ToString(),
        //                                        comment.Author, comment.Text
        //                                    };

        //            MailHelper.SendTemplate(emailFrom, new List<MailAddress> {new MailAddress(emailTo)}, subject,
        //                                    "newComment", false, replacements);

        //        }
        //        return RedirectToAction("Details", "Excursions", new { area = "", id = excursionId });
        //    }
        //    else
        //    {
        //        return RedirectToAction()
        //    }
            
        //}

        //
        // GET: /Comments/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Comments/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Comments/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Comments/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
