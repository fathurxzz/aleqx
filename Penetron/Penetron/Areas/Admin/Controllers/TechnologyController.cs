using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Penetron.Helpers;
using Penetron.Models;
using SiteExtensions;
using SiteExtensions.Graphics;

namespace Penetron.Areas.Admin.Controllers
{
    [Authorize]
    public class TechnologyController : Controller
    {
        private SiteContext _context;

        public TechnologyController(SiteContext context)
        {
            _context = context;
        }

        public ActionResult Create(int? id)
        {
            if (id.HasValue)
                ViewBag.ParentId = id.Value;
            return View(new Technology());
        }

        [HttpPost]
        [HandleError(ExceptionType = typeof(Exception), View = "DbError")]
        public ActionResult Create(FormCollection form, int? parentId)
        {
            var technology = new Technology
                             {
                                 Name = Utils.Transliterator.Transliterate(form["Name"])
                             };
            TryUpdateModel(technology, new[] { "Title", "SortOrder", "SeoDescription", "SeoKeywords" });
            technology.CategoryLevel = 1;
            technology.Active = true;
            technology.Text = HttpUtility.HtmlDecode(form["Text"]);

            if (parentId.HasValue)
            {
                var parent = _context.Technology.First(c => c.Id == parentId);
                parent.Children.Add(technology);
            }
            else
            {
                _context.AddToTechnology(technology);
            }
            _context.SaveChanges();
            return RedirectToAction("Technologies", "Home", new { area = "" });
        }



        public ActionResult Edit(int id)
        {
            var technology = _context.Technology.First(t => t.Id == id);
            return View(technology);
        }

        [HttpPost]
        [HandleError(ExceptionType = typeof(Exception), View = "DbError")]
        public ActionResult Edit(Technology model)
        {
            var technology = _context.Technology.First(t => t.Id == model.Id);
            technology.Name = Utils.Transliterator.Transliterate(model.Name);
            technology.Title = model.Title;
            technology.SortOrder = model.SortOrder;
            technology.Text = HttpUtility.HtmlDecode(model.Text);
            technology.SeoDescription = model.SeoDescription;
            technology.SeoKeywords = model.SeoKeywords;
            technology.Active = model.Active;
            _context.SaveChanges();
            return RedirectToAction("Technologies", "Home", new { area = "" });
        }

        public ActionResult Delete(int id)
        {
            var technology = _context.Technology.First(t => t.Id == id);
            _context.DeleteObject(technology);
            _context.SaveChanges();
            return RedirectToAction("Technologies", "Home", new { area = "" });
        }


        public ActionResult EditMainPage()
        {
            var technology = _context.Technology.First(t => t.CategoryLevel == 0);
            return View("Edit", technology);
        }


        public ActionResult AddImage(int id)
        {
            var technology = _context.Technology.First(t => t.Id == id);
            return View(technology);
        }

        [HttpPost]
        public ActionResult AddImage(int id, FormCollection form)
        {
            var technology = _context.Technology.Include("Parent").First(t => t.Id == id);
            for (int i = 0; i < Request.Files.Count; i++)
            {
                var file = Request.Files[i];

                if (file == null) continue;
                if (string.IsNullOrEmpty(file.FileName)) continue;

                var ti = new TechnologyImage();
                string fileName = IOHelper.GetUniqueFileName("~/Content/Images", file.FileName);
                string filePath = Server.MapPath("~/Content/Images");

                filePath = Path.Combine(filePath, fileName);
                //GraphicsHelper.SaveOriginalImage(filePath, fileName, file, 1500);
                file.SaveAs(filePath);

                ti.ImageSource = fileName;

                technology.TechnologyImages.Add(ti);

                _context.SaveChanges();
            }

            string categoryId;
            string subCategoryId = null;
            if (technology.Parent == null)
            {
                categoryId = technology.Name;
            }
            else
            {
                categoryId = technology.Parent.Name;
                subCategoryId = technology.Name;
            }

            return RedirectToAction("Technologies", "Home", new { area = "", categoryId = categoryId, subCategoryId = subCategoryId });
        }

        public ActionResult DeleteImage(int id, string categoryId, string subCategoryId)
        {
            var image = _context.TechnologyImage.First(i => i.Id == id);
            ImageHelper.DeleteImage(image.ImageSource);
            _context.DeleteObject(image);
            _context.SaveChanges();
            return RedirectToAction("Technologies", "Home", new { area = "", categoryId = categoryId, subCategoryId = subCategoryId });
        }


        public ActionResult AddContentItem(int id)
        {
            var content = _context.Technology.Include("Parent").First(c => c.Id == id);
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

            return View(new TechnologyItem());
        }

        [HttpPost]
        public ActionResult AddContentItem(int contentId, TechnologyItem model)
        {
            var content = _context.Technology.Include("Parent").First(c => c.Id == contentId);

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


            var ci = new TechnologyItem
            {
                SortOrder = model.SortOrder,
                Text = HttpUtility.HtmlDecode(model.Text)
            };
            content.TechnologyItems.Add(ci);
            _context.SaveChanges();
            return RedirectToAction("Technologies", "Home", new { area = "", categoryId = catId, subCategoryId = subCatId });
        }


        public ActionResult EditContentItem(int id)
        {
            var ci = _context.TechnologyItem.Include("Technology").First(c => c.Id == id);
            var parent = _context.Technology.Include("Parent").First(t => t.Id == ci.TechnologyId);
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
        public ActionResult EditContentItem(ContentItem model)
        {
            var ci = _context.TechnologyItem.Include("Technology").First(c => c.Id == model.Id);
            ci.SortOrder = model.SortOrder;
            ci.Text = HttpUtility.HtmlDecode(model.Text);
            _context.SaveChanges();

            var parent = _context.Technology.Include("Parent").First(t => t.Id == ci.TechnologyId);

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

            return RedirectToAction("Technologies", "Home", new { area = "", categoryId = catId, subCategoryId = subCatId });
        }

        public ActionResult DeleteContentItem(int id)
        {
            var ci = _context.TechnologyItem.Include("Technology").First(c => c.Id == id);
            
            var parent = _context.Technology.Include("Parent").First(t => t.Id == ci.TechnologyId);
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
            return RedirectToAction("Technologies", "Home", new { area = "", categoryId = catId, subCategoryId = subCatId });
        }

    }
}
