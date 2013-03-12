using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Filimonov.Models;

namespace Filimonov.Areas.Presentation.Controllers
{
    public class LayoutController : Controller
    {
        //
        // GET: /Presentation/Layout/

        public ActionResult Index()
        {
            using (var context = new LibraryContainer())
            {
                var layouts = context.Layout.ToList();
                return View(layouts);
            }
        }

        //
        // GET: /Presentation/Layout/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Presentation/Layout/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                using (var context = new LibraryContainer())
                {
                    var layout = new Layout();
                    TryUpdateModel(layout, new[] { "Name", "Title" });
                    context.AddToLayout(layout);
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
        // GET: /Presentation/Layout/Edit/5

        public ActionResult Edit(int id)
        {
            using (var context = new LibraryContainer())
            {
                var layout = context.Layout.First(l => l.Id == id);
                return View(layout);
            }
        }

        //
        // POST: /Presentation/Layout/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                using (var context = new LibraryContainer())
                {
                    var layout = context.Layout.First(l => l.Id == id);
                    TryUpdateModel(layout, new[] { "Name", "Title" });
                    context.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id, FormCollection collection)
        {
            using (var context = new LibraryContainer())
            {
                var layout = context.Layout.First(l => l.Id == id);
                context.DeleteObject(layout);
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
