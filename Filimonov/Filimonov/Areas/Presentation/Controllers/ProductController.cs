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
    [Authorize]

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
        [Authorize(Roles = "Administrators")]
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
        [Authorize(Roles = "Administrators")]
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
                return RedirectToAction("Details", "Category", new { id = category.Name });
            }
        }
        [Authorize(Roles = "Administrators")]
        public ActionResult MakeDefaultPicture(int id)
        {
            using (var context = new LibraryContainer())
            {
                var product = context.Product.Include("Category").First(p => p.Id == id);
                int categoryId = product.Category.Id;
                var category = context.Category.First(c => c.Id == categoryId);
                category.ImageSource = product.ImageSource;
                context.SaveChanges();
                return RedirectToAction("Details", "Category", new { id = category.Name });
            }
        }

        [HttpPost]
        public ActionResult AddProductToSet(FormCollection form)
        {
            using (var context = new LibraryContainer())
            {
                var categoryId = form["categoryId"];

                var serializer = new JavaScriptSerializer();
                if (!string.IsNullOrEmpty(form["enablities"]))
                {
                    var enables = serializer.Deserialize<Dictionary<string, int>>(form["enablities"]);


                    var productIds = new List<int>();
                    var productIdsToDelete = new List<int>();

                    foreach (KeyValuePair<string, int> pair in enables)
                    {
                        int key = Convert.ToInt32(pair.Key.Split(new[] { "p_" }, StringSplitOptions.None)[1]);

                        if (pair.Value == 1)
                            productIds.Add(key);
                        else
                            productIdsToDelete.Add(key);
                    }

                    int productSetId = Convert.ToInt32(form["productContainers"]);

                    var productSet = context.ProductSet.Include("Products").First(ps => ps.Id == productSetId);


                    foreach (int id in productIdsToDelete)
                    {
                        var product = productSet.Products.FirstOrDefault(p => p.Id == id);

                        if (product != null && productSet.Products.Contains(product))
                            productSet.Products.Remove(product);
                    }
                    asdasd

                    foreach (int id in productIds)
                    {
                        var product = context.Product.First(p => p.Id == id);
                        if (!productSet.Products.Contains(product))
                            productSet.Products.Add(product);
                    }

                    context.SaveChanges();

                    //var client = context.Customer.First(c => c.Name == User.Identity.Name);

                }

                return RedirectToAction("Details", "Category", new { id = categoryId });
            }
        }
    }
}
