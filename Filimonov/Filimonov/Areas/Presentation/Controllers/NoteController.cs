using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Filimonov.Models;

namespace Filimonov.Areas.Presentation.Controllers
{
    public class NoteController : Controller
    {
        //
        // GET: /Presentation/Note/

        public ActionResult Index()
        {
            using (var context = new LibraryContainer())
            {
                var notes = context.Note.ToList();
                ViewBag.CurrentItem = "note-details";
                return View(notes);
            }
        }

        //
        // GET: /Presentation/Note/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Presentation/Note/Create

        public ActionResult Create()
        {
            return View(new Note(){Date = DateTime.Now});
        }

        //
        // POST: /Presentation/Note/Create

        [HttpPost]
        public ActionResult Create(FormCollection form)
        {
            try
            {
                using (var context = new LibraryContainer())
                {
                    var note = new Note();
                    TryUpdateModel(note, new[] { "Title", "Date" });
                    note.Text = HttpUtility.HtmlDecode(form["Text"]);
                    context.AddToNote(note);
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
        // GET: /Presentation/Note/Edit/5

        public ActionResult Edit(int id)
        {
            using (var context = new LibraryContainer())
            {
                var note = context.Note.First(n => n.Id == id);
                return View(note);
            }
        }

        //
        // POST: /Presentation/Note/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection form)
        {
            try
            {
                using (var context = new LibraryContainer())
                {
                    var note = context.Note.First(n => n.Id == id);
                    TryUpdateModel(note, new[] { "Title", "Date" });
                    note.Text = HttpUtility.HtmlDecode(form["Text"]);
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
        // GET: /Presentation/Note/Delete/5

        public ActionResult Delete(int id)
        {
            using (var context = new LibraryContainer())
            {
                var note = context.Note.First(n => n.Id == id);
                context.DeleteObject(note);
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
