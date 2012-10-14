using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rakurs.Helpers;
using Rakurs.Models;

namespace Rakurs.Areas.Admin.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {

        public ActionResult Create(int id)
        {
            using (var context = new StructureContainer())
            {
                var category = context.Category.Include("Parent").Include("ProductAttributes").First(c => c.Id == id);
                ViewBag.CategoryId = category.Id;
                ViewBag.Attributes = category.ProductAttributes;
                if (category.Parent != null)
                {
                    ViewBag.CategoryName = category.Parent.Name;
                    ViewBag.SubCategoryName = category.Name;
                }
                else
                {
                    ViewBag.CategoryName = category.Name;
                }
                return View();
            }
        }

        [HttpPost]
        public ActionResult Create(FormCollection form, int categoryId, HttpPostedFileBase fileUpload)
        {
            using (var context = new StructureContainer())
            {
                Product product = new Product();
                Category category = context.Category.Include("Parent").First(c => c.Id == categoryId);
                product.Category = category;

                var attributes = context.ProductAttribute.ToList();
                PostCheckboxesData postData = form.ProcessPostCheckboxesData("attr", "categoryId");

                foreach (var kvp in postData)
                {
                    var attribute = attributes.First(a => a.Id == kvp.Key);
                    if (kvp.Value)
                    {
                        if (!product.ProductAttributes.Contains(attribute))
                            product.ProductAttributes.Add(attribute);
                    }
                    else
                    {
                        if (product.ProductAttributes.Contains(attribute))
                            product.ProductAttributes.Remove(attribute);
                    }
                }


                TryUpdateModel(product, new[] { "Title", "TitleEng", "Description", "DescriptionEng", "ShowOnMainPage" });

                if (fileUpload != null)
                {
                    string fileName = IOHelper.GetUniqueFileName("~/Content/Images", fileUpload.FileName);
                    string filePath = Server.MapPath("~/Content/Images");
                    filePath = Path.Combine(filePath, fileName);
                    GraphicsHelper.SaveOriginalImage(filePath, fileName, fileUpload);

                    product.ImageSource = fileName;
                }

                context.AddToProduct(product);

                context.SaveChanges();

                return category.Parent != null
                    ? RedirectToAction("Index", "Catalogue", new { Area = "", category = category.Parent.Name, subCategory = category.Name })
                    : RedirectToAction("Index", "Catalogue", new { Area = "", category = category.Name });
            }
        }

        public ActionResult CreateMany(int id)
        {
            using (var context = new StructureContainer())
            {
                var category = context.Category.Include("Parent").Include("ProductAttributes").First(c => c.Id == id);
                ViewBag.CategoryId = category.Id;
                ViewBag.Attributes = category.ProductAttributes;
                if (category.Parent != null)
                {
                    ViewBag.CategoryName = category.Parent.Name;
                    ViewBag.SubCategoryName = category.Name;
                }
                else
                {
                    ViewBag.CategoryName = category.Name;
                }
                return View();
            }
        }

        [HttpPost]
        public ActionResult CreateMany(FormCollection form, int categoryId, IList<HttpPostedFileBase> fileUpload, IList<string> titleRU, IList<string> titleEN, IList<string> descriptionRU, IList<string> descriptionEN)
        {
            using (var context = new StructureContainer())
            {
                
                Category category = context.Category.Include("Parent").First(c => c.Id == categoryId);
                var attributes = context.ProductAttribute.ToList();
                PostCheckboxesData postData = form.ProcessPostCheckboxesData("attr", "categoryId");

                for (int i = 0; i < 10; i++)
                {
                    if (fileUpload[i] != null)
                    {

                        Product product = new Product {Category = category};

                        string fileName = IOHelper.GetUniqueFileName("~/Content/Images", fileUpload[i].FileName);
                        string filePath = Server.MapPath("~/Content/Images");
                        filePath = Path.Combine(filePath, fileName);
                        GraphicsHelper.SaveOriginalImage(filePath, fileName, fileUpload[i]);
                        product.ImageSource = fileName;




                        foreach (var kvp in postData)
                        {
                            var attribute = attributes.First(a => a.Id == kvp.Key);
                            if (kvp.Value)
                            {
                                if (!product.ProductAttributes.Contains(attribute))
                                    product.ProductAttributes.Add(attribute);
                            }
                            else
                            {
                                if (product.ProductAttributes.Contains(attribute))
                                    product.ProductAttributes.Remove(attribute);
                            }
                        }


                        product.Title = titleRU[i];
                        product.TitleEng = titleEN[i];
                        product.Description = descriptionRU[i];
                        product.DescriptionEng = descriptionEN[i];

                        context.AddToProduct(product);

                    }
                }

                context.SaveChanges();

                return category.Parent != null
                    ? RedirectToAction("Index", "Catalogue", new { Area = "", category = category.Parent.Name, subCategory = category.Name })
                    : RedirectToAction("Index", "Catalogue", new { Area = "", category = category.Name });
            }
        }


