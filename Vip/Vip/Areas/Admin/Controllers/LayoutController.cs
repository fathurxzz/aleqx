using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vip.Models;

namespace Vip.Areas.Admin.Controllers
{
    public class LayoutController : Controller
    {
        public ActionResult Index()
        {
            using (var context = new SiteContainer())
            {
                var layout = context.Layout.Include("Children").ToList();
                return View(layout);
            }
        }

        public ActionResult Create(int? parentId)
        {
            if (parentId.HasValue)
                ViewBag.ParentId = parentId.Value;
            return View(new Layout());
        }

        [HttpPost]
        public ActionResult Create(int? parentId, FormCollection form)
        {
            try
            {
                using (var context = new SiteContainer())
                {
                    var layout = new Layout();

                    if (parentId.HasValue)
                    {
                        var parent = context.Layout.First(l => l.Id == parentId);
                        layout.Parent = parent;
                    }

                    TryUpdateModel(layout, new[] { "Title", "SortOrder" });
                    context.AddToLayout(layout);
                    context.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            using (var context = new SiteContainer())
            {
                var layout = context.Layout.First(l => l.Id == id);
                return View(layout);
            }
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                using (var context = new SiteContainer())
                {
                    var layout = context.Layout.First(l => l.Id == id);
                    TryUpdateModel(layout, new[] { "Title", "SortOrder" });
                    context.SaveChanges();
                }

                return RedirectToAction("Index");
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
                var layout = context.Layout.Include("Children").First(l => l.Id == id);
                if (layout.Children.Any())
                {
                    var l = layout.Children.First();
                    context.DeleteObject(l);
                }
                context.DeleteObject(layout);
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
