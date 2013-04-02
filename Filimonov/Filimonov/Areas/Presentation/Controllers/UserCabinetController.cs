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
        public ActionResult Details(string id, string layout,  FormCollection form, string set)
        {
            if (User.Identity.Name != "admin")
            {
                if (User.Identity.Name != id)
                {
                    throw new Exception();
                }
            }

            var productSet = set;

            using (var context = new LibraryContainer())
            {
                var customer = context.Customer.Include("ProductSets").First(c => c.Name == id);

                foreach (var set1 in customer.ProductSets)
                {
                    set1.Products.Load();
                }

                var layouts = context.Layout.ToList();
                layouts.Insert(0, new Layout { Name = "", Title = "Все" });
                ViewBag.Layouts = layouts;

                ViewBag.Layout = layout;


                if (string.IsNullOrEmpty(productSet))
                {
                    var pset = customer.ProductSets.OrderBy(ps => ps.Title).FirstOrDefault();
                    if (pset != null)
                    {
                        productSet = pset.Id.ToString();
                    }
                }


                ViewBag.ProductSetId = productSet;


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

                return RedirectToAction("Index", "Customer");
            }
        }

        
        public ActionResult EditProductSet(int id)
        {
            using (var context = new LibraryContainer())
            {
                var productSet = context.ProductSet.Include("Customer").First(ps => ps.Id == id);
                ViewBag.CustomerId = productSet.Customer.Name;
                return View(productSet);
            }
        }

        [HttpPost]
        public ActionResult EditProductSet(int id, FormCollection form)
        {
            using (var context = new LibraryContainer())
            {
                var productSet = context.ProductSet.Include("Customer").First(ps => ps.Id == id);
                TryUpdateModel(productSet, new[] { "Title" });
                context.SaveChanges();
                //return RedirectToAction("Details", new { id = productSet.Customer.Name });
                return RedirectToAction("Index", "Customer");
            }
        }

        public ActionResult DeleteProductSet(int id)
        {
            using (var context = new LibraryContainer())
            {
                var productSet = context.ProductSet.Include("Products").Include("Customer").First(ps => ps.Id == id);
                var customerName = productSet.Customer.Name;
                ViewBag.CustomerId = productSet.Customer.Name;
                productSet.Products.Clear();
                context.DeleteObject(productSet);
                context.SaveChanges();
                return RedirectToAction("Index", "Customer");
            }
        }

    }
}
