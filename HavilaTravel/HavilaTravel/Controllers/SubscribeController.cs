using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HavilaTravel.Models;

namespace HavilaTravel.Controllers
{
    public class SubscribeController : Controller
    {
        //
        // GET: /Subscribe/

        public ActionResult Index()
        {
            return View();
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
            return View("ThankYou");
        }
    }
}
