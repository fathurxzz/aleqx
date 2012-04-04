using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Posh.Helpers;
using Posh.Models;

namespace Posh.Areas.Admin.Controllers
{
    public class ElementController : Controller
    {
        //
        // GET: /Admin/Element/

        public ActionResult Create()
        {
            return View(new Element());
        }

        [HttpPost]
        public ActionResult Create(FormCollection form)
        {
            using (var context = new ModelContainer())
            {
                var element = new Element();
                TryUpdateModel(element, new[] { "Title", "SortOrder" });
                context.AddToElement(element);
                context.SaveChanges();
            }
            return RedirectToAction("Index", "Home", new { Area = "", id = "" });
        }

        public ActionResult Edit(int id)
        {
            using (var context = new ModelContainer())
            {
                var element = context.Element.First(e => e.Id == id);
                return View(element);
            }
        }

        [HttpPost]
        public ActionResult Edit(Element model)
        {
            using (var context = new ModelContainer())
            {
                var element = context.Element.First(e => e.Id == model.Id);
                TryUpdateModel(element, new[] { "Title", "SortOrder" });
                context.SaveChanges();
            }
            return RedirectToAction("Index", "Home", new { Area = "", id="" });
        }

        public ActionResult Delete(int id)
        {
            using (var context = new ModelContainer())
            {
                var element = context.Element.First(e => e.Id == id);
                element.Products.Clear();
                context.DeleteObject(element);
                context.SaveChanges();
            }
            return RedirectToAction("Index", "Home", new { Area = "", id = "" });
        }

    }
}
