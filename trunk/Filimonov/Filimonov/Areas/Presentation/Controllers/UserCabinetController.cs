using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Filimonov.Models;

namespace Filimonov.Areas.Presentation.Controllers
{
    [Authorize]
    public class UserCabinetController : Controller
    {
        //
        // GET: /Presentation/UserCabinet/
        [Authorize(Roles = "Administrators")]
        public ActionResult Index()
        {

            return View();
        }

        //
        // GET: /Presentation/UserCabinet/Details/5

        public ActionResult Details(string id, string layout)
        {
            if (User.Identity.Name != "admin")
            {
                if (User.Identity.Name != id)
                {
                    throw new Exception();
                }
            }

            using (var context = new LibraryContainer())
            {
                var customer = context.Customer.Include("ProductContainers").First(c => c.Name == id);

                var layouts = context.Layout.ToList();
                layouts.Insert(0, new Layout { Name = "", Title = "Все" });
                ViewBag.Layouts = layouts;

                ViewBag.Layout = layout;

                return View(customer);
            }
        }

        //
        // GET: /Presentation/UserCabinet/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Presentation/UserCabinet/Create

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
        // GET: /Presentation/UserCabinet/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Presentation/UserCabinet/Edit/5

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
        // GET: /Presentation/UserCabinet/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Presentation/UserCabinet/Delete/5

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
