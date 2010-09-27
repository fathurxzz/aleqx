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
            return View(excursion);
        }

        [HttpPost]
        public ActionResult AddEdit(Excursion excursion, int? Id)
        {
            
            excursion.Text = HttpUtility.HtmlDecode(excursion.Text);
            excursion.ShortDescription = HttpUtility.HtmlDecode(excursion.ShortDescription);
            using (var context = new ContentStorage())
            {
                if (Id.HasValue && Id.Value > 0)
                {
                    excursion.Id = Id.Value;
                    object originalItem;
                    EntityKey entityKey = new EntityKey("ContentStorage.Excursion", "Id", excursion.Id);
                    if (context.TryGetObjectByKey(entityKey, out originalItem))
                    {
                        context.ApplyPropertyChanges(entityKey.EntitySetName, excursion);
                    }
                }
                else
                {
                    
                    string file = Request.Files["image"].FileName;
                    if (!string.IsNullOrEmpty(file))
                    {
                        string newFileName = IOHelper.GetUniqueFileName("~/Content/Images", file);
                        string filePath = Path.Combine(Server.MapPath("~/Content/Images"), newFileName);
                        Request.Files["image"].SaveAs(filePath);
                        excursion.ImageSource = newFileName;
                        context.AddToExcursion(excursion);
                    }
                }
                context.SaveChanges();
            }
            return RedirectToAction("Index", "Excursions", new { area = "" });
        }


        public ActionResult Delete(int id)
        {

// ReSharper disable Asp.NotResolved
            return RedirectToAction("Index", "Excursions", new { area = "" });
// ReSharper restore Asp.NotResolved
        }

    }
}
