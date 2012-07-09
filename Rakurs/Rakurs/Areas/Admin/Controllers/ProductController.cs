using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rakurs.Models;

namespace Rakurs.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        
        public ActionResult Create(int id)
        {
            using (var context = new StructureContainer())
            {
                var category = context.Category.Include("ProductAttributes").First(c => c.Id == id);
                ViewBag.CategoryId = category.Id;
                return View();
            }
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
