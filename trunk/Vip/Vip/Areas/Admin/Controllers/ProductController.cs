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
        public ActionResult Create(int id, string filter)
        {
            using (var context = new CatalogueContainer())
            {
                var brand = context.Brand.First(c => c.Id == id);
                ViewBag.Filter = filter;
                return View(new Product { Brand = brand });
            }
        }

        [HttpPost]
        public ActionResult Create(FormCollection form, int brandId, IEnumerable<HttpPostedFileBase> fileUpload, IList<string> fileTitles, string filter)
        {
            try
            {
                using (var context = new CatalogueContainer())
                {
                    var brand = context.Brand.Include("Category").First(b => b.Id == brandId);

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
                        }
                    }

                    context.SaveChanges();

                    return RedirectToAction("Index", "Catalogue", new { Area = "", brand = brand.Name, category=brand.Category.Name, filter = filter });
                }
            }
            catch
            {
                return View();
            }
        }

        //public ActionResult Edit(int id)
        //{
        //    using (var context = new CatalogueContainer())
        //    {
        //        var product = context.Product.Include("Brand").First(p => p.Id == id);
        //        return View(product);
        //    }
        //}

        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection form, int brandId, int makerId, HttpPostedFileBase fileUpload)
        //{
        //    try
        //    {
        //        using (var context = new CatalogueContainer())
        //        {
        //            var product = context.Product.Include("Brand").First(p => p.Id == id);
        //            if (fileUpload != null)
        //            {
        //                if (!string.IsNullOrEmpty(product.ImageSource))
        //                {

        //                    IOHelper.DeleteFile("~/Content/Images", product.ImageSource);
        //                    foreach (var thumbnail in SiteSettings.Thumbnails)
        //                    {
        //                        IOHelper.DeleteFile("~/ImageCache/" + thumbnail.Key, product.ImageSource);
        //                    }
        //                }
        //                string fileName = IOHelper.GetUniqueFileName("~/Content/Images", fileUpload.FileName);
        //                string filePath = Server.MapPath("~/Content/Images");
        //                filePath = Path.Combine(filePath, fileName);
        //                fileUpload.SaveAs(filePath);
        //                product.ImageSource = fileName;
        //            }

        //            context.SaveChanges();

        //            return RedirectToAction("Index", "Catalogue", new { Area = "", brand = product.Brand.Name });
        //        }
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}




        public ActionResult Delete(int id, int? page, string filter)
        {
            using (var context = new CatalogueContainer())
            {
                var product = context.Product.Include("Brand").First(p => p.Id == id);
                var brandId = product.Brand.Id;
                var brand = context.Brand.Include("Category").First(b => b.Id == brandId);
                var brandName = brand.Name;
                var categoryName = brand.Category.Name;
                ImageHelper.DeleteImage(product.ImageSource);
                context.DeleteObject(product);
                context.SaveChanges();
                return RedirectToAction("Index", "Catalogue", new { Area = "", brand = brandName, category = categoryName, page = page, filter = filter });
            }
        }
    }
}
