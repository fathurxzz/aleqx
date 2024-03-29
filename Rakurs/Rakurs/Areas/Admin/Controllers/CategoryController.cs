﻿using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rakurs.Helpers;
using Rakurs.Models;

namespace Rakurs.Areas.Admin.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        //
        // GET: /Admin/Category/

        public ActionResult Add(int? id)
        {
            using (var context = new StructureContainer())
            {
                var category = new Category { SortOrder = 0 };
                var attributes = context.ProductAttribute.ToList();
                ViewBag.Attributes = attributes;

                ViewData["parentId"] = id;
                if (id.HasValue)
                {
                    var parent = context.Category.First(c => c.Id == id);
                    category.Parent = parent;
                }

                return View(category);
            }
        }

        [HttpPost]
        public ActionResult Add(FormCollection form)
        {
            using (var context = new StructureContainer())
            {
                var category = new Category();

                if (!string.IsNullOrEmpty(form["parentId"]))
                {
                    int parentId = Convert.ToInt32(form["parentId"]);
                    var parent = context.Category.First(c => c.Id == parentId);
                    category.Parent = parent;
                }


                var attributes = context.ProductAttribute.ToList();
                PostCheckboxesData postData = form.ProcessPostCheckboxesData("attr", "parentId");
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


                TryUpdateModel(category, new[] { "Name", "Title", "TitleEng", "SortOrder" });
                category.Text = HttpUtility.HtmlDecode(form["Text"]);
                category.TextEng = HttpUtility.HtmlDecode(form["TextEng"]);
                context.AddToCategory(category);
                context.SaveChanges();

                return RedirectToAction("Index", "Catalogue",
                                        new
                                            {
                                                Area = "",
                                                category = category.Parent != null ? category.Parent.Name : category.Name,
                                                subCategory = category.Parent != null ? category.Name : null
                                            });
            }
        }


        public ActionResult Edit(int id)
        {
            using (var context = new StructureContainer())
            {
                var category = context.Category.Include("Parent").Include("ProductAttributes").First(c => c.Id == id);
                var attributes = context.ProductAttribute.ToList();
                ViewBag.Attributes = attributes;
                return View(category);
            }
        }

        [HttpPost]
        public ActionResult Edit(Category model, FormCollection form)
        {
            using (var context = new StructureContainer())
            {
                var category = context.Category.Include("Parent").First(c => c.Id == model.Id);

                TryUpdateModel(category, new[]
                                            {
                                                "Name",
                                                "Title",
                                                "TitleEng",
                                                "SortOrder"
                                            });
                category.Text = HttpUtility.HtmlDecode(form["Text"]);
                category.TextEng = HttpUtility.HtmlDecode(form["TextEng"]);

                var attributes = context.ProductAttribute.ToList();
                PostCheckboxesData postData = form.ProcessPostCheckboxesData("attr", "parentId");
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

                context.SaveChanges();

                return RedirectToAction("Index", "Catalogue", new { Area = "", category = category.Parent != null ? category.Parent.Name : category.Name });
            }
        }

        public ActionResult Delete(int id)
        {
            using (var context = new StructureContainer())
            {
                var category = context.Category.Include("Children").Include("Products").First(c => c.Id == id);
                while (category.Products.Any())
                {
                    var product = category.Products.First();
                    if (!string.IsNullOrEmpty(product.ImageSource))
                    {
                        IOHelper.DeleteFile("~/Content/Images", product.ImageSource);
                        IOHelper.DeleteFile("~/ImageCache/galleryThumbnail", product.ImageSource);
                        product.ProductAttributes.Clear();
                        context.DeleteObject(product);
                    }
                }
                context.SaveChanges();

                while (category.Children.Any())
                {
                    int catId = category.Children.First().Id;
                    var child = context.Category.Include("Products").Include("ProductAttributes").First(c => c.Id == catId);
                    child.ProductAttributes.Load();
                    while (child.Products.Any())
                    {
                        var product = child.Products.First();
                        if (!string.IsNullOrEmpty(product.ImageSource))
                        {
                            IOHelper.DeleteFile("~/Content/Images", product.ImageSource);
                            IOHelper.DeleteFile("~/ImageCache/galleryThumbnail", product.ImageSource);
                            product.ProductAttributes.Clear();
                            context.DeleteObject(product);
                            context.SaveChanges();
                        }
                    }
                    child.Products.Clear();
                    child.ProductAttributes.Clear();
                    context.DeleteObject(child);
                    context.SaveChanges();
                }
                category.ProductAttributes.Load();
                category.ProductAttributes.Clear();
                context.DeleteObject(category);
                context.SaveChanges();
            }
            return RedirectToAction("Index", "Home", new { Area = "", id = "" });
        }

    }
}
