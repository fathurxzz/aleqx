using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Penetron.Helpers;
using Penetron.Models;
using SiteExtensions;

namespace Penetron.Areas.Admin.Controllers
{
    [Authorize]
    public class BuildingController : Controller
    {
        private SiteContext _context;

        public BuildingController(SiteContext context)
        {
            _context = context;
        }

        public ActionResult Create(int type, int? id)
        {
            if (id.HasValue)
                ViewBag.ParentId = id.Value;
            return View(new Building() { ContentType = type });
        }

        [HttpPost]
        [HandleError(ExceptionType = typeof(Exception), View = "DbError")]
        public ActionResult Create(FormCollection form, int? parentId)
        {
            var building = new Building
                             {
                                 Name = Utils.Transliterator.Transliterate(form["Name"])
                             };
            TryUpdateModel(building, new[] { "Title", "SortOrder", "SeoDescription", "SeoKeywords", "ContentType" });
            building.CategoryLevel = 1;
            building.Active = true;
            building.Text = HttpUtility.HtmlDecode(form["Text"]);

            if (parentId.HasValue)
            {
                var parent = _context.Building.First(c => c.Id == parentId);
                parent.Children.Add(building);
            }
            else
            {
                _context.AddToBuilding(building);
            }
            _context.SaveChanges();


            switch (form["ContentType"])
            {
                case "1": return RedirectToAction("Buildings", "Home", new { area = "" });
                case "2": return RedirectToAction("Products", "Home", new { area = "" });
                case "3": return RedirectToAction("Documents", "Home", new { area = "" });
                case "4": return RedirectToAction("WhereToBuy", "Home", new { area = "" });
                case "5": return RedirectToAction("About", "Home", new { area = "" });
            }
            return RedirectToAction("Buildings", "Home", new { area = "" });
        }



        public ActionResult Edit(int id)
        {
            var building = _context.Building.First(t => t.Id == id);
            return View(building);
        }

        [HttpPost]
        [HandleError(ExceptionType = typeof(Exception), View = "DbError")]
        public ActionResult Edit(Building model)
        {
            var building = _context.Building.First(t => t.Id == model.Id);
            building.Name = Utils.Transliterator.Transliterate(model.Name);
            building.Title = model.Title;
            building.SortOrder = model.SortOrder;
            building.Text = HttpUtility.HtmlDecode(model.Text);
            building.SeoDescription = model.SeoDescription;
            building.SeoKeywords = model.SeoKeywords;
            building.Active = model.Active;
            _context.SaveChanges();
            switch (model.ContentType)
            {
                case 1: return RedirectToAction("Buildings", "Home", new { area = "" });
                case 2: return RedirectToAction("Products", "Home", new { area = "" });
                case 3: return RedirectToAction("Documents", "Home", new { area = "" });
                case 4: return RedirectToAction("WhereToBuy", "Home", new { area = "" });
                case 5: return RedirectToAction("About", "Home", new { area = "" });
            }
            return RedirectToAction("Buildings", "Home", new { area = "" });
        }

        public ActionResult Delete(int id)
        {
            var technology = _context.Building.First(t => t.Id == id);
            var ct = technology.ContentType;
            _context.DeleteObject(technology);
            _context.SaveChanges();
            switch (ct)
            {
                case 1: return RedirectToAction("Buildings", "Home", new { area = "" });
                case 2: return RedirectToAction("Products", "Home", new { area = "" });
                case 3: return RedirectToAction("Documents", "Home", new { area = "" });
                case 4: return RedirectToAction("WhereToBuy", "Home", new { area = "" });
                case 5: return RedirectToAction("About", "Home", new { area = "" });
            }
            return RedirectToAction("Buildings", "Home", new { area = "" });
        }

        public ActionResult EditMainPage(int type)
        {
            var b = _context.Building.First(t => t.CategoryLevel == 0 && t.ContentType==type);
            return View("Edit", b);
        }

        [HttpPost]
        public ActionResult EditMainPage(Building model)
        {
            var building = _context.Building.First(t => t.Id == model.Id);
            building.Name = Utils.Transliterator.Transliterate(model.Name);
            building.Title = model.Title;
            building.SortOrder = model.SortOrder;
            building.Text = HttpUtility.HtmlDecode(model.Text);
            building.SeoDescription = model.SeoDescription;
            building.SeoKeywords = model.SeoKeywords;
            building.Active = model.Active;
            _context.SaveChanges();
            switch (model.ContentType)
            {
                case 1: return RedirectToAction("Buildings", "Home", new { area = "" });
                case 2: return RedirectToAction("Products", "Home", new { area = "" });
                case 3: return RedirectToAction("Documents", "Home", new { area = "" });
                case 4: return RedirectToAction("WhereToBuy", "Home", new { area = "" });
                case 5: return RedirectToAction("About", "Home", new { area = "" });
            }
            return RedirectToAction("Buildings", "Home", new { area = "" });
        }


