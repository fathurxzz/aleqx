using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Listelli.Controllers;
using Listelli.Models;
using SiteExtensions;

namespace Listelli.Areas.FactoryCatalogue.Controllers
{
    public class CategoryController : DefaultController
    {
        //
        // GET: /FactoryCatalogue/Category/

        public ActionResult Index()
        {
            using (var context = new SiteContainer())
            {
                var model = new FactoryCatalogueModel(CurrentLang, context, null);
                this.SetSeoContent(model);
                ViewBag.CurrentMenuItem = model.Content.Name;
                return View(model);
            }
        }

        //
        // GET: /FactoryCatalogue/Category/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /FactoryCatalogue/Category/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /FactoryCatalogue/Category/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /FactoryCatalogue/Category/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /FactoryCatalogue/Category/Edit/5

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
        // GET: /FactoryCatalogue/Category/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /FactoryCatalogue/Category/Delete/5

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
