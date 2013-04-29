using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Listelli.Controllers;
using Listelli.Models;

namespace Listelli.Areas.Admin.Controllers
{
    public class ContentController : AdminController
    {
        public ActionResult Edit(string id)
        {
            using (var context = new SiteContainer())
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Edit(FormCollection form)
        {
            //using (var context = new SiteContainer())
            //{
            //    var content = new Content();
            //    TryUpdateModel(content, new[] { "Title", "Text" });
            //    context.AddToContent(content);
            //    context.SaveChanges();
            //}

            return RedirectToAction("Index", "Home");
        }
    }
}
