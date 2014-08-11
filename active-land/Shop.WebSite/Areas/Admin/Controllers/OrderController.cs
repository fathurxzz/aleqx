using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shop.DataAccess.Repositories;

namespace Shop.WebSite.Areas.Admin.Controllers
{
    public class OrderController : AdminController
    {
        //
        // GET: /Admin/Order/

        public OrderController(IShopRepository repository) : base(repository)
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
    }
}
