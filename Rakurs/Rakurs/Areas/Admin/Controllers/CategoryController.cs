using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rakurs.Helpers;
using Rakurs.Models;

namespace Rakurs.Areas.Admin.Controllers
{
    //[Authorize]
    public class CategoryController : Controller
    {
        //
        // GET: /Admin/Category/

        public ActionResult Add(int? id)
        {
            using (var context = new StructureContainer())
            {
                var attributes = context.ProductAttribute.ToList();
                ViewBag.Attributes = attributes;

                ViewData["parentId"] = id;
                return View(new Category {SortOrder = 0});
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
                    var parent = context.Category.Where(c => c.Id == parentId).First();
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


                TryUpdateModel(category, new[] {"Name", "Title", "SortOrder" });
                category.Text = HttpUtility.HtmlDecode(form["Text"]);
                context.AddToCategory(category);
                context.SaveChanges();
            }
            return RedirectToAction("Index", "Home", new {Area = "", id = ""});
        }


        public ActionResult Edit(int id)
        {
            using (var context = new StructureContainer())
            {
                var category = context.Category.Include("Parent").Include("ProductAttributes").Where(c => c.Id == id).First();
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
                var category = context.Category.Include("Parent").Where(c => c.Id == model.Id).First();

                TryUpdateModel(category, new[]
                                            {
                                                "Name",
                                                "Title",
                                                "SortOrder"
                                            });
                category.Text = HttpUtility.HtmlDecode(form["Text"]);

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

                return RedirectToAction("Index", "Catalogue", new { Area = "", category = category.Parent!=null? category.Parent.Name:category.Name });
            }
        }

        public ActionResult Delete(int id)
        {
            throw new NotImplementedException("Удаление категорий пока не реализовано");
            return RedirectToAction("Index", "Home", new { Area = "" });
        }

    }
}
