using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Listelli.Areas.FactoryCatalogue.Controllers
{
    public class BrandController : Controller
    {
        //
        // GET: /FactoryCatalogue/Brand/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /FactoryCatalogue/Brand/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /FactoryCatalogue/Brand/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /FactoryCatalogue/Brand/Create

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
        // GET: /FactoryCatalogue/Brand/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /FactoryCatalogue/Brand/Edit/5

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
        // GET: /FactoryCatalogue/Brand/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /FactoryCatalogue/Brand/Delete/5

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
