﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Filimonov.Models;
using SiteExtensions;

namespace Filimonov.Controllers
{
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

        public ActionResult Gallery()
        {
            using (var context = new SiteContainer())
            {
                return View();
            }
        }
    }
}
