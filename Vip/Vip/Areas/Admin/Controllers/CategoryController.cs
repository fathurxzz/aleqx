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
    public class CategoryController : Controller
    {
        public ActionResult Index()
        {
            using (var context = new SiteContainer())
            {
                var categories = context.Category.ToList();
                return View(categories);
            }
        }

        public ActionResult Create()
        {
            using (var context = new SiteContainer())
            {
                var category = new Category { SortOrder = 0 };
                var attributes = context.ProductAttribute.ToList();
                ViewBag.Attributes = attributes;
                return View(category);
            }
        }

        [HttpPost]
        public ActionResult Create(FormCollection form, HttpPostedFileBase fileUpload)
        {
            try
            {
                using (var context = new SiteContainer())
                {
                    var category = new Category();


                    var attributes = context.ProductAttribute.ToList();
                    PostCheckboxesData postData = form.ProcessPostCheckboxesData("attr");
                    foreach (var kvp in postData)
                    {
                        var attribute = attributes.First(a => a.Id == kvp.Key);
                        if (kvp.Value)
                        {
                            if (!category.ProductAttributes.Contains(attribute))
                                category.ProductAttributes.Add(attribute);
                        }
                        else
                        {
                            if (category.ProductAttributes.Contains(attribute))
                                category.ProductAttributes.Remove(attribute);
                        }
                    }

                    TryUpdateModel(category, new[] { 
                    "Name", 
                    "Title", 
                    "SortOrder",
                    "SeoDescription",
                    "SeoKeywords",
                    "Description",
                    "DescriptionTitle"
                    });

                    if (fileUpload != null)
                    {
                        string fileName = IOHelper.GetUniqueFileName("~/Content/Images", fileUpload.FileName);
                        string filePath = Server.MapPath("~/Content/Images");
                        filePath = Path.Combine(filePath, fileName);
                        //GraphicsHelper.SaveOriginalImage(filePath, fileName, fileUpload);
                        fileUpload.SaveAs(filePath);

                        category.ImageSource = fileName;
                    }

                    context.AddToCategory(category);
                    context.SaveChanges();
                    return RedirectToAction("Index", "Catalogue", new { area = "", category = category.Name });
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
                var category = context.Category.Include("ProductAttributes").First(c => c.Id == id);
                var attributes = context.ProductAttribute.ToList();
                ViewBag.Attributes = attributes;
                return View(category);
            }
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection form, HttpPostedFileBase fileUpload)
        {
            try
            {
                using (var context = new SiteContainer())
                {
                    var category = context.Category.First(c => c.Id == id);

                    TryUpdateModel(category, new[]{
                    "Name",
                    "Title",
                    "SortOrder",
                    "SeoDescription",
                    "SeoKeywords",
                    "Description",
                    "DescriptionTitle"
                });

                    var attributes = context.ProductAttribute.ToList();
                    PostCheckboxesData postData = form.ProcessPostCheckboxesData("attr");
                    foreach (var kvp in postData)
                    {
                        var attribute = attributes.First(a => a.Id == kvp.Key);
                        if (kvp.Value)
                        {
                            if (!category.ProductAttributes.Contains(attribute))
                                category.ProductAttributes.Add(attribute);
                        }
                        else
                        {
                            if (category.ProductAttributes.Contains(attribute))
                                category.ProductAttributes.Remove(attribute);
                        }
                    }

                    if (fileUpload != null)
                    {
                        ImageHelper.DeleteImage(category.ImageSource);

                        string fileName = IOHelper.GetUniqueFileName("~/Content/Images", fileUpload.FileName);
                        string filePath = Server.MapPath("~/Content/Images");
                        filePath = Path.Combine(filePath, fileName);
                        //GraphicsHelper.SaveOriginalImage(filePath, fileName, fileUpload);
                        fileUpload.SaveAs(filePath);
                        category.ImageSource = fileName;
                    }


                    context.SaveChanges();
                    return RedirectToAction("Index", "Catalogue", new { area = "", category = category.Name });
                }
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {

            
            using (var context = new SiteContainer())
            {
                var category = context.Category.Include("Products").Include("ProductAttributes").First(c => c.Id == id);
                if (!category.Products.Any())
                {
                    category.ProductAttributes.Clear();
                    ImageHelper.DeleteImage(category.ImageSource);

                    context.DeleteObject(category);
                    context.SaveChanges();

                }
            }
            }
            catch
            {
            }
            return RedirectToAction("Index");
        }
    }
}
