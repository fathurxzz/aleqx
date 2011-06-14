﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Klafs.Models;

namespace Klafs.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        public ActionResult Index(string id)
        {
            using (var context = new ContentStorage())
            {
                var contentItems = context.Content.Where(c => c.Visible).OrderBy(c=>c.SortOrder).ToList();
                ViewData["contentItems"] = contentItems;
                Content content;
                if (id == null)
                    content = context.Content.Where(c => c.Id == 1).FirstOrDefault();
                else
                {
                    content = context.Content.Where(c => c.Name == id).FirstOrDefault();
                }
                return View(content);
            }
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
