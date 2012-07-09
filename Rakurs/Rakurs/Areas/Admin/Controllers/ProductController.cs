using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rakurs.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection form)
        {
            return RedirectToAction("Index", "Catalogue");
        }

        public ActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit(FormCollection form)
        {
            return RedirectToAction("Index", "Catalogue");
        }


    }
}
