﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Posh.Helpers;
using Posh.Models;

namespace Posh.Controllers
{
    public class NewsController : Controller
    {
        public ActionResult Index()
        {
            using (var context = new ModelContainer())
            {
                var model = new NewsModel(context, "news");
                this.SetSeoContent(model);

                ViewBag.MainMenu = model.MainMenu;
                ViewBag.isHomePage = model.IsHomePage;

                return View(model);
            }
        }

        public ActionResult Subscribe()
        {
            using (var context = new ModelContainer())
            {
                var model = new NewsModel(context, null,false);
                this.SetSeoContent(model);
                ViewBag.MainMenu = model.MainMenu;
                ViewBag.isHomePage = model.IsHomePage;
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult Subscribe(FormCollection form)
        {
            using (var context = new ModelContainer())
            {
                var subscriber = new Subscriber();
                TryUpdateModel(subscriber, new[] { "Name", "Email" });
                context.AddToSubscriber(subscriber);
                context.SaveChanges();
            }
            return RedirectToAction("ThankYou");
        }

        public ActionResult ThankYou()
        {
            using (var context = new ModelContainer())
            {
                var model = new NewsModel(context, null, false);
                this.SetSeoContent(model);
                ViewBag.MainMenu = model.MainMenu;
                ViewBag.isHomePage = model.IsHomePage;
                return View(model);
            }
        }

    }
}
