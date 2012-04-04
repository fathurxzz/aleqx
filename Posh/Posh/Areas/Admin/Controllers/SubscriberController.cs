using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Posh.Models;

namespace Posh.Areas.Admin.Controllers
{
    public class SubscriberController : Controller
    {
        //
        // GET: /Admin/Subscriber/

        public ActionResult Index()
        {
            using (var context = new ModelContainer())
            {
                var subscribers = context.Subscriber.ToList();
                return View(subscribers);
            }
        }

        public ActionResult Create()
        {
            return View(new Subscriber());
        }

        [HttpPost]
        public ActionResult Create(FormCollection form)
        {
            using (var context = new ModelContainer())
            {
                var subscriber = new Subscriber();
                TryUpdateModel(subscriber, new[] { "Name", "Email" });
                context.AddToSubscriber(subscriber);
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            using (var context = new ModelContainer())
            {
                var subscriber = context.Subscriber.First(s => s.Id == id);
                return View(subscriber);
            }
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection form)
        {
            using (var context = new ModelContainer())
            {
                var subscriber = context.Subscriber.First(s => s.Id == id);
                TryUpdateModel(subscriber, new[] {"Name", "Email"});
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }


        public ActionResult Delete(int id)
        {
            using (var context = new ModelContainer())
            {
                var subscriber = context.Subscriber.First(s => s.Id == id);
                context.DeleteObject(subscriber);
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }




    }
}
