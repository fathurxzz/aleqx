using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vip.Models;

namespace Vip.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        public ActionResult Index()
        {
            using (var context = new SiteContainer())
            {
                var categories = context.Category.ToList();
                return View(categories);
            }
        }

        public ActionResult Create()
        {
            return View(new Category());
        } 

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                using (var context = new SiteContainer())
                {

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
            return View();
        }

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

        public ActionResult Delete(int id)
        {
            return View();
        }

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
