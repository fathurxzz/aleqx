﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SiteExtensions;
using Vip.Models;

namespace Vip.Areas.Admin.Controllers
{
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
                    "SortOrder"
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
                }

                return RedirectToAction("Index");
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
                    "SortOrder"
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
                        if (!string.IsNullOrEmpty(category.ImageSource))
                        {

                            IOHelper.DeleteFile("~/Content/Images", category.ImageSource);
                            foreach (var thumbnail in SiteSettings.Thumbnails)
                            {
                                IOHelper.DeleteFile("~/ImageCache/" + thumbnail.Key, category.ImageSource);
                            }
                        }

                        string fileName = IOHelper.GetUniqueFileName("~/Content/Images", fileUpload.FileName);
                        string filePath = Server.MapPath("~/Content/Images");
                        filePath = Path.Combine(filePath, fileName);
                        //GraphicsHelper.SaveOriginalImage(filePath, fileName, fileUpload);
                        fileUpload.SaveAs(filePath);
                        category.ImageSource = fileName;
                    }


                    context.SaveChanges();

                    return RedirectToAction("Index");
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