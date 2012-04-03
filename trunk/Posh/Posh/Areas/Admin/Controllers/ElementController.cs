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
            return RedirectToAction("Index", "Home", new { Area = "" });
        }

    }
}
