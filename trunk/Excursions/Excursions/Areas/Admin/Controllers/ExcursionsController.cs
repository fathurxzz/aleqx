using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Excursions.Helpers;
using Excursions.Models;

namespace Excursions.Areas.Admin.Controllers
{
    [Authorize]
    public class ExcursionsController : Controller
    {
        //
        // GET: /Admin/Excursions/

        [HttpGet]
        public ActionResult AddEdit(int? id)
        {
            Excursion excursion = null;
            if (id.HasValue)
            {
                using (var context = new ContentStorage())
                {
                    excursion = context.Excursion.Select(c => c).Where(c => c.Id == id).First();
                }
            }
            else
            {
                excursion = new Excursion();
            }
            return View(excursion);
        }





        // [HttpPost]
        //public ActionResult AddEdit(Excursion excursion, int? Id)
        //{

        //    excursion.Text = HttpUtility.HtmlDecode(excursion.Text);
        //    excursion.ShortDescription = HttpUtility.HtmlDecode(excursion.ShortDescription);
        //    using (var context = new ContentStorage())
        //    {
        //        string file = Request.Files["image"].FileName;
        //        if (!string.IsNullOrEmpty(file))
        //        {
        //            string newFileName = IOHelper.GetUniqueFileName("~/Content/Images", file);
        //            string filePath = Path.Combine(Server.MapPath("~/Content/Images"), newFileName);
        //            Request.Files["image"].SaveAs(filePath);
        //            excursion.ImageSource = newFileName;
        //        }

        //        if (Id.HasValue && Id.Value > 0)
        //        {
        //            excursion.Id = Id.Value;
        //            object originalItem;
        //            EntityKey entityKey = new EntityKey("ContentStorage.Excursion", "Id", excursion.Id);
        //            if (context.TryGetObjectByKey(entityKey, out originalItem))
        //            {
        //                context.ApplyPropertyChanges(entityKey.EntitySetName, excursion);
        //            }
        //        }
        //        else
        //        {
        //            context.AddToExcursion(excursion);
        //        }
        //        context.SaveChanges();
        //    }
        //    return RedirectToAction("Index", "Excursions", new { area = "" });
        //}

        [HttpPost]
        public ActionResult AddEdit(FormCollection form, int? Id)
        {
            using (var context = new ContentStorage())
            {
                Excursion excursion;

                if (Id.HasValue && Id.Value > 0)
                {
                    excursion = context.Excursion.Select(e => e).Where(e => e.Id == Id).First();
                }
                else
                {
                    excursion = new Excursion();
                }


                string[] fieldsToUpdate;
                string file = Request.Files["image"].FileName;
                if (!string.IsNullOrEmpty(file))
                {
                    string newFileName = IOHelper.GetUniqueFileName("~/Content/Images", file);
                    string filePath = Path.Combine(Server.MapPath("~/Content/Images"), newFileName);
                    Request.Files["image"].SaveAs(filePath);
                    excursion.ImageSource = newFileName;
                    fieldsToUpdate = new string[] { "Title", "ShortDescription", "Text", "Name", "Keywords", "Description", "Price", "SortOrder", "Popular", "ImageSource" };
                }
                else
                {
                    fieldsToUpdate = new string[] { "Title", "ShortDescription", "Text", "Name", "Keywords", "Description", "Price", "SortOrder", "Popular" };
                }

                TryUpdateModel(excursion, fieldsToUpdate, form.ToValueProvider());

                excursion.Text = HttpUtility.HtmlDecode(excursion.Text);
                excursion.ShortDescription = HttpUtility.HtmlDecode(excursion.ShortDescription);


                if (String.IsNullOrEmpty(excursion.Title))
                    ModelState.AddModelError("Title", "Title is required!");
                if (String.IsNullOrEmpty(excursion.ShortDescription))
                    ModelState.AddModelError("ShortDescription", "ShortDescription is required!");
                if (String.IsNullOrEmpty(excursion.Text))
                    ModelState.AddModelError("Text", "Text is required!");
                if (String.IsNullOrEmpty(excursion.Name))
                    ModelState.AddModelError("Name", "Name is required!");

                if (ModelState.IsValid)
                {
                    if (excursion.Id == 0)
                        context.AddToExcursion(excursion);

                    context.SaveChanges();
                    return RedirectToAction("Index", "Excursions", new { area = "" });
                }
                return View(excursion);
            }
        }


        public ActionResult Delete(int id)
        {
            using (var context = new ContentStorage())
            {
                Excursion excursion = context.Excursion.Select(e => e).Where(e => e.Id == id).First();
                context.DeleteObject(excursion);
                context.SaveChanges();
            }
            // ReSharper disable Asp.NotResolved
            return RedirectToAction("Index", "Excursions", new { area = "" });
            // ReSharper restore Asp.NotResolved
        }

    }
}
