using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Filimonov.Helpers;
using Filimonov.Models;
using SiteExtensions;

namespace Filimonov.Areas.Presentation.Controllers
{
    [Authorize(Roles = "Administrators")]
    [OutputCache(NoStore = true, VaryByParam = "*", Duration = 1)]
    public class ProductController : Controller
    {

        //
        // GET: /Presentation/Product/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Presentation/Product/Create

        public ActionResult Create(int id)
        {
            using (var context = new LibraryContainer())
            {
                var categoryName = context.Category.First(c => c.Id == id).Name;
                ViewBag.CategoryName = categoryName;
                ViewBag.CategoryId = id;

                var layouts = context.Layout.ToList();
                List<SelectListItem> layoutItems = layouts.Select(layout => new SelectListItem { Text = layout.Title, Value = layout.Id.ToString() }).ToList();
                ViewBag.Layouts = layoutItems;

                return View();
            }
        }

        //
        // POST: /Presentation/Product/Create

        [HttpPost]
        public ActionResult Create(int categoryId, int layoutId, FormCollection collection)
        {
            try
            {
                using (var context = new LibraryContainer())
                {
                    var category = context.Category.First(c => c.Id == categoryId);
                    var layout = context.Layout.First(l => l.Id == layoutId);

                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        var file = Request.Files[i];
                        if (file == null) continue;

                        var p = new Product { Layout = layout };
                        string fileName = IOHelper.GetUniqueFileName("~/Content/Images", file.FileName);
                        string filePath = Server.MapPath("~/Content/Images");
                        filePath = Path.Combine(filePath, fileName);
                        file.SaveAs(filePath);
                        p.ImageSource = fileName;
                        category.Products.Add(p);
                        context.SaveChanges();
                    }

                    return RedirectToAction("Details", "Category", new { area = "Presentation", id = category.Name });
                }
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Presentation/Product/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Presentation/Product/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Presentation/Product/Delete/5

        public ActionResult Delete(int id)
        {
            using (var context = new LibraryContainer())
            {
                var product = context.Product.Include("Category").First(p => p.Id == id);
                var category = context.Category.First(c => c.Id == product.CategoryId);
                ImageHelper.DeleteImage(product.ImageSource);
                product.Category = null;
                product.Layout = null;
                context.DeleteObject(product);
                context.SaveChanges();
                return RedirectToAction("Details", "Category", new {id = category.Name});
            }
        }

        public ActionResult MakeDefaultPicture(int id)
        {
            using (var context = new LibraryContainer())
            {
                var product = context.Product.Include("Category").First(p => p.Id == id);
                int categoryId = product.Category.Id;
                var category = context.Category.First(c=>c.Id==categoryId);
                category.ImageSource = product.ImageSource;
                context.SaveChanges();
                return RedirectToAction("Details", "Category", new {id = category.Name});
            }
        }

        
    }
}
