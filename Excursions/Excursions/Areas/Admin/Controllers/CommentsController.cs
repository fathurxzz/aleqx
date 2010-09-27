using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Excursions.Areas.Admin.Controllers
{
    public class CommentsController : Controller
    {
        //
        // GET: /Admin/Comments/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Admin/Comments/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Admin/Comments/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Admin/Comments/Create

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
        // GET: /Admin/Comments/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Admin/Comments/Edit/5

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
        // GET: /Admin/Comments/Delete/5
 
        public ActionResult Delete(int id)
        {
            // ReSharper disable Asp.NotResolved
            return RedirectToAction("Index", "Excursions", new { area = "" });
            // ReSharper restore Asp.NotResolved
        }

        //
        // POST: /Admin/Comments/Delete/5

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
