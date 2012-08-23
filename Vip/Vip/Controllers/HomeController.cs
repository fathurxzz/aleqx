﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SiteExtensions;
using Vip.Models;

namespace Vip.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string id)
        {
            using (var context = new SiteContainer())
            {
                var model = new SiteViewModel(context, id??"");
                this.SetSeoContent(model);
                if (model.Content != null && model.Layouts != null)
                    ViewBag.Layouts = model.Layouts;
                ViewBag.isHomePage = model.IsHomePage;
                return View(model);
            }
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
