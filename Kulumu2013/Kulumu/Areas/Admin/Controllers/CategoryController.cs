﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kulumu.Models;

namespace Kulumu.Areas.Admin.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        public ActionResult Create(int? id)
        {
            if (id.HasValue)
                ViewBag.ParentId = id.Value;
            return View(new Category());
        }

        [HttpPost]
        public ActionResult Create(int? parentId, FormCollection collection)
        {
            using (var context = new SiteContainer())
            {
                var category = new Category();
                TryUpdateModel(category, new[]
                                                 {
                                                     "Name",
                                                     "Title", 
                                                     "Description", 
                                                     "BottomDescription", 
                                                     "BottomDescriptionTitle",
                                                     "ShowOnMainPage",
                                                     "PageTitle"
                                                 });
                if (parentId.HasValue)
                {
                    var parent = context.Category.First(c => c.Id == parentId);
                    parent.Children.Add(category);
                }
                else
                {
                    context.AddToCategory(category);
                }
                context.SaveChanges();
                return RedirectToAction("Gallery", "Home", new { area = "", id = category.Name });
            }
        }

        public ActionResult Edit(int id)
        {
            using (var context = new SiteContainer())
            {
                var category = context.Category.First(c => c.Id == id);
                return View(category);
            }
        }


        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                using (var context = new SiteContainer())
                {
                    var category = context.Category.First(c => c.Id == id);
                    TryUpdateModel(category, new[]
                                                 {
                                                     "Name",
                                                     "Title", 
                                                     "Description", 
                                                     "BottomDescription", 
                                                     "BottomDescriptionTitle",
                                                     "ShowOnMainPage",
                                                     "PageTitle"
                                                 });
                    context.SaveChanges();
                    return RedirectToAction("Gallery", "Home", new { area = "", id = category.Name });
                }
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            using (var context = new SiteContainer())
            {
                var category = context.Category.Include("Children").Include("Products").First(c => c.Id == id);
                if (!category.Products.Any())
                {
                    context.DeleteObject(category);
                    context.SaveChanges();
                }
            }
            return RedirectToAction("Gallery", "Home", new { area = "" });
        }

        
    }
}
