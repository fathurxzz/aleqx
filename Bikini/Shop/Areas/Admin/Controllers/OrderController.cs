using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shop.Models;

namespace Shop.Areas.Admin.Controllers
{
    public class OrderController : AdminController
    {
        public ActionResult Index()
        {
            using (var context = new ShopContainer())
            {
                var orders = context.Order.Include("OrderItems").ToList();
                return View(orders);
            }
        }


        public ActionResult Details(int id)
        {
            using (var context = new ShopContainer())
            {
                var order = context.Order.Include("OrderItems").First(o => o.Id == id);
                return View(order);
            }
        }

        [HttpPost]
        public ActionResult Details(int id, FormCollection form)
        {
            using (var context = new ShopContainer())
            {
                var order = context.Order.Include("OrderItems").First(o => o.Id == id);
                TryUpdateModel(order, new[] { "Complited", "Info", "Name", "DeliveryAddress", "Email" });
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            using (var context = new ShopContainer())
            {
                var order = context.Order.Include("OrderItems").First(o => o.Id == id);

                while (order.OrderItems.Any())
                {
                    var item = order.OrderItems.First();
                    context.DeleteObject(item);
                }

                context.DeleteObject(order);
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

    }
}
