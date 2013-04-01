using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Filimonov.Helpers;
using Filimonov.Models;

namespace Filimonov.Areas.Presentation.Controllers
{
    [Authorize]
    [OutputCache(NoStore = true, VaryByParam = "*", Duration = 1)]
    public class CategoryController : Controller
    {
        //
        // GET: /Presentation/Category/

        public ActionResult Index()
        {
            using (var context = new LibraryContainer())
            {
                var categories = context.Category.ToList();
                
                ViewBag.CurrentItem = "picture-lib";
                return View(categories);
            }
        }

        //
        // GET: /Presentation/Category/Details/5

        public ActionResult Details(string id, string layout)
        {
            using (var context = new LibraryContainer())
            {
                
                var layouts = context.Layout.ToList();
                layouts.Insert(0,new Layout{Name = "",Title = "Все"});
                ViewBag.Layouts = layouts;

                ViewBag.Layout = layout;

                var category = context.Category.Include("Products").First(c => c.Name == id);

                //category.Products.Load();

                //IEnumerable<Product> products = category.Products.Where(p => p.Layout.Name == layout);

                //category.Products.Clear();

                //foreach (var product in products)
                //{
                //    category.Products.Add(product);
                //}

                var client = context.Customer.Include("ProductSets").First(c => c.Name == User.Identity.Name);

                ViewBag.ProductSets = client.ProductSets.ToList();

                //ViewBag.ProductContainers = client.ProductContainers.Select(pc => new SelectListItem { Text = pc.Title, Value = pc.Id.ToString() }).ToList();

                return View(category);
            }
        }

        //
        // GET: /Presentation/Category/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Presentation/Category/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                using (var context = new LibraryContainer())
                {
                    var category = new Category{ImageSource = ""};
                    TryUpdateModel(category, new[] {"Name", "Title"});

                    context.AddToCategory(category);
                    context.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /Presentation/Category/Edit/5
 
        public ActionResult Edit(int id)
        {
            using (var context = new LibraryContainer())
            {
                var category = context.Category.First(c => c.Id == id);
                return View(category);
            }
        }

        //
        // POST: /Presentation/Category/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                using (var context = new LibraryContainer())
                {
                    var category = context.Category.First(c => c.Id == id);
                    TryUpdateModel(category, new[] {"Name", "Title"});
                    context.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return View();
            }
        }

       
        public ActionResult Delete(int id)
        {
            return View();
        }

      
    }
}
