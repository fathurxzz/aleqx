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
            using (var context = new CatalogueContainer())
            {
                var brand = context.Brand.First(c => c.Id == id);
                return View(new Product { Brand = brand });
            }
        }

        [HttpPost]
        public ActionResult Create(FormCollection form, int brandId, IEnumerable<HttpPostedFileBase> fileUpload, IList<string> fileTitles)
        {
            try
            {
                using (var context = new CatalogueContainer())
                {
                    var brand = context.Brand.Include("Category").First(b => b.Id == brandId);

                    int titleIndex = 0;
                    foreach (var file in fileUpload)
                    {
                        if (file != null)
                        {
                            var product = new Product { Brand = brand };
                            string fileName = IOHelper.GetUniqueFileName("~/Content/Images", file.FileName);
                            string filePath = Server.MapPath("~/Content/Images");
                            filePath = Path.Combine(filePath, fileName);
                            file.SaveAs(filePath);
                            product.ImageSource = fileName;

                            context.AddToProduct(product);
                            titleIndex++;
                        }
                    }

                    context.SaveChanges();

                    return RedirectToAction("Index", "Catalogue", new { Area = "", brand = brand.Name, category=brand.Category.Name });
                }
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            using (var context = new CatalogueContainer())
            {
                var product = context.Product.Include("Brand").First(p => p.Id == id);
                return View(product);
            }
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection form, int brandId, int makerId, HttpPostedFileBase fileUpload)
        {
            try
            {
                using (var context = new CatalogueContainer())
                {
                    var product = context.Product.Include("Brand").First(p => p.Id == id);
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

                    return RedirectToAction("Index", "Catalogue", new { Area = "", brand = product.Brand.Name });
                }
            }
            catch
            {
                return View();
            }
        }


        //[HttpPost]
        //public ActionResult EditMany(FormCollection form, int categoryId, int? page)
        //{
        //    var productIds = form.GetArray<int>("prod_");
        //    string sequence = productIds.Aggregate(string.Empty, (current, pid) => current + (pid + ","));
        //    ViewBag.ProductIds = sequence;
        //    ViewBag.Page = page;

        //    using (var context = new CatalogueContainer())
        //    {
        //        var brands = context.Brand.ToList();
        //        List<SelectListItem> brandItems = new List<SelectListItem> { new SelectListItem { Text = "", Value = "0", Selected = true } };
        //        brandItems.AddRange(brands.Select(brand => new SelectListItem { Text = brand.Title, Value = brand.Id.ToString() }));
        //        ViewBag.Brands = brandItems;

        //        var makers = context.Maker.ToList();
        //        List<SelectListItem> makerItems = new List<SelectListItem> { new SelectListItem { Text = "", Value = "0", Selected = true } };
        //        makerItems.AddRange(makers.Select(b => new SelectListItem { Text = b.Title, Value = b.Id.ToString() }));
        //        ViewBag.Makers = makerItems;

        //        //var product = context.Product.Include("ProductAttributes").Include("Layouts").Include("Category").Include("Maker").Include("Brand").First(p => p.Id == id);

        //        var layouts = context.Layout.Include("Parent").Include("Children").ToList();
        //        ViewBag.Layouts = layouts;

        //        var category = context.Category.Include("ProductAttributes").First(c => c.Id == categoryId);
        //        ViewBag.CategoryName = category.Name;
        //        ViewBag.CategoryTitle = category.Title;
        //        ViewBag.Attributes = category.ProductAttributes;
        //        //return View(product);
        //    }

        //    return View();
        //}


        //public ActionResult EditManyProcess(string productIds, FormCollection form, string categoryName, int brandId, int makerId, bool delete, int? page)
        //{
        //    int[] ids = productIds.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)
        //        .Select(c => Convert.ToInt32(c))
        //        .ToArray();

        //    using (var context = new SiteContainer())
        //    {
        //        var brand = context.Brand.FirstOrDefault(b => b.Id == brandId);
        //        var maker = context.Maker.FirstOrDefault(m => m.Id == makerId);

        //        var products = ids.Select(id => context.Product.First(p => p.Id == id)).ToList();

        //        if (delete)
        //        {

        //            foreach (var product in products)
        //            {
        //                product.Layouts.Clear();
        //                product.ProductAttributes.Clear();
        //                context.DeleteObject(product);
        //            }
        //        }
        //        else
        //        {




        //            foreach (var product in products)
        //            {
        //                if (brand != null)
        //                    product.Brand = brand;
        //                if (maker != null)
        //                    product.Maker = maker;

        //                if (!string.IsNullOrEmpty(form["ProductTitle"]))
        //                    product.Title = form["ProductTitle"];

        //                PostCheckboxesData cbData;

        //                if (form["editCategory"] == "true,false")
        //                {
        //                    product.Layouts.Clear();
        //                    cbData = form.ProcessPostCheckboxesData("lyt");
        //                    foreach (var kvp in cbData)
        //                    {
        //                        var layoutId = kvp.Key;
        //                        bool layoutValue = kvp.Value;
        //                        if (layoutValue)
        //                        {
        //                            var layout = context.Layout.First(l => l.Id == layoutId);
        //                            product.Layouts.Add(layout);
        //                        }
        //                    }
        //                }

        //                if (form["editAttributes"] == "true,false")
        //                {
        //                    product.ProductAttributes.Clear();
        //                    cbData = form.ProcessPostCheckboxesData("attr");
        //                    foreach (var kvp in cbData)
        //                    {
        //                        var attrId = kvp.Key;
        //                        bool attrValue = kvp.Value;
        //                        if (attrValue)
        //                        {
        //                            var attribute = context.ProductAttribute.First(at => at.Id == attrId);
        //                            product.ProductAttributes.Add(attribute);
        //                        }
        //                    }
        //                }
        //            }
        //        }

        //        context.SaveChanges();
        //    }
        //    return RedirectToAction("Index", "Catalogue", new { Area = "", category = categoryName, page = page.HasValue ? page : 0 });
        //}

        public ActionResult Delete(int id, int? page)
        {
            using (var context = new CatalogueContainer())
            {
                var product = context.Product.Include("Brand").First(p => p.Id == id);
                var brandName = product.Brand.Name;
                ImageHelper.DeleteImage(product.ImageSource);
                context.DeleteObject(product);
                context.SaveChanges();
                return RedirectToAction("Index", "Catalogue", new { Area = "", brand = brandName, page = page });
            }
        }
    }
}
