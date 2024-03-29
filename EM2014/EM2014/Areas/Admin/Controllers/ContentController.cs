﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EM2014.Models;

namespace EM2014.Areas.Admin.Controllers
{
    public class ContentController : Controller
    {
        private readonly SiteContext _context;

        public ContentController(SiteContext context)
        {
            _context = context;
        }

        public ActionResult Edit(int id)
        {
            var content = _context.Contents.First(c => c.Id == id);
            return View(content);
        }

        [HttpPost]
        public ActionResult Edit(Content model)
        {
            var content = _context.Contents.First(c => c.Id == model.Id);
            TryUpdateModel(content, new[] { "Name", "Title", "SortOrder", "SeoDescription", "SeoKeywords" });
            content.Text = HttpUtility.HtmlDecode(model.Text);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home", new { area = "", category = content.Name });
        }

    }
}
