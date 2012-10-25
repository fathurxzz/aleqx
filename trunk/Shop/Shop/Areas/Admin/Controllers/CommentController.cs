using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shop.Models;

namespace Shop.Areas.Admin.Controllers
{
     [Authorize]
    public class CommentController : Controller
    {
        public ActionResult Create(int id)
        {
            ViewBag.ParentId = id;
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection, int parentId)
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

        public ActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
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

        public ActionResult Delete(int id)
        {
            using (var context = new ShopContainer())
            {
                var comment = context.Comment.First(c => c.Id == id);
                context.DeleteObject(comment);
                context.SaveChanges();
                return RedirectToAction("Index", "Home", new { area = "", id = "about" });
            }
        }
    }
}
