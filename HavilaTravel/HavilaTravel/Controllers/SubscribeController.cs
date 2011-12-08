using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HavilaTravel.Helpers;
using HavilaTravel.Models;

namespace HavilaTravel.Controllers
{
    public class SubscribeController : Controller
    {
        //
        // GET: /Subscribe/


        public ActionResult Index()
        {
            using (var context = new ContentStorage())
            {
                var model = new SiteViewModel(null, context, false);
                return View(model);
            }
        }



        [HttpPost]
        public ActionResult Subscribe(FormCollection form)
        {
            using (var context = new ContentStorage())
            {
                var subscriber = new Customers();
                TryUpdateModel(subscriber, new[] { "Name", "Email", "SubscribeType" });
                context.AddToCustomers(subscriber);
                context.SaveChanges();
            }
            
            return RedirectToAction("ThankYou");
        }

        public ActionResult ThankYou()
        {
            using (var context = new ContentStorage())
            {
                var model = new SiteViewModel(null, context, false);
                return View(model);
            }
        }
    }
}
