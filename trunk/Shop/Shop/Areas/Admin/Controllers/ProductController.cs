using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shop.Models;

namespace Shop.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        //
        // GET: /Admin/Product/

        public ActionResult Index()
        {
            using (var context = new ShopContainer())
            {
                var products = context.Product.ToList();
                return View(products);
            }
        }

        //
        // GET: /Admin/Product/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Admin/Product/Create

        public ActionResult Create()
        {
            using (var context = new ShopContainer())
            {
                var categories = context.Category.ToList();
                List<SelectListItem> categoryItems = categories.Select(category => new SelectListItem { Text = category.Name, Value = category.Id.ToString() }).ToList();
                ViewBag.Categories = categoryItems;

                var brands = context.Brand.ToList();
                List<SelectListItem> brandItems = brands.Select(b => new SelectListItem {Text = b.Name, Value = b.Id.ToString()}).ToList();
                ViewBag.Brands = brandItems;
                return View();
            }
        }

        //
        // POST: /Admin/Product/Create

        [HttpPost]
        public ActionResult Create(int categoryId, int brandId, FormCollection form)
        {
            try
            {
                using (var context = new ShopContainer())
                {
                    var category = context.Category.First(c => c.Id == categoryId);
                    var brand = context.Brand.First(b => b.Id == brandId);
                    var product = new Product
                                      {
                                          Category = category,
                                          Brand = brand
                                      };
                    TryUpdateModel(product, 
                        new[]
                        {
                            "Name",
                            "SortOrder",
                            "Price", 
                            "OldPrice",
                            "ShortDescription",
                            "Description",
                            "IsNew",
                            "IsSpecialOffer",
                            "Published",
                            "SeoDescription",
                            "SeoKeywords"
                        });
                    context.AddToProduct(product);
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Admin/Product/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Admin/Product/Edit/5

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
        // GET: /Admin/Product/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Admin/Product/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        public ActionResult Attributes(int id)
        {
            using (var context = new ShopContainer())
            {
                var product = context.Product.Include("Category").First(p => p.Id == id);
                product.Category.ProductAttributes.Load();
                foreach (var attribute in product.Category.ProductAttributes)
                {
                    attribute.ProductAttributeValues.Load();
                }

                return View(product.Category.ProductAttributes);
            }
        }
    }
}
