﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rakurs.Helpers;
using Rakurs.Models;

namespace Rakurs.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {

        public ActionResult Create(int id)
        {
            using (var context = new StructureContainer())
            {
                var category = context.Category.Include("ProductAttributes").First(c => c.Id == id);
                ViewBag.CategoryId = category.Id;
                ViewBag.Attributes = category.ProductAttributes;
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


                TryUpdateModel(product, new[] { "Title", "Description", "ShowOnMainPage" });

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


                return RedirectToAction("Index", "Catalogue", new { Area = "", category = category.Parent.Name, subCategory = category.Name });
            }
        }

        public ActionResult Edit(int id)
        {
            using (var context = new StructureContainer())
            {
                ViewBag.productId = id;
                var category = context.Category.Include("ProductAttributes").First(c => c.Id == id);
                ViewBag.CategoryId = category.Id;
                ViewBag.Attributes = category.ProductAttributes;

                return View();
            }
        }

        [HttpPost]
        public ActionResult Edit(FormCollection form, int productId)
        {
            using (var context = new StructureContainer())
            {


                var product = context.Product.Include("Category").First(p => p.Id == productId);
                var category = context.Category.Include("Parent").First(c => c.Id == product.Category.Id);
                return RedirectToAction("Index", "Catalogue", new { Area = "", category = category.Parent.Name, subCategory = category.Name });
            }
        }

        public ActionResult Delete(int id)
        {
            using (var context = new StructureContainer())
            {
                var product = context.Product.Include("Category").First(p => p.Id == id);
                var categoryId = product.Category.Id;
                var category = context.Category.Include("Parent").First(c => c.Id == categoryId);
                return RedirectToAction("Index", "Catalogue", new { Area = "", category = category.Parent.Name, subCategory = category.Name });
            }
        }
    }
}
