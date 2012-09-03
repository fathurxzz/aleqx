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
                var category = context.Category.Include("ProductAttributes").First(c => c.Id == id);
                var brands = context.Brand.ToList();
                List<SelectListItem> brandItems = brands.Select(b => new SelectListItem { Text = b.Title, Value = b.Id.ToString() }).ToList();
                ViewBag.Brands = brandItems;
                var makers = context.Maker.ToList();
                List<SelectListItem> makerItems = makers.Select(b => new SelectListItem { Text = b.Title, Value = b.Id.ToString() }).ToList();
                ViewBag.Makers = makerItems;
                var layouts = context.Layout.Include("Parent").Include("Children").ToList();
                ViewBag.Layouts = layouts;
                ViewBag.Attributes = category.ProductAttributes;
                return View(new Product { Category = category });
            }
        }

        [HttpPost]
        public ActionResult Create(FormCollection form, int categoryId, int brandId, int makerId, IEnumerable<HttpPostedFileBase> fileUpload, IList<string> fileTitles)
        {
            try
            {
                using (var context = new SiteContainer())
                {
                    var category = context.Category.Include("ProductAttributes").First(c => c.Id == categoryId);
                    var brand = context.Brand.First(b => b.Id == brandId);
                    var maker = context.Maker.First(m => m.Id == makerId);

                    

                    var productLayouts = new List<Layout>();
                    var productAttributes = new List<ProductAttribute>();

                    PostCheckboxesData cbData = form.ProcessPostCheckboxesData("lyt");
                    foreach (var kvp in cbData)
                    {
                        var layoutId = kvp.Key;
                        bool layoutValue = kvp.Value;
                        if (layoutValue)
                        {
                            var layout = context.Layout.First(l => l.Id == layoutId);
                            productLayouts.Add(layout);
                        }
                    }

                    cbData = form.ProcessPostCheckboxesData("attr");
                    foreach (var kvp in cbData)
                    {
                        var attrId = kvp.Key;
                        bool attrValue = kvp.Value;
                        if (attrValue)
                        {
                            var attribute = context.ProductAttribute.First(at => at.Id == attrId);
                            productAttributes.Add(attribute);
                        }
                    }

                    int titleIndex = 0;
                    foreach (var file in fileUpload)
                    {
                        if (file != null)
                        {
                            var product = new Product
                            {
                                Category = category,
                                Brand = brand,
                                Maker = maker,
                                Title = fileTitles[titleIndex]
                            };

                            string fileName = IOHelper.GetUniqueFileName("~/Content/Images", file.FileName);
                            string filePath = Server.MapPath("~/Content/Images");
                            filePath = Path.Combine(filePath, fileName);
                            file.SaveAs(filePath);
                            product.ImageSource = fileName;

                            foreach (var productAttribute in productAttributes)
                            {
                                product.ProductAttributes.Add(productAttribute);
                            }

                            foreach (var productLayout in productLayouts)
                            {
                                product.Layouts.Add(productLayout);
                            }
                            context.AddToProduct(product);
                            titleIndex++;
                        }
                    }
                    
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
                var product = context.Product.Include("ProductAttributes").Include("Layouts").Include("Category").Include("Maker").Include("Brand").First(p => p.Id == id);
                var layouts = context.Layout.Include("Parent").Include("Children").ToList();
                ViewBag.Layouts = layouts;
                var category = context.Category.Include("ProductAttributes").First(c => c.Id == product.Category.Id);
                ViewBag.Attributes = category.ProductAttributes;
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

                    product.ProductAttributes.Clear();
                    cbData = form.ProcessPostCheckboxesData("attr");
                    foreach (var kvp in cbData)
                    {
                        var attrId = kvp.Key;
                        bool attrValue = kvp.Value;
                        if (attrValue)
                        {
                            var attribute = context.ProductAttribute.First(at => at.Id == attrId);
                            product.ProductAttributes.Add(attribute);
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
                product.ProductAttributes.Clear();
                ImageHelper.DeleteImage(product.ImageSource);
                context.DeleteObject(product);
                context.SaveChanges();
                return RedirectToAction("Index", "Catalogue", new { Area = "", category = categotyName });
            }
        }
    }
}
