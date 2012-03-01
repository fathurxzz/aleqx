using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shop.Models;

namespace Shop.Areas.Admin.Controllers
{
    public class TagController : Controller
    {
        //
        // GET: /Admin/Tag/

        public ActionResult Index()
        {
            using (var context = new ShopContainer())
            {
                var tags = context.Tag.ToList();
                return View(tags);
            }
        }

        //
        // GET: /Admin/Tag/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Admin/Tag/Create

        [HttpPost]
        public ActionResult Create(FormCollection form)
        {
            try
            {
                using (var context = new ShopContainer())
                {
                    var tag = new Tag {Name = form["Name"], Title = form["Title"]};
                    context.AddToTag(tag);
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
        // GET: /Admin/Tag/Edit/5

        public ActionResult Edit(int id)
        {
            using (var context = new ShopContainer())
            {
                var tag = context.Tag.First(t => t.Id == id);
                return View(tag);
            }
        }

        //
        // POST: /Admin/Tag/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                using (var context = new ShopContainer())
                {
                    var tag = context.Tag.First(t => t.Id == id);
                    TryUpdateModel(tag, new[] {"Name","Title"});
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Admin/Tag/Delete/5

        public ActionResult Delete(int id)
        {
            try
            {
                using (var context = new ShopContainer())
                {

                }

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }
    }
}
