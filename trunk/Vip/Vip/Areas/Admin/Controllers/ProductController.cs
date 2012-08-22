using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SiteExtensions;
using Vip.Helpers;
using Vip.Models;

namespace Vip.Areas.Admin.Controllers
{
    [Authorize]
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
                var layouts = context.Layout.Include("Parent").Include("Children").ToList();
                ViewBag.Layouts = layouts;
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

                    PostCheckboxesData cbData = form.ProcessPostCheckboxesData("lyt");
                    foreach (var kvp in cbData)
                    {
                        var layoutId = kvp.Key;
                        bool layoutValue = kvp.Value;
                        if (layoutValue)
                        {
                            var layout = context.Layout.First(l => l.Id == layoutId);
                            product.Layouts.Add(layout);
                        }
                    }


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
            using (var context = new SiteContainer())
            {
                var brands = context.Brand.ToList();
                List<SelectListItem> brandItems = brands.Select(b => new SelectListItem { Text = b.Title, Value = b.Id.ToString() }).ToList();
                ViewBag.Brands = brandItems;
                var makers = context.Maker.ToList();
                List<SelectListItem> makerItems = makers.Select(b => new SelectListItem { Text = b.Title, Value = b.Id.ToString() }).ToList();
                ViewBag.Makers = makerItems;
                var product = context.Product.Include("Layouts").Include("Category").Include("Maker").Include("Brand").First(p => p.Id == id);
                var layouts = context.Layout.Include("Parent").Include("Children").ToList();
                ViewBag.Layouts = layouts;
                return View(product);
            }
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection form, int brandId, int makerId, HttpPostedFileBase fileUpload)
        {
            try
            {


                using (var context = new SiteContainer())
                {
                    var product = context.Product.Include("Category").Include("Brand").Include("Maker").First(p => p.Id == id);
                    var brand = context.Brand.First(b => b.Id == brandId);
                    var maker = context.Maker.First(m => m.Id == makerId);

                    product.Title = form["Title"];
                    product.Brand = brand;
                    product.Maker = maker;

                    product.Layouts.Clear();
                    PostCheckboxesData cbData = form.ProcessPostCheckboxesData("lyt");
                    foreach (var kvp in cbData)
                    {
                        var layoutId = kvp.Key;
                        bool layoutValue = kvp.Value;
                        if (layoutValue)
                        {
                            var layout = context.Layout.First(l => l.Id == layoutId);
                            product.Layouts.Add(layout);
                        }
                    }


                    if (fileUpload != null)
                    {
                        if (!string.IsNullOrEmpty(product.ImageSource))
                        {

                            IOHelper.DeleteFile("~/Content/Images", product.ImageSource);
                            foreach (var thumbnail in SiteSettings.Thumbnails)
                            {
                                IOHelper.DeleteFile("~/ImageCache/" + thumbnail.Key, product.ImageSource);
                            }
                        }
                        string fileName = IOHelper.GetUniqueFileName("~/Content/Images", fileUpload.FileName);
                        string filePath = Server.MapPath("~/Content/Images");
                        filePath = Path.Combine(filePath, fileName);
                        fileUpload.SaveAs(filePath);
                        product.ImageSource = fileName;
                    }

                    context.SaveChanges();

                    return RedirectToAction("Index", "Catalogue", new { Area = "", category = product.Category.Name });
                }
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            using (var context = new SiteContainer())
            {
                var product = context.Product.Include("Category").Include("Brand").Include("Maker").First(p => p.Id == id);
                var categotyName = product.Category.Name;
                product.Layouts.Clear();
                ImageHelper.DeleteImage(product.ImageSource);
                context.DeleteObject(product);
                context.SaveChanges();
                return RedirectToAction("Index", "Catalogue", new { Area = "", category = categotyName });
            }
        }
    }
}
