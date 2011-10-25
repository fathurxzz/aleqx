﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HavilaTravel.Models;

namespace HavilaTravel.Areas.Admin.Controllers
{
    [Authorize]
    public class AccordionController : Controller
    {
        //
        // GET: /Admin/Accordion/
        public ActionResult Create(int id)
        {
            ViewBag.parentId = id;
            return View();
        }

        [HttpPost]
        public ActionResult Create(int id, int parentId, FormCollection form)
        {
            using (var context = new ContentStorage())
            {
                var content = context.Content.Where(c => c.Id == id).First();

                var accordion = new Accordion();
                TryUpdateModel(accordion, new[]
                                              {
                                                  "Title",
                                                  "SortOrder"
                                              });

                accordion.Text = HttpUtility.HtmlDecode(form["Text"]);
                accordion.Content = content;
                context.AddToAccordion(accordion);
                context.SaveChanges();

                return RedirectToAction("Index", "Home", new { id = content.Name, area = "" });
            }
        }

        public ActionResult Edit(int id)
        {
            using (var context = new ContentStorage())
            {
                var accordion = context.Accordion.Where(a => a.Id == id).First();
                return View(accordion);
            }
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection form)
        {
            using (var context = new ContentStorage())
            {
                var accordion = context.Accordion.Include("Content").Where(a => a.Id == id).First();
                var content = accordion.Content;

                return RedirectToAction("Index", "Home", new {id = content.Name, area = ""});
            }
        }

        public ActionResult AddPhoto(int id)
        {
            return View();
        }

    }
}
