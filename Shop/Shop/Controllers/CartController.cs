using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shop.Helpers;
using Shop.Models;

namespace Shop.Controllers
{
    public class CartController : Controller
    {
        //
        // GET: /Cart/

        public ActionResult Index()
        {
            if (WebSession.OrderItems.Count == 0)
                return RedirectToAction("Index", "Home", null);


            using (var context = new ShopContainer())
            {
                decimal totalAmount = WebSession.OrderItems.Sum(oi => oi.Value.Price * oi.Value.Quantity);
                ViewData["totalAmount"] = totalAmount;
                var model = new SiteViewModel(context);
                return View(model);
            }
        }

        [OutputCache(NoStore = true, VaryByParam = "*", Duration = 1)]
        public int Add(int id)
        {
            if (WebSession.OrderItems.ContainsKey(id))
                WebSession.OrderItems[id].Quantity++;
            else
            {
                using (var context = new ShopContainer())
                {
                    var product = context.Product.Include("ProductImages").First(p => p.Id == id);
                    var image = product.ProductImages.Where(i => i.Default).FirstOrDefault();
                    OrderItem orderItem = new OrderItem
                                              {
                                                  Description = product.Description,
                                                  Image = (image != null) ? image.ImageSource : null,
                                                  Price = product.Price,
                                                  ProductId = product.Id,
                                                  Quantity = 1,
                                                  Name = product.Title
                                              };

                    WebSession.OrderItems.Add(product.Id, orderItem);
                }
            }
            return WebSession.OrderItems.Sum(o=>o.Value.Quantity);
        }

        public ActionResult Delete(int id)
        {
            WebSession.OrderItems.Remove(id);
            if (WebSession.OrderItems.Count == 0)
                return RedirectToAction("Index", "Home", null);
            return RedirectToAction("Index");
        }

        public ActionResult Clear()
        {
            WebSession.OrderItems.Clear();
            return RedirectToAction("Index");
        }


        public ActionResult CheckOut()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CheckOut(FormCollection form)
        {
            using (var context = new OrdersContainer())
            {
                Order order = new Order
                                  {
                                      DeliveryAddress = form["DeliveryAddress"],
                                      Email = form["Email"],
                                      Name = form["Name"],
                                      OrderDate = DateTime.Now,
                                      Phone = form["Phone"],
                                      Processed = false
                                  };

                foreach (var orderItem in WebSession.OrderItems.Select(o=>o.Value))
                {
                    order.OrderItems.Add(orderItem);
                }

                context.AddToOrder(order);
                context.SaveChanges();
            }
            return View("ThankYou");
        }
    }
}