        public ActionResult AddImage(int id)
        {
            var bo = _context.BuildingObj.First(t => t.Id == id);
            return View(bo);
        }

        [HttpPost]
        public ActionResult AddImage(int id, FormCollection form)
        {
            var bo = _context.BuildingObj.First(t => t.Id == id);
            for (int i = 0; i < Request.Files.Count; i++)
            {
                var file = Request.Files[i];

                if (file == null) continue;
                if (string.IsNullOrEmpty(file.FileName)) continue;

                var title = form["title[" + i + "]"];

                var bi = new BuildingImage{Title = title};
                string fileName = IOHelper.GetUniqueFileName("~/Content/Images", file.FileName);
                string filePath = Server.MapPath("~/Content/Images");

                filePath = Path.Combine(filePath, fileName);
                //GraphicsHelper.SaveOriginalImage(filePath, fileName, file, 1500);
                file.SaveAs(filePath);

                bi.ImageSource = fileName;

                bo.BuildingImages.Add(bi);

                _context.SaveChanges();
            }

            return RedirectToAction("Buildings", "Home", new { area = "" });
        }


        public ActionResult DeleteImage(int id)
        {
            var image = _context.BuildingImage.First(i => i.Id == id);
            ImageHelper.DeleteImage(image.ImageSource);
            _context.DeleteObject(image);
            _context.SaveChanges();
            return RedirectToAction("Buildings", "Home", new { area = "" });
        }






        public ActionResult AddContentItem(int id)
        {
            var content = _context.Building.Include("Parent").First(c => c.Id == id);
            ViewBag.ContentId = content.Id;
            if (content.Parent != null)
            {
                ViewBag.CatId = content.Parent.Name;
                ViewBag.SubCatId = content.Name;
            }
            else
            {
                ViewBag.CatId = content.Name;
            }
            return View(new BuildingItem());
        }

        [HttpPost]
        public ActionResult AddContentItem(int contentId, BuildingItem model)
        {
            var content = _context.Building.Include("Parent").First(c => c.Id == contentId);

            string catId = "";
            string subCatId = "";
            if (content.Parent != null)
            {
                catId = content.Parent.Name;
                subCatId = content.Name;
            }
            else
            {
                catId = content.Name;
            }


            var ci = new BuildingItem
            {
                SortOrder = model.SortOrder,
                Text = HttpUtility.HtmlDecode(model.Text)
            };
            content.BuildingItems.Add(ci);
            _context.SaveChanges();
            return RedirectToAction("Buildings", "Home", new { area = "", categoryId = catId, subCategoryId = subCatId });
        }


        public ActionResult EditContentItem(int id)
        {
            var ci = _context.BuildingItem.Include("Building").First(c => c.Id == id);
            var parent = _context.Building.Include("Parent").First(t => t.Id == ci.BuildingId);
            if (parent.Parent != null)
            {
                ViewBag.CatId = parent.Parent.Name;
                ViewBag.SubCatId = parent.Name;
            }
            else
            {
                ViewBag.CatId = parent.Name;
            }
            return View(ci);
        }

        [HttpPost]
        public ActionResult EditContentItem(BuildingItem model)
        {
            var ci = _context.BuildingItem.Include("Building").First(c => c.Id == model.Id);
            ci.SortOrder = model.SortOrder;
            ci.Text = HttpUtility.HtmlDecode(model.Text);
            _context.SaveChanges();

            var parent = _context.Building.Include("Parent").First(t => t.Id == ci.BuildingId);

            string catId = "";
            string subCatId = "";
            if (parent.Parent != null)
            {
                catId = parent.Parent.Name;
                subCatId = parent.Name;
            }
            else
            {
                catId = parent.Name;
            }

            return RedirectToAction("Buildings", "Home", new { area = "", categoryId = catId, subCategoryId = subCatId });
        }

        public ActionResult DeleteContentItem(int id)
        {
            var ci = _context.BuildingItem.Include("Building").First(c => c.Id == id);

            var parent = _context.Building.Include("Parent").First(t => t.Id == ci.BuildingId);
            string catId = "";
            string subCatId = "";
            if (parent.Parent != null)
            {
                catId = parent.Parent.Name;
                subCatId = parent.Name;
            }
            else
            {
                catId = parent.Name;
            }

            _context.DeleteObject(ci);
            _context.SaveChanges();
            return RedirectToAction("Buildings", "Home", new { area = "", categoryId = catId, subCategoryId = subCatId });
        }
    }
}
