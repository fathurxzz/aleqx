using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EM2013.Models;
using SiteExtensions;

namespace EM2013.Areas.Admin.Controllers
{
    public class ProductItemController : Controller
    {
        public ActionResult Create(int id)
        {
            ViewBag.ProductId = id;
            return View(new ProductItem());
        } 

        [HttpPost]
        public ActionResult Create(int productId, FormCollection collection, HttpPostedFileBase fileUpload)
        {
            using (var context = new SiteContext())
            {
                var product = context.Product.Include("Category").First(c => c.Id == productId);
                
                var pi = new ProductItem();
                TryUpdateModel(pi, new[] { "Description", "SortOrder" });
                pi.Product = product;

                if (fileUpload != null)
                {
                    string fileName = IOHelper.GetUniqueFileName("~/Content/Images", fileUpload.FileName);
                    string filePath = Server.MapPath("~/Content/Images");
                    filePath = Path.Combine(filePath, fileName);
                    //GraphicsHelper.SaveOriginalImage(filePath, fileName, fileUpload);
                    fileUpload.SaveAs(filePath);
                    pi.ImageSource = fileName;
                }

                context.AddToProductItem(pi);
                context.SaveChanges();
                return RedirectToAction("Index", "Home", new { area = "", category =product.Category.Name, product = product.Name });
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
    }
}
