using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shop.Helpers;
using Shop.Models;
using SiteExtensions;
using SiteExtensions.Graphics;

namespace Shop.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        
        public ActionResult Create(int id)
        {
            using (var context = new ShopContainer())
            {
                int maxSortOrder = context.Product.Where(p=>p.CategoryId==id).Max(c => (int?)c.SortOrder) ?? 0;

                var category = context.Category.First(c => c.Id == id);

                ViewBag.CategoryName = category.Name;

                var product = new Product
                                  {
                                      SortOrder = maxSortOrder + 1,
                                      CategoryId = id
                                  };

                return View(product);
            }
        } 

        [HttpPost]
        public ActionResult Create(Product model, HttpPostedFileBase fileUpload)
        {
            try
            {
                using (var context = new ShopContainer())
                {
                    var category = context.Category.First(c => c.Id == model.CategoryId);

                    

                    var product = new Product
                                      {
                                          Name = SiteHelper.UpdatePageWebName(model.Name),
                                          Description = model.Description,
                                          SortOrder = model.SortOrder,
                                          Brand = model.Brand,
                                          Composition = model.Composition,
                                          Size = model.Size,
                                          Price = model.Price,
                                          SeoDescription = model.SeoDescription,
                                          SeoKeywords = model.SeoKeywords,
                                          Title = model.Title
                                      };


                    if (fileUpload != null)
                    {
                        string fileName = IOHelper.GetUniqueFileName("~/Content/Images", fileUpload.FileName);
                        string filePath = Server.MapPath("~/Content/Images");
                        filePath = Path.Combine(filePath, fileName);
                        GraphicsHelper.SaveOriginalImage(filePath, fileName, fileUpload, 1200);
                        //fileUpload.SaveAs(filePath);
                        product.ImageSource = fileName;
                        product.Preview = fileName;
                    }

                    product.Category = category;
                    context.AddToProduct(product);
                    context.SaveChanges();
                    
                    

                    return RedirectToAction("Category", "Home", new {area = "", id = category.Name});
                }
            }
            catch
            {
                return View();
            }
        }
        
        public ActionResult Edit(int id)
        {
            using (var context = new ShopContainer())
            {
                var product = context.Product.Include("Category").First(p => p.Id == id);
                

                
                return View(product);
            }
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection form, HttpPostedFileBase fileUpload)
        {
            try
            {
                using (var context = new ShopContainer())
                {
                    var product = context.Product.Include("Category").First(p => p.Id == id);


                    TryUpdateModel(product,new[]{"Title", "Name", "Description", "SortOrder", "Brand", "Composition", "Size","Price", "SeoDescription", "SeoKeywords"});

                    if (fileUpload != null)
                    {
                        if (!string.IsNullOrEmpty(product.ImageSource))
                        {
                            ImageHelper.DeleteImage(product.ImageSource);
                        }

                        string fileName = IOHelper.GetUniqueFileName("~/Content/Images", fileUpload.FileName);
                        string filePath = Server.MapPath("~/Content/Images");
                        filePath = Path.Combine(filePath, fileName);
                        GraphicsHelper.SaveOriginalImage(filePath, fileName, fileUpload, 500);
                        //fileUpload.SaveAs(filePath);
                        product.ImageSource = fileName;
                    }

                    context.SaveChanges();

                    return RedirectToAction("Category", "Home", new { area = "", id = product.Category.Name });
                }
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
