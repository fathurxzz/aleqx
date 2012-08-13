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
        //
        // GET: /Admin/Layout/

        public ActionResult Index()
        {
            using (var context = new SiteContainer())
            {
                var layout = context.Layout.Include("Children").ToList();
                return View(layout);
            }
        }

        //
        // GET: /Admin/Layout/Create

        public ActionResult Create(int? parentId)
        {
            if (parentId.HasValue)
                ViewBag.ParentId = parentId.Value;
            return View(new Layout());
        } 

        //
        // POST: /Admin/Layout/Create

        [HttpPost]
        public ActionResult Create(int? parentId, FormCollection form)
        {
            try
            {
                using (var context = new SiteContainer())
                {
                    var layout = new Layout();
                    TryUpdateModel(layout, new[] { "Title" });
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
        
        //
        // GET: /Admin/Layout/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Admin/Layout/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Admin/Layout/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Admin/Layout/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
