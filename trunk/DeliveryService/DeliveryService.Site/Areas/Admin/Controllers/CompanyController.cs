using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DeliveryService.DataAccess.Models;

namespace DeliveryService.Site.Areas.Admin.Controllers
{
    public class CompanyController : Controller
    {
        //
        // GET: /Admin/Company/

        public ActionResult Index()
        {
            using (var context = new SiteContext())
            {
                var companies = context.Companies.ToList();
                return View(companies);
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
                var company = new Company();
                TryUpdateModel(company, new[] { "Name", "Title", "IsActive","Description","ImageSource" });
                context.Companies.Add(company);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(int id)
        {
            using (var context = new SiteContext())
            {
                var company = context.Companies.First(c => c.Id == id);
                return View(company);
            }
        }

        [HttpPost]
        public ActionResult Edit(City model)
        {
            using (var context = new SiteContext())
            {
                var company = context.Companies.First(c => c.Id == model.Id);
                TryUpdateModel(company, new[] { "Name", "Title", "IsActive", "Description", "ImageSource" });
                context.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Details(int id)
        {
            using (var context = new SiteContext())
            {
                var company = context.Companies.First(c => c.Id == id);
                return View(company);
            }
        }

        public ActionResult Delete(int id)
        {
            using (var context = new SiteContext())
            {
                var company = context.Companies.First(c => c.Id == id);
                context.Companies.Remove(company);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
        }

    }
}
