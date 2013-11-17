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
    public class SliderController : Controller
    {
        private SiteContext _context;

        public SliderController(SiteContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            var sliders = _context.Slider.ToList();
            return View(sliders);
        }

        public ActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Create(FormCollection form)
        {
            var slider = new Slider();
            TryUpdateModel(slider, new[] {"Url"});
            if (Request.Files["banner"] != null && !string.IsNullOrEmpty(Request.Files["banner"].FileName))
            {
                string fileName = IOHelper.GetUniqueFileName("~/Content/Images", Request.Files["banner"].FileName);
                string filePath = Server.MapPath("~/Content/Images");
                filePath = Path.Combine(filePath, fileName);
                Request.Files["banner"].SaveAs(filePath);
                slider.ImageSource = fileName;
            }
            _context.AddToSlider(slider);
            _context.SaveChanges();
            return RedirectToAction("Index", "Slider", new { area = "Admin" });
        }


        public ActionResult Edit(int id)
        {
            return View(_context.Slider.First(s => s.Id == id));
        }

        [HttpPost]
        public ActionResult Edit(FormCollection form)
        {
            int id = Convert.ToInt32(form["Id"]);
            var slider = _context.Slider.First(s => s.Id ==id);
            TryUpdateModel(slider, new[] { "Url" });
            if (Request.Files["banner"] != null && !string.IsNullOrEmpty(Request.Files["banner"].FileName))
            {
                ImageHelper.DeleteImage(slider.ImageSource);
                string fileName = IOHelper.GetUniqueFileName("~/Content/Images", Request.Files["banner"].FileName);
                string filePath = Server.MapPath("~/Content/Images");
                filePath = Path.Combine(filePath, fileName);
                Request.Files["banner"].SaveAs(filePath);
                slider.ImageSource = fileName;
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Slider", new { area = "Admin" });
        }

        public ActionResult Delete(int id)
        {
            var slider = _context.Slider.First(s => s.Id==id);
            ImageHelper.DeleteImage(slider.ImageSource);
            _context.DeleteObject(slider);
            _context.SaveChanges();
            return RedirectToAction("Index", "Slider", new { area = "Admin" });
        }

    }
}
