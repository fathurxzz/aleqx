using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SiteExtensions;
using Vip.Models;

namespace Vip.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        public ActionResult Create(int id)
        {
            using (var context = new SiteContainer())
            {
                var category = context.Category.First(c => c.Id == id);
                var brands = context.Brand.ToList();
                List<SelectListItem> brandItems = brands.Select(b => new SelectListItem { Text = b.Title, Value = b.Id.ToString() }).ToList();
                ViewBag.Brands = brandItems;
                var makers = context.Maker.ToList();
                List<SelectListItem> makerItems = makers.Select(b => new SelectListItem { Text = b.Title, Value = b.Id.ToString() }).ToList();
                ViewBag.Makers = makerItems;
                return View(new Product { Category = category });
            }
        }

        [HttpPost]
        public ActionResult Create(FormCollection form, int categoryId, int brandId, int makerId, HttpPostedFileBase fileUpload)
        {
            try
            {
                using (var context = new SiteContainer())
                {
                    var category = context.Category.First(c => c.Id == categoryId);
                    var brand = context.Brand.First(b => b.Id == brandId);
                    var maker = context.Maker.First(m => m.Id == makerId);

                    var product = new Product
                                      {
                                          Category = category,
                                          Brand = brand,
                                          Maker = maker,
                                          Title = form["Title"]
                                      };

                    if (fileUpload != null)
                    {
                        string fileName = IOHelper.GetUniqueFileName("~/Content/Images", fileUpload.FileName);
                        string filePath = Server.MapPath("~/Content/Images");
                        filePath = Path.Combine(filePath, fileName);
                        fileUpload.SaveAs(filePath);
                        product.ImageSource = fileName;
                    }
                    
                    context.AddToProduct(product);
                    context.SaveChanges();
                    
                    return RedirectToAction("Index", "Catalogue", new { Area = "", category = category.Name });
                }
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            return View();
        }

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

        public ActionResult Delete(int id)
        {
            return View();
        }
    }
}
