using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vip.Models;

namespace Vip.Areas.Admin.Controllers
{
    [Authorize]
    public class ProjectController : Controller
    {
        //
        // GET: /Admin/Project/Create

        public ActionResult Create()
        {
            return View(new Project{SortOrder = 0});
        } 

        //
        // POST: /Admin/Project/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /Admin/Project/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Admin/Project/Edit/5

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
        // GET: /Admin/Project/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }
    }
}
