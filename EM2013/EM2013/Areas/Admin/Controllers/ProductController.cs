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
    public class ProductController : Controller
    {
        public ActionResult Create(int id)
        {
            ViewBag.CategoryId = id;
            return View(new Product());
        } 

        [HttpPost]
        public ActionResult Create(int categoryId, FormCollection form, HttpPostedFileBase fileUpload)
        {
            try
            {
                using (var context = new SiteContext())
                {
                    var category = context.Category.First(c => c.Id == categoryId);
                    var product = new Product();
                    TryUpdateModel(product, new[] {"Name", "Title", "Description", "SortOrder", "SeoDescription", "SeoKeywords"});
                    product.Category = category;

                    if (fileUpload != null)
                    {
                        string fileName = IOHelper.GetUniqueFileName("~/Content/Images", fileUpload.FileName);
                        string filePath = Server.MapPath("~/Content/Images");
                        filePath = Path.Combine(filePath, fileName);
                        //GraphicsHelper.SaveOriginalImage(filePath, fileName, fileUpload);
                        fileUpload.SaveAs(filePath);
                        product.ImageSource = fileName;
                    }


                    context.AddToProduct(product);
                    context.SaveChanges();
                    return RedirectToAction("Index", "Home", new {area = "", category = category.Name, product = product.Name});
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
    }
}