        public ActionResult Edit(int id)
        {
            using (var context = new StructureContainer())
            {
                ViewBag.productId = id;
                var product = context.Product.Include("ProductAttributes").Include("Category").First(p => p.Id == id);
                var category = context.Category.Include("Parent").Include("ProductAttributes").First(c => c.Id == product.Category.Id);

                if (category.Parent != null)
                {
                    ViewBag.CategoryName = category.Parent.Name;
                    ViewBag.SubCategoryName = category.Name;
                }
                else
                {
                    ViewBag.CategoryName = category.Name;
                }

                ViewBag.CategoryId = category.Id;
                ViewBag.Attributes = category.ProductAttributes;

                return View(product);
            }
        }

        [HttpPost]
        public ActionResult Edit(FormCollection form, int id, HttpPostedFileBase fileUpload)
        {
            using (var context = new StructureContainer())
            {
                var product = context.Product.Include("Category").First(p => p.Id == id);
                var category = context.Category.Include("Parent").First(c => c.Id == product.Category.Id);


                var attributes = context.ProductAttribute.ToList();
                PostCheckboxesData postData = form.ProcessPostCheckboxesData("attr", "categoryId");

                foreach (var kvp in postData)
                {
                    var attribute = attributes.First(a => a.Id == kvp.Key);
                    if (kvp.Value)
                    {
                        if (!product.ProductAttributes.Contains(attribute))
                            product.ProductAttributes.Add(attribute);
                    }
                    else
                    {
                        if (product.ProductAttributes.Contains(attribute))
                            product.ProductAttributes.Remove(attribute);
                    }
                }

                TryUpdateModel(product, new[] { "Title", "TitleEng", "Description", "DescriptionEng", "ShowOnMainPage" });




                if (fileUpload != null)
                {
                    if (!string.IsNullOrEmpty(product.ImageSource))
                    {
                        IOHelper.DeleteFile("~/Content/Images", product.ImageSource);
                        IOHelper.DeleteFile("~/ImageCache/galleryThumbnail", product.ImageSource);
                    }

                    string fileName = IOHelper.GetUniqueFileName("~/Content/Images", fileUpload.FileName);
                    string filePath = Server.MapPath("~/Content/Images");
                    filePath = Path.Combine(filePath, fileName);
                    GraphicsHelper.SaveOriginalImage(filePath, fileName, fileUpload);
                    product.ImageSource = fileName;
                }

                context.SaveChanges();


                return category.Parent != null
                    ? RedirectToAction("Index", "Catalogue", new { Area = "", category = category.Parent.Name, subCategory = category.Name })
                    : RedirectToAction("Index", "Catalogue", new { Area = "", category = category.Name });
            }
        }

        public ActionResult Delete(int id)
        {
            using (var context = new StructureContainer())
            {
                var product = context.Product.Include("Category").First(p => p.Id == id);
                var categoryId = product.Category.Id;
                var category = context.Category.Include("Parent").First(c => c.Id == categoryId);

                if (!string.IsNullOrEmpty(product.ImageSource))
                {
                    IOHelper.DeleteFile("~/Content/Images", product.ImageSource);
                    IOHelper.DeleteFile("~/ImageCache/galleryThumbnail", product.ImageSource);
                }
                product.ProductAttributes.Clear();
                context.DeleteObject(product);
                context.SaveChanges();

                return category.Parent != null
                   ? RedirectToAction("Index", "Catalogue", new { Area = "", category = category.Parent.Name, subCategory = category.Name })
                   : RedirectToAction("Index", "Catalogue", new { Area = "", category = category.Name });
            }
        }
    }
}
