using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shop.Models;

namespace Shop.Areas.Admin.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        //
        // GET: /Admin/Order/

        public ActionResult Index()
        {
            using (var context = new OrdersContainer())
            {
                var orders = context.Order.Include("OrderItems").ToList().OrderBy(o => o.OrderDate);
                return View(orders);
            }
        }


        public ActionResult Details(int id)
        {
            using (var context = new OrdersContainer())
            {
                var order = context.Order.Include("OrderItems").First(o => o.Id == id);
                return View(order);
            }
        }

        [HttpPost]
        public ActionResult Details(int id, FormCollection form)
        {
            using (var context = new OrdersContainer())
            {
                var order = context.Order.Include("OrderItems").First(o => o.Id == id);
                TryUpdateModel(order, new[] { "Processed", "Info", "Name", "DeliveryAddress", "Email" });
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
