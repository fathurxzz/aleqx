using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Filimonov.Models;

namespace Filimonov.Areas.Presentation.Controllers
{
    [Authorize]
    public class SurveyController : Controller
    {
        //
        // GET: /Presentation/Survey/

        public ActionResult Index()
        {
            using (var context = new LibraryContainer())
            {
                var customers = context.Customer.Where(c=>c.Name!="admin").ToList();
                ViewBag.CurrentItem = "survey";
                return View(customers);
            }
        }



        //
        // GET: /Presentation/Survey/Details/5

        public ActionResult Details(string id)
        {
            using (var context = new LibraryContainer())
            {
                var customer = context.Customer.Include("Surveys").First(c => c.Name == id);
                return View(customer);
            }
        }


        public ActionResult EditSurveyData(string id)
        {
            using (var context = new LibraryContainer())
            {
                var customer = context.Customer.Include("Surveys").First(c => c.Name == id);
                return View(customer);
            }
        }

        [HttpPost]
        public ActionResult EditSurveyData(string id, FormCollection form)
        {
            using (var context = new LibraryContainer())
            {
                var customer = context.Customer.Include("Surveys").First(c => c.Name == id);
                TryUpdateModel(customer, new[] { "SurveyTitle", "SurveyDate" });
                customer.SurveyDescription = HttpUtility.HtmlDecode(form["SurveyDescription"]);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        //
        // GET: /Presentation/Survey/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Presentation/Survey/Create

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
        // GET: /Presentation/Survey/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Presentation/Survey/Edit/5

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
        // GET: /Presentation/Survey/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Presentation/Survey/Delete/5

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
