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
    [Authorize]
    public class CategoryController : Controller
    {
        //
        // GET: /Admin/Category/

        private void LoadChildren(Category category, int level)
        {
            category.Children.Load();
            foreach (var child in category.Children)
            {
                level++;
                child.Level = level;
                LoadChildren(child, level);
                level--;
            }
        }

        public ActionResult Index()
        {
            using (var context = new ShopContainer())
            {
                var categories = context.Category.Where(c => c.Parent == null).ToList();
                int level = 0;
                foreach (var category in categories)
                {
                    category.Level = level;
                    LoadChildren(category, level);
                }

                return View(categories);
            }
        }


        //
        // GET: /Admin/Category/Create

        public ActionResult Create(int? parentId)
        {
            if (parentId.HasValue)
                ViewBag.ParentId = parentId.Value;
            return View(new Category());
        }

        //
        // POST: /Admin/Category/Create

        [HttpPost]
        public ActionResult Create(int? parentId, FormCollection form, HttpPostedFileBase uploadFile)
        {
            try
            {
                using (var context = new ShopContainer())
                {
                    var category = new Category();
                    TryUpdateModel(category, new[] { "Name", "SeoDescription", "SeoKeywords", "SortOrder","Title" });

                    if (uploadFile != null)
                    {
                        string fileName = IOHelper.GetUniqueFileName("~/Content/Images", uploadFile.FileName);
                        string filePath = Server.MapPath("~/Content/Images");
                        filePath = Path.Combine(filePath, fileName);
                        uploadFile.SaveAs(filePath);
                        category.ImageSource = fileName;
                    }


                    if (parentId.HasValue)
                    {
                        var parent = context.Category.First(c => c.Id == parentId);
                        parent.Children.Add(category);
                    }
                    else
                    {
                        context.AddToCategory(category);
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
        // GET: /Admin/Category/Edit/5

        public ActionResult Edit(int id)
        {
            using (var context = new ShopContainer())
            {
                var category = context.Category.First(c => c.Id == id);
                return View(category);
            }
        }

        //
        // POST: /Admin/Category/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection form, HttpPostedFileBase uploadFile)
        {
            try
            {
                using (var context = new ShopContainer())
                {
                    var category = context.Category.First(c => c.Id == id);
                    TryUpdateModel(category, new[] { "Name", "SeoDescription", "SeoKeywords", "SortOrder", "Title" });

                    if (uploadFile != null)
                    {
                        if (!string.IsNullOrEmpty(category.ImageSource))
                        {
                            IOHelper.DeleteFile("~/Content/Images", category.ImageSource);
                            
                            foreach (var folder in GraphicsHelper.ThumbnailFolders)
                            {
                                IOHelper.DeleteFile("~/ImageCache/" + folder, category.ImageSource);
                            }
                        }

                        string fileName = IOHelper.GetUniqueFileName("~/Content/Images", uploadFile.FileName);
                        string filePath = Server.MapPath("~/Content/Images");
                        filePath = Path.Combine(filePath, fileName);
                        uploadFile.SaveAs(filePath);
                        category.ImageSource = fileName;
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

        public ActionResult Delete(int id)
        {
            using (var context = new ShopContainer())
            {
                var category = context.Category.Include("ProductAttributes").First(c => c.Id == id);
                if (!string.IsNullOrEmpty(category.ImageSource))
                {
                    IOHelper.DeleteFile("~/Content/Images", category.ImageSource);

                    foreach (var folder in GraphicsHelper.ThumbnailFolders)
                    {
                        IOHelper.DeleteFile("~/ImageCache/" + folder, category.ImageSource);
                    }
                }
                category.ProductAttributes.Clear();
                context.DeleteObject(category);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Attributes(int id)
        {
            using (var context = new ShopContainer())
            {
                var category = context.Category.Include("ProductAttributes").First(c => c.Id == id);
                ViewBag.Category = category;
                var attributes = context.ProductAttribute.ToList();
                return View(attributes);
            }
        }

        [HttpPost]
        public ActionResult Attributes(int categoryId, FormCollection form)
        {
            try
            {
                using (var context = new ShopContainer())
                {
                    var category = context.Category.Include("ProductAttributes").First(c => c.Id == categoryId);
                    var attributes = context.ProductAttribute.ToList();

                    PostCheckboxesData postData = form.ProcessPostCheckboxesData("attr", "categoryId");

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

                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return View();
            }
        }
    }
}
