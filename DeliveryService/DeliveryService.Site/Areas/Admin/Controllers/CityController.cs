using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DeliveryService.DataAccess.Models;

namespace DeliveryService.Site.Areas.Admin.Controllers
{
    public class CityController : Controller
    {
        //
        // GET: /Admin/City/

        public ActionResult Index()
        {
            using(var context = new SiteContext())
            {
                var cities = context.Cities
                    //.Include("Companies")
                    .ToList()
                    ;
                return View(cities);
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(City model)
        {
            using (var context = new SiteContext())
            {
                var city = new City();
                TryUpdateModel(city, new[] {"Name", "Title", "IsActive"});
                context.Cities.Add(city);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(int id)
        {
            using (var context = new SiteContext())
            {
                var city = context.Cities.First(c => c.Id == id);
                return View(city);
            }
        }

        [HttpPost]
        public ActionResult Edit(City model)
        {
            using (var context = new SiteContext())
            {
                var city = context.Cities.First(c => c.Id == model.Id);
                TryUpdateModel(city, new[] { "Name", "Title", "IsActive" });
                context.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Details(int id)
        {
            using (var context = new SiteContext())
            {
                var city = context.Cities.First(c => c.Id == id);
                return View(city);
            }
        }

        public ActionResult Delete(int id)
        {
            using (var context = new SiteContext())
            {
                var city = context.Cities.First(c => c.Id == id);
                context.Cities.Remove(city);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
        }

    }
}
