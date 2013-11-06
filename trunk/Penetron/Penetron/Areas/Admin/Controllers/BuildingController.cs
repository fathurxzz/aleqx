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
    public class BuildingController : Controller
    {
        private SiteContext _context;

        public BuildingController(SiteContext context)
        {
            _context = context;
        }

        public ActionResult Create(int? id)
        {
            if (id.HasValue)
                ViewBag.ParentId = id.Value;
            return View(new Building());
        }

        [HttpPost]
        [HandleError(ExceptionType = typeof(Exception), View = "DbError")]
        public ActionResult Create(FormCollection form, int? parentId)
        {
            var building = new Building
                             {
                                 Name = Utils.Transliterator.Transliterate(form["Name"])
                             };
            TryUpdateModel(building, new[] { "Title", "SortOrder", "SeoDescription", "SeoKeywords" });
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

                var bi = new BuildingImage();
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
    }
}
