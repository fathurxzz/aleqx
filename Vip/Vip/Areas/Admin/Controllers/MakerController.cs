using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vip.Models;

namespace Vip.Areas.Admin.Controllers
{
    [Authorize]
    public class MakerController : Controller
    {
        public ActionResult Index()
        {
            using (var context = new SiteContainer())
            {
                var makers = context.Maker.ToList();
                return View(makers);
            }
        }

        public ActionResult Create()
        {
            return View(new Maker());
        } 

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                using (var context = new SiteContainer())
                {
                    var maker = new Maker();
                    TryUpdateModel(maker, new[] { "Title" });
                    context.AddToMaker(maker);
                    context.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        public ActionResult Edit(int id)
        {
            using (var context = new SiteContainer())
            {
                var maker = context.Maker.First(m => m.Id == id);
                return View(maker);
            }
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                using (var context = new SiteContainer())
                {
                    var maker = context.Maker.First(m => m.Id == id);
                    TryUpdateModel(maker, new[] { "Title" });
                    context.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            using (var context = new SiteContainer())
            {
                var maker = context.Maker.First(m => m.Id == id);
                context.DeleteObject(maker);
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
