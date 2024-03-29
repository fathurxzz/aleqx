﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Shop.Helpers;
using Shop.Models;
using SiteExtensions;
using TextHelper = Shop.Helpers.TextHelper;

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

                var model = new SiteModel(context, null);
                ViewBag.MainMenu = model.Menu;
                ViewBag.CatalogueMenu = model.CatalogueMenu;
                model.Title += " Корзина";
                this.SetSeoContent(model);
                return View(model);
            }
        }

        [OutputCache(NoStore = true, VaryByParam = "*", Duration = 1)]
        public string Add(int id)
        {

            const string prefix = "В корзине ";

            if (WebSession.OrderItems.ContainsKey(id))
                WebSession.OrderItems[id].Quantity++;
            else
            {
                using (var context = new ShopContainer())
                {
                    var product = context.Product.Include("Category").First(p => p.Id == id);
                    OrderItem orderItem = new OrderItem
                    {
                        Description = product.Description,
                        ImageSource = product.ImageSource,
                        Price = product.Price,
                        ProductId = product.Id,
                        ProductName = product.Name,
                        Quantity = 1,
                        ProductTitle = product.Title,
                        CategoryName = product.Category.Name,
                        CategoryTitle = product.Category.Title
                    };

                    WebSession.OrderItems.Add(product.Id, orderItem);
                }
            }

            int quantity = WebSession.OrderItems.Sum(o => o.Value.Quantity);

            return prefix+" " + quantity +" "+ TextHelper.GetQuantitySufix(quantity);
            // WebSession.OrderItems.Sum(o => o.Value.Quantity);
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
                var model = new SiteModel(context, null);
                ViewBag.MainMenu = model.Menu;
                ViewBag.CatalogueMenu = model.CatalogueMenu;
                model.Title += " - Оформление заказа";
                this.SetSeoContent(model);
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult CheckOut(FormCollection form)
        {
            using (var context = new ShopContainer())
            {
                Order order = new Order
                {
                    DeliveryAddress = form["Order.DeliveryAddress"],
                    Email = form["Order.Email"],
                    Name = form["Order.Name"],
                    OrderDate = DateTime.Now,
                    Phone = form["Order.Phone"],
                    Complited = false
                };

                foreach (var orderItem in WebSession.OrderItems.Select(o => o.Value))
                {
                    order.OrderItems.Add(orderItem);
                }


                if (order.OrderItems.Any())
                {
                    context.AddToOrder(order);
                    context.SaveChanges();
                    SendEmail(order);
                    WebSession.OrderItems.Clear();
                }

                using (var siteContext = new ShopContainer())
                {
                    var model = new SiteModel(siteContext, null);
                    ViewBag.MainMenu = model.Menu;
                    ViewBag.CatalogueMenu = model.CatalogueMenu;
                    model.Title += " - Ваш заказ оформлен";
                    this.SetSeoContent(model);
                    return View("ThankYou", model);
                }
            }

        }


        private void SendEmail(Order order)
        {
            string defaultMailAddressFrom = ConfigurationManager.AppSettings["feedbackEmailFrom"];
            string defaultMailAddresses = ConfigurationManager.AppSettings["feedbackEmailsTo"];
            var emailFrom = new MailAddress(defaultMailAddressFrom, "BIKINI-OPTOM");
            var emailsTo = defaultMailAddresses
                .Split(new[] { ";", " ", "," }, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => new MailAddress(s))
                .ToList();

            Helpers.MailHelper.SendTemplate(emailFrom, emailsTo, "Новый заказ", "OrderTemplate.htm", null, true, order.Name, order.Phone);
        }

        [HttpPost]
        public ActionResult Recalculate(FormCollection form)
        {
            var post = form.ProcessPostData("q");

            foreach (var kvp in post)
            {
                int orderItemId = Convert.ToInt32(kvp.Key);
                foreach (var q in kvp.Value)
                {
                    int orderItemQuantity = Convert.ToInt32(q.Value);

                    foreach (var orderItem in WebSession.OrderItems.Select(oi => oi.Value))
                    {
                        if (orderItem.GetHashCode() == orderItemId)
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
