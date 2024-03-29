﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EM2013.Models;

namespace EM2013.Areas.Admin.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        public ActionResult Create()
        {
            using (var context = new SiteContext())
            {
                int max=0;
                if(context.Category.Any())
                max = context.Category.Max(c => c.SortOrder);
                return View(new Category {SortOrder = max + 1});
            }
        }

        [HttpPost]
        public ActionResult Create(FormCollection form)
        {
            using (var context = new SiteContext())
            {
                var category = new Category();
                TryUpdateModel(category, new[] { "Name", "Title", "TitleToCategory", "SeoDescription", "SeoKeywords", "SortOrder" });
                category.Description = HttpUtility.HtmlDecode(form["Description"]);
                context.AddToCategory(category);
                context.SaveChanges();
                return RedirectToAction("Index", "Home", new { area = "", category = category.Name });
            }
        }

        public ActionResult Edit(int id)
        {
            using (var context = new SiteContext())
            {
                var category = context.Category.First(c => c.Id == id);
                return View(category);
            }
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection form)
        {
            using (var context = new SiteContext())
            {
                var category = context.Category.First(c => c.Id == id);
                TryUpdateModel(category, new[] { "Name", "Title", "TitleToCategory", "SeoDescription", "SeoKeywords", "SortOrder" });
                category.Description = HttpUtility.HtmlDecode(form["Description"]);
                context.SaveChanges();
                return RedirectToAction("Index", "Home", new { area = "", category = category.Name });
            }
        }

        public ActionResult Delete(int id)
        {
            using (var context = new SiteContext())
            {
                var category = context.Category.Include("Products").First(c => c.Id == id);
                if (!category.Products.Any())
                {
                    context.DeleteObject(category);
                    context.SaveChanges();
                }

                return RedirectToAction("Index", "Home", new { area = "" });
            }
        }
    }
}
