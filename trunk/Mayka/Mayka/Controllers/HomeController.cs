﻿using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Mayka.Helpers;
using Mayka.Models;
using SiteExtensions;

namespace Mayka.Controllers
{
    public class HomeController : BaseController
    {
        private readonly SiteContext _context;

        public HomeController(SiteContext context)
        {
            _context = context;
        }

        public ActionResult Index(string id)
        {
            var model = new SiteModel(_context, id ?? "");
            //ViewBag.isHomePage = model.Content.ContentType == (int)ContentType.HomePage;
            this.SetSeoContent(model);
            if (model.Content.ContentType != 1)
            {
                switch (model.Content.ContentType)
                {
                    case 2:
                        return RedirectToAction("Gallery", new {id = model.Content.Name});
                    case 3:
                        return RedirectToAction("Products", new { id = model.Content.Name });
                }
            }

            return View(model);
        }

        public ActionResult Gallery(string id)
        {
            var model = new SiteModel(_context, id);
            this.SetSeoContent(model);
            return View(model);
        }

        public ActionResult Products(string id)
        {
            var model = new SiteModel(_context, id);
            this.SetSeoContent(model);
            return View(model);
        }

        public ActionResult ProductDetails(int id)
        {
            var model = new SiteModel(_context, null, id);
            this.SetSeoContent(model);
            return View(model);
        }

       
    }
}
