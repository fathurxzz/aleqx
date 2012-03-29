using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Posh.Helpers;
using Posh.Models;

namespace Posh.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {

        public ActionResult Create()
        {
            return View(new Category());
        }

        [HttpPost]
        public ActionResult Create(FormCollection form, HttpPostedFileBase uploadFile)
        {
            using (var context = new ModelContainer())
            {
                var category = new Category();
                
                TryUpdateModel(category, new[] { "Title", "SortOrder" });
                
                string fileName = IOHelper.GetUniqueFileName("~/Content/Images", uploadFile.FileName);
                string filePath = Server.MapPath("~/Content/Images");
                filePath = Path.Combine(filePath, fileName);
                uploadFile.SaveAs(filePath);
                
                category.ImageSource = fileName;
                
                context.AddToCategory(category);
                context.SaveChanges();
            }
            return RedirectToAction("Index", "Home", new { Area = "" });
        }

    }
}
