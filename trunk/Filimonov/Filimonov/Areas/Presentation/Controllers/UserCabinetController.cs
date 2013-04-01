using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Filimonov.Helpers;
using Filimonov.Models;

namespace Filimonov.Areas.Presentation.Controllers
{
    [Authorize]
    public class UserCabinetController : Controller
    {
        //
        // GET: /Presentation/UserCabinet/
        //[Authorize(Roles = "Administrators")]
        [OutputCache(NoStore = true, VaryByParam = "*", Duration = 1)]
        public ActionResult Index()
        {

            return View();
        }

        //
        // GET: /Presentation/UserCabinet/Details/5
        //[Authorize(Roles = "Administrators")]
        public ActionResult Details(string id, string layout)
        {
            if (User.Identity.Name != "admin")
            {
                if (User.Identity.Name != id)
                {
                    throw new Exception();
                }
            }

            using (var context = new LibraryContainer())
            {
                var customer = context.Customer.Include("ProductSets").First(c => c.Name == id);

                var layouts = context.Layout.ToList();
                layouts.Insert(0, new Layout { Name = "", Title = "Все" });
                ViewBag.Layouts = layouts;

                ViewBag.Layout = layout;

                return View(customer);
            }
        }

        //[Authorize(Roles = "Administrators")]
        public ActionResult AddProductSet(string id)
        {
            ViewBag.CustomerId = id;
            return View();
        }

        
        [HttpPost]
        public ActionResult AddProductSet(string customerId, FormCollection form)
        {
            using (var context = new LibraryContainer())
            {
                var customer = context.Customer.Include("ProductSets").First(c => c.Name == customerId);

                var productSet = new ProductSet();

                TryUpdateModel(productSet, new[] { "Title" });
                customer.ProductSets.Add(productSet);

                context.SaveChanges();

                return RedirectToAction("Details", new { id = customer.Name });
            }
        }

        
    }
}
