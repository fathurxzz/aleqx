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
                //context.UpdateTranslations(category.NamesXml);
            }
            return RedirectToAction("Categories");
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UpdateCategories(FormCollection form)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            if (!string.IsNullOrEmpty(form["updates"]))
            {
                Dictionary<string, Dictionary<string, string>> updates = serializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(form["updates"]);
            //    foreach (string key in updates.Keys)
            //    {
            //        int itemId = int.Parse(key);
            //        Dictionary<string, string> translations = updates[key];
            //        List<TranslationItem> translationItems = new List<TranslationItem>();
            //        translationItems = (from tr in translations select new TranslationItem { ItemId = itemId, ItemType = ItemTypes.Category, Language = tr.Key, Translation = tr.Value }).ToList();
            //        string translationXml = Utils.CreateTranslationXml(translationItems);
            //        using (ShopStorage context = new ShopStorage())
            //        {
            //            context.UpdateTranslations(translationXml);
            //        }
            //    }
            }
            //if (!string.IsNullOrEmpty(form["enablities"]))
            //{
            //    Dictionary<string, string> enables = serializer.Deserialize<Dictionary<string, string>>(form["enablities"]);
            //    using (ShopStorage context = new ShopStorage())
            //    {
            //        foreach (string key in enables.Keys)
            //        {
            //            int id = int.Parse(key);
            //            Category category = context.Categories.Select(c => c).Where(c => c.Id == id).First();
            //            category.Enabled = bool.Parse(enables[key]);
            //        }
            //        context.SaveChanges(true);
            //    }
            //}
            return RedirectToAction("Categories");
        }

        #endregion

    }
}
