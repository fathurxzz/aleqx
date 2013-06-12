using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ego.Models;

namespace Ego.Areas.Admin.Controllers
{
     [Authorize]
    public class OrderController : Controller
    {
        //
        // GET: /Admin/Order/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(int id)
        {
            using (var context = new SiteContainer())
            {
                var model = new OrderModel(context, id);
                return View(model);
            }
        }
    }
}
