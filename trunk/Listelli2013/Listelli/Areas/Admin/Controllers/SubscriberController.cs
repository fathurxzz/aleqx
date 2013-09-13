using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Listelli.Models;

namespace Listelli.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrators")]
    public class SubscriberController : Controller
    {
        //
        // GET: /Admin/Subscriber/

        public ActionResult Index()
        {
            using (var context = new CustomerContainer())
            {
                var subscribers = context.Subscriber.ToList();
                return View(subscribers);
            }
        }

        public ActionResult Edit(int id)
        {
            using (var context = new CustomerContainer())
            {
                var subscriber = context.Subscriber.First(s => s.Id == id);
                return View(subscriber);
            }
        }
        
        [HttpPost]
        public ActionResult Edit(Subscriber model)
        {
            using (var context = new CustomerContainer())
            {
                var subscriber = context.Subscriber.First(s => s.Id == model.Id);
                TryUpdateModel(subscriber, new[] {"Active","Email"});
                context.SaveChanges();
                return RedirectToAction("Index");
            }
        }


    }
}
