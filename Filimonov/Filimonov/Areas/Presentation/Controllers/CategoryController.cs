using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Filimonov.Models;

namespace Filimonov.Areas.Presentation.Controllers
{
    public class CategoryController : Controller
    {
        //
        // GET: /Presentation/Category/

        public ActionResult Index()
        {
            using (var context = new LibraryContainer())
            {
                var categories = context.Category.ToList();
                return View(categories);
            }
        }

        //
        // GET: /Presentation/Category/Details/5

        public ActionResult Details(string id)
        {
            using (var context = new LibraryContainer())
            {

                return View();
            }
        }

        //
        // GET: /Presentation/Category/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Presentation/Category/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                using (var context = new LibraryContainer())
                {
                    var category = new Category(){ImageSource = ""};
                    TryUpdateModel(category, new[] {"Name", "Title"});

                    context.AddToCategory(category);
                    context.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /Presentation/Category/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Presentation/Category/Edit/5

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
        // GET: /Presentation/Category/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Presentation/Category/Delete/5

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
