using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shop.DataAccess.Entities;
using Shop.DataAccess.Repositories;
using Shop.WebSite.Helpers;
using Shop.WebSite.Models;

namespace Shop.WebSite.Controllers
{
    public class CartController : DefaultController
    {

        public CartController(IShopRepository repository)
            : base(repository)
        {

        }

        public ActionResult Index()
        {
            if (WebSession.OrderItems.Count == 0)
                return RedirectToAction("Catalogue", "Home", null);

            decimal totalAmount = WebSession.OrderItems.Sum(oi => oi.Value.Price * oi.Value.Quantity);
            ViewBag.TotalAmount = totalAmount;
            var model = new SiteModel(_repository, CurrentLangId, null) { CurrentLangCode = CurrentLangCode };
            this.SetSeoContent(model);
            return View(model);
        }

        [OutputCache(NoStore = true, VaryByParam = "*", Duration = 1)]
        public int Add(int id)
        {

            if (WebSession.OrderItems.ContainsKey(id))
            {
                WebSession.OrderItems[id].Quantity++;
            }
            else
            {
                _repository.LangId = CurrentLangId;
                var product = _repository.GetProduct(id);
                var orderItem = new OrderItem
                {
                    CategoryName = product.Category.Name,
                    Description = product.Description,
                    ImageSource = product.ImageSource,
                    Price = product.Price,
                    ProductName = product.Name,
                    Quantity = 1,
                    ProductTitle = product.Title
                };

                orderItem.ProductStocks = new List<ProductStock>();
                foreach (var productStock in product.ProductStocks)
                {
                    orderItem.ProductStocks.Add(productStock);
                }

                WebSession.OrderItems.Add(product.Id, orderItem);
            }
            return WebSession.OrderItems.Sum(o => o.Value.Quantity);
        }

        [OutputCache(NoStore = true, VaryByParam = "*", Duration = 1)]
        public void Update(int id, int quantity)
        {
            if (WebSession.OrderItems.ContainsKey(id))
            {
                WebSession.OrderItems[id].Quantity = quantity;
            }
            //return WebSession.OrderItems.Sum(o => o.Value.Quantity);
        }

        [OutputCache(NoStore = true, VaryByParam = "*", Duration = 1)]
        public void UpdateStock(int id, string stock, string size, string color)
        {
            if (WebSession.OrderItems.ContainsKey(id))
            {
                WebSession.OrderItems[id].ProductStockNumber = stock;
                WebSession.OrderItems[id].ProductSize = size;
                WebSession.OrderItems[id].ProductColor = color;
            }
            //return WebSession.OrderItems.Sum(o => o.Value.Quantity);
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
            var model = new SiteModel(_repository, CurrentLangId, null);
            this.SetSeoContent(model);
            return View(model);
        }

        [HttpPost]
        public ActionResult CheckOut(FormCollection form)
        {

            var order = new Order
                {
                    CustomerName = form["customerName"],
                    CustomerPhone = form["mobilePhone"],
                    Date = DateTime.Now,
                    CustomerEmail = form["email"],
                    Subscribed = form["subscribe-radio"] == "ok",
                    Completed = false,
                    // 0 - Доставка 
                    // 1 - Самовывоз
                    DeliveryMethod = form["delivery-method"] == "delivery" ? 0 : 1,
                    DeliveryCity = form["city"],
                    DeliveryStreet = form["street"],
                    DeliveryOffice = form["office"],
                    // 0 - Наличными
                    // 1 - Оплата карточкой
                    PaymentMethod = form["payment-method"] == "cache" ? 0 : 1,
                };

            foreach (var orderItem in WebSession.OrderItems.Select(o => o.Value))
            {
                foreach (var productStock in orderItem.ProductStocks.Where(ps=>ps.StockNumber==form["stock"]))
                {
                    orderItem.ProductStockNumber = productStock.StockNumber;
                    orderItem.ProductColor = productStock.Color;
                    orderItem.ProductSize = productStock.Size;
                }

                order.OrderItems.Add(orderItem);
            }

            if (order.OrderItems.Any())
            {
                WebSession.Order = order;
                
                //WebSession.OrderItems.Clear();
            }

            return RedirectToAction("CheckOutConfirm");
        }

        public ActionResult CheckOutConfirm()
        {
            var model = new CartModel(_repository,CurrentLangId, null)
            {
                Order = WebSession.Order
            };
            this.SetSeoContent(model);
            return View(model);
        }

        public ActionResult ThankYou()
        {
            var model = new CartModel(_repository, CurrentLangId, null);
            try
            {
                var order = WebSession.Order;
                var amount = order.OrderItems.Sum(oi => oi.Quantity * oi.Price);
                var number = _repository.AddOrder(order);
                WebSession.OrderItems.Clear();
                model.OrderComplete = new OrderComplete
                {
                    Amount = amount,
                    Number = number.ToString(),
                    CustomerName = order.CustomerName
                };

                MailHelper.Notify(order, number);

                return View(model);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                
            }
            return RedirectToAction("CheckOut");
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
