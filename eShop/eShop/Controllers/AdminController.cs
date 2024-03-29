using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using eShop.Models;
using System.Web.Script.Serialization;

namespace eShop.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/
        public ActionResult MainMenu()
        {

            return View();
        }
        public ActionResult Index()
        {
            return View();
        }

        #region Catigories

        public ActionResult Categories()
        {
            return View();
        }

        public ActionResult CategoriesList(int? id, int level)
        {
            using (ShopStorage context = new ShopStorage())
            {
                List<Category> categories = context.Categories.Select(c => c).ToList();
                if (id == null)
                    categories = categories.Select(c => c).Where(c => c.Parent == null).ToList();
                else
                    categories = categories.Select(c => c).Where(c => c.Parent != null && c.Parent.Id == id.Value).ToList();
                ViewData["level"] = level;
                if (id != null)
                    ViewData["id"] = id.Value;
                return View(categories);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult InsertCategory(FormCollection form)
        {
            using (ShopStorage context = new ShopStorage())
            {
                int parentId = int.Parse(form["parentId"]);
                Category parent = null;
                if (parentId >= 0)
                    parent = context.Categories.Select(c => c).Where(c => c.Id == parentId).First();
                Category category = new Category();
                category.Parent = parent;
                category.Name = form["categoryName"];
                category.Enabled = form["categoryEnabled"].ToLowerInvariant().IndexOf("true") > -1;
                context.AddToCategories(category);
                context.SaveChanges();
            }
            return RedirectToAction("Categories");
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UpdateCategories(FormCollection form)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            
            if (!string.IsNullOrEmpty(form["updates"]))
            {
                Dictionary<string, string> updates = serializer.Deserialize<Dictionary<string, string>>(form["updates"]);
                using (ShopStorage context = new ShopStorage())
                {
                    foreach(string key in updates.Keys)
                    {
                        int id = int.Parse(key);
                        Category category = context.Categories.Select(c => c).Where(c => c.Id == id).First();
                        category.Name = updates[key];
                    }
                    context.SaveChanges();
                }
            }

            if (!string.IsNullOrEmpty(form["enablities"]))
            {
                Dictionary<string, string> enables = serializer.Deserialize<Dictionary<string, string>>(form["enablities"]);
                using (ShopStorage context = new ShopStorage())
                {
                    foreach (string key in enables.Keys)
                    {
                        int id = int.Parse(key);
                        Category category = context.Categories.Select(c => c).Where(c => c.Id == id).First();
                        category.Enabled = bool.Parse(enables[key]);
                    }
                    context.SaveChanges(true);
                }
            }
            return RedirectToAction("Categories");
        }

        public ActionResult DeleteCategory(int id)
        {
            using (ShopStorage context = new ShopStorage())
            {
                Category category = context.Categories.Select(c => c).Where(c => c.Id == id).First();
                context.DeleteObject(category);
                context.SaveChanges();
            }

            return RedirectToAction("Categories");
        }

        #endregion



        #region CategoryProperty

        public ActionResult CategoryPropertiesList(int? id)
        {
            using (ShopStorage context = new ShopStorage())
            {
                List<CategoryProperties> categoryProperties = (from categoryProperty in context.CategoryProperties where categoryProperty.Category.Id == SystemSettings.CategoryId select categoryProperty).ToList();
                return View(categoryProperties);
            }
        }


        public ActionResult CategoryProperties(string sCategory, string pCategory)
        {
            bool isParentChanged = pCategory != SystemSettings.ParentCategoryId.ToString() && !string.IsNullOrEmpty(sCategory);
            
            if (!string.IsNullOrEmpty(pCategory))
            {
                SystemSettings.ParentCategoryId = int.Parse(pCategory);
            }
            else if (SystemSettings.ParentCategoryId == int.MinValue)
            {
                using (ShopStorage context = new ShopStorage())
                {
                    Category parentCategory = context.Categories.Select(c => c).Where(c => c.Enabled && c.Parent == null).First();
                    SystemSettings.ParentCategoryId = parentCategory.Id;
                }
            }

            if (isParentChanged || SystemSettings.CategoryId == int.MinValue)
            {
                using (ShopStorage context = new ShopStorage())
                {
                    Category category = context.Categories.Select(c => c).Where(c => c.Enabled && c.Parent != null && c.Parent.Id == SystemSettings.ParentCategoryId).First();
                    SystemSettings.CategoryId = category.Id;
                }
            }
            else if (!string.IsNullOrEmpty(sCategory))
            {
                SystemSettings.CategoryId = int.Parse(sCategory);
            }

            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult InsertCategoryProperty(FormCollection form)
        {
            using (ShopStorage context = new ShopStorage())
            {
                int categoryId = SystemSettings.CategoryId;
                Category category = context.Categories.Select(c => c).Where(c => c.Id == categoryId).First();
                CategoryProperties categoryProperty = new CategoryProperties();
                categoryProperty.Category = category;
                categoryProperty.Name = form["categoryPropertyName"];
                categoryProperty.Unit = form["categoryUnitName"];

                categoryProperty.IsMainProperty = form["isMainProperty"].ToLowerInvariant().IndexOf("true") > -1;
                context.AddToCategoryProperties(categoryProperty);
                context.SaveChanges();
            }
            return RedirectToAction("CategoryProperties");
        }

        public ActionResult DeleteCategoryProperty(int id)
        {
            using (ShopStorage context = new ShopStorage())
            {
                CategoryProperties categoryProperty = context.CategoryProperties.Select(c => c).Where(c => c.Id == id).First();
                context.DeleteObject(categoryProperty);
                context.SaveChanges();
            }

            return RedirectToAction("CategoryProperties");
        }

        #endregion

        #region Products

        public ActionResult Products()
        {
            return View();
        }
        #endregion
    }
}
