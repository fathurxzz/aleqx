using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FashionIntention.UI.Models;

namespace FashionIntention.UI.Areas.Admin.Controllers
{
    public class SubscriberController : AdminController
    {
        private readonly SiteContext _context;

        public SubscriberController(SiteContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            var subscribers = _context.Subscribers.ToList();
            return View(subscribers);
        }

       

        public ActionResult Create()
        {
            return View(new Subscriber());
        }

        

        [HttpPost]
        public ActionResult Create(Subscriber model)
        {
            try
            {
                var subscriber = new Subscriber
                {
                    Email = model.Email,
                    IsActive = model.IsActive,
                    Guid = Guid.NewGuid().ToString("N")
                };

                _context.Subscribers.Add(subscriber);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

       

        public ActionResult Edit(int id)
        {
            var subscriber = _context.Subscribers.First(s => s.Id == id);
            return View(subscriber);
        }

       

        [HttpPost]
        public ActionResult Edit(int id, Subscriber model)
        {
            try
            {
                var subscriber = _context.Subscribers.First(s => s.Id == id);
                subscriber.Email = model.Email;
                subscriber.IsActive = model.IsActive;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

       
        public ActionResult Delete(int id)
        {
            var subscriber = _context.Subscribers.First(s => s.Id == id);
            _context.Subscribers.Remove(subscriber);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
