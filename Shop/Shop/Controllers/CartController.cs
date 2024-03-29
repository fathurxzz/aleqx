﻿using System;
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

                var model = new SiteViewModel(context,null);
                ViewBag.MainMenu = model.MainMenu;
                model.Title = "Магазин детских игрушек Toy-Planet - Корзина";
                this.SetSeoContent(model);
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
                    var product = context.Product.Include("Category").Include("ProductImages").First(p => p.Id == id);
                    var image = product.ProductImages.Where(i => i.Default).FirstOrDefault();
                    OrderItem orderItem = new OrderItem
                                              {
                                                  Description = product.Description,
                                                  Image = (image != null) ? image.ImageSource : null,
                                                  Price = product.Price,
                                                  ProductId = product.Id,
                                                  ProductName = product.Name,
                                                  Quantity = 1,
                                                  Name = product.Title,
                                                  CategoryName = product.Category.Name
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
            using (var context = new ShopContainer())
            {
                var model = new SiteViewModel(context, null);
                ViewBag.MainMenu = model.MainMenu;
                model.Title = "Магазин детских игрушек Toy-Planet - Оформление заказа";
                this.SetSeoContent(model);
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult CheckOut(FormCollection form)
        {
            using (var context = new OrdersContainer())
            {
                Order order = new Order
                                  {
                                      DeliveryAddress = form["Order.DeliveryAddress"],
                                      Email = form["Order.Email"],
                                      Name = form["Order.Name"],
                                      OrderDate = DateTime.Now,
                                      Phone = form["Order.Phone"],
                                      Processed = false
                                  };

                foreach (var orderItem in WebSession.OrderItems.Select(o=>o.Value))
                {
                    order.OrderItems.Add(orderItem);
                }


                if (order.OrderItems.Any())
                {
                    context.AddToOrder(order);
                    context.SaveChanges();
                    WebSession.OrderItems.Clear();
                }

                using (var siteContext = new ShopContainer())
                {
                    var model = new SiteViewModel(siteContext, null);
                    ViewBag.MainMenu = model.MainMenu;
                    model.Title = "Магазин детских игрушек Toy-Planet - Ваш заказ оформлен";
                    this.SetSeoContent(model);
                    return View("ThankYou", model);
                }
            }
            
        }

        [HttpPost]
        public ActionResult Recalculate(FormCollection form)
        {
            PostData post = form.ProcessPostData("q");

            foreach (var kvp in post)
            {
                int orderItemId = Convert.ToInt32(kvp.Key);
                foreach (var q in kvp.Value)
                {
                    int orderItemQuantity = Convert.ToInt32(q.Value);

                    foreach (var orderItem in WebSession.OrderItems.Select(oi=>oi.Value))
                    {
                        if(orderItem.GetHashCode()==orderItemId)
                        {
                            orderItem.Quantity = orderItemQuantity;
                        }
                    }
                }
            }

            return RedirectToAction("Index");
        }
    }
}
