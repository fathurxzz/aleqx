using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HavilaTravel.Helpers;
using HavilaTravel.Models;

namespace HavilaTravel.Areas.Admin.Controllers
{
    [Authorize]
    public class ActualToursController : Controller
    {

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection form, HttpPostedFileBase fileUpload)
        {
            using (var context = new ContentStorage())
            {
                if (fileUpload != null)
                {
                    string fileName = IOHelper.GetUniqueFileName("~/Content/ActualTours", fileUpload.FileName);
                    string filePath = Server.MapPath("~/Content/ActualTours");
                    filePath = Path.Combine(filePath, fileName);
                    fileUpload.SaveAs(filePath);

                    var tour = new ActualTours();
                    TryUpdateModel(tour, new[] { "Price", "Title", "Description", "Sign1", "Sign2", "Sign3", "Sign4", "Sign5" });
                    tour.Text = HttpUtility.HtmlDecode(form["Text"]);
                    tour.ImageSource = fileName;
                    context.AddToActualTours(tour);
                    context.SaveChanges();
                }

            }
            return RedirectToAction("Index", "Home", new { Area = "" });
        }



        public ActionResult Edit(int id)
        {
            using (var context = new ContentStorage())
            {
                var tour = context.ActualTours.Where(t => t.Id == id).First();
                return View(tour);
            }
        }

        [HttpPost]
        public ActionResult Edit(Banner model, FormCollection form, HttpPostedFileBase fileUpload)
        {
            using (var context = new ContentStorage())
            {
                var tour = context.ActualTours.Where(t => t.Id == model.Id).First();
                string fileName = tour.ImageSource;

                if (fileUpload != null)
                {
                    IOHelper.DeleteFile("~/Content/ActualTours", tour.ImageSource);

                    fileName = IOHelper.GetUniqueFileName("~/Content/ActualTours", fileUpload.FileName);
                    string filePath = Server.MapPath("~/Content/ActualTours");
                    filePath = Path.Combine(filePath, fileName);
                    fileUpload.SaveAs(filePath);
                }
                TryUpdateModel(tour, new[] { "Price", "Title", "Description", "Sign1", "Sign2", "Sign3", "Sign4", "Sign5" });
                tour.Text = HttpUtility.HtmlDecode(form["Text"]);
                tour.ImageSource = fileName;
                context.SaveChanges();
            }
            return RedirectToAction("Index", "Home", new { Area = "" });
        }

        public ActionResult Delete(int id)
        {
            using (var context = new ContentStorage())
            {
                var tour = context.ActualTours.Where(t => t.Id == id).First();
                IOHelper.DeleteFile("~/Content/ActualTours", tour.ImageSource);
                context.DeleteObject(tour);
                context.SaveChanges();
            }
            return RedirectToAction("Index", "Home", new { Area = "" });
        }



    }
}
