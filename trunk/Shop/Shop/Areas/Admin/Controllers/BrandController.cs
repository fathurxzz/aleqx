using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shop.Helpers;
using Shop.Models;

namespace Shop.Areas.Admin.Controllers
{
    public class BrandController : Controller
    {
        //
        // GET: /Admin/Brand/

        public ActionResult Index()
        {
            using (var context = new ShopContainer())
            {
                var brands = context.Brand.ToList();
                return View(brands);
            }
        }

        //
        // GET: /Admin/Brand/Details/5

        public ActionResult Details(int id)
        {
            using (var context = new ShopContainer())
            {
                var brand = context.Brand.Where(b => b.Id == id).First();
                return View(brand);
            }
        }

        //
        // GET: /Admin/Brand/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Admin/Brand/Create

        [HttpPost]
        public ActionResult Create(FormCollection form, HttpPostedFileBase uploadFile)
        {
            try
            {
                using (var context = new ShopContainer())
                {

                    var brand = new Brand
                                    {
                                        Name = form["Name"], 
                                        Description = form["Description"], 
                                        SeoDescription = form["SeoDescription"], 
                                        SeoKeywords = form["SeoKeywords"]
                                    };
                    if (uploadFile != null)
                    {
                        string fileName = IOHelper.GetUniqueFileName("~/Content/Images", uploadFile.FileName);
                        string filePath = Server.MapPath("~/Content/Images");
                        filePath = Path.Combine(filePath, fileName);
                        uploadFile.SaveAs(filePath);
                        brand.Logo = fileName;
                    }
                    context.AddToBrand(brand);
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
        // GET: /Admin/Brand/Edit/5

        public ActionResult Edit(int id)
        {
            using (var context = new ShopContainer())
            {
                var brand = context.Brand.Where(b => b.Id == id).First();
                return View(brand);
            }
        }

        //
        // POST: /Admin/Brand/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection, HttpPostedFileBase uploadFile)
        {
            try
            {
                using (var context = new ShopContainer())
                {
                    var brand = context.Brand.First(b => b.Id == id);
                    TryUpdateModel(brand, new[] {"Name", "Description", "SeoDescription", "SeoKeywords"});

                    if (uploadFile != null)
                    {
                        if(!string.IsNullOrEmpty(brand.Logo))
                        {
                            IOHelper.DeleteFile("~/Content/Images", brand.Logo);
                            IOHelper.DeleteFile("~/ImageCache/thumbnail0", brand.Logo);
                            IOHelper.DeleteFile("~/ImageCache/thumbnail1", brand.Logo);
                        }
                        string fileName = IOHelper.GetUniqueFileName("~/Content/Images", uploadFile.FileName);
                        string filePath = Server.MapPath("~/Content/Images");
                        filePath = Path.Combine(filePath, fileName);
                        uploadFile.SaveAs(filePath);
                        brand.Logo = fileName;
                    }

                    context.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Admin/Brand/Delete/5

        public ActionResult Delete(int id)
        {
            using (var context = new ShopContainer())
            {
                var brand = context.Brand.Where(b => b.Id == id).First();
                if (!string.IsNullOrEmpty(brand.Logo))
                {
                    IOHelper.DeleteFile("~/Content/Images", brand.Logo);
                    IOHelper.DeleteFile("~/ImageCache/thumbnail0", brand.Logo);
                    IOHelper.DeleteFile("~/ImageCache/thumbnail1", brand.Logo);
                }
                context.DeleteObject(brand);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}
