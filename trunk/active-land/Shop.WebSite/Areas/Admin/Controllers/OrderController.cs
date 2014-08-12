using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shop.DataAccess.Entities;
using Shop.DataAccess.Repositories;

namespace Shop.WebSite.Areas.Admin.Controllers
{
    public class OrderController : AdminController
    {
        //
        // GET: /Admin/Order/

        public OrderController(IShopRepository repository)
            : base(repository)
        {
        }

        public ActionResult Index()
        {
            _repository.LangId = CurrentLangId;
            var orders = _repository.GetOrders();
            return View(orders);
        }


        public ActionResult Delete(int id)
        {
            _repository.LangId = CurrentLangId;
            _repository.DeleteOrder(id);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            return View(_repository.GetOrder(id));
        }

        [HttpPost]
        public ActionResult Edit(Order model, FormCollection form)
        {
            var order = _repository.GetOrder(model.Id);

            TryUpdateModel(order, new[] { "CustomerName", "CustomerPhone", "CustomerEmail", "Completed", "Info", "Subscribed", "DeliveryCity", "DeliveryStreet", "DeliveryOffice" });
            order.DeliveryMethod = form["delivery-method"] == "delivery" ? 0 : 1;
            order.PaymentMethod = form["payment-method"] == "cache" ? 0 : 1;
            _repository.SaveOrder(order);
            return RedirectToAction("Index");
        }
    }
}
