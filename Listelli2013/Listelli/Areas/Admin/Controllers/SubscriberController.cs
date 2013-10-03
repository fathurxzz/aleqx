using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Listelli.Models;

namespace Listelli.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrators")]
    public class SubscriberController : Controller
    {
        //
        // GET: /Admin/Subscriber/

        public ActionResult Index()
        {
            using (var context = new CustomerContainer())
            {
                var subscribers = context.Subscriber.ToList();
                Thread emailSending = (Thread)HttpContext.Application["mailSender"];
                ViewBag.EmailSendingStatus = emailSending != null ? "Active" : "Aborted";
                return View(subscribers);
            }
        }

        public ActionResult Edit(int id)
        {
            using (var context = new CustomerContainer())
            {
                var subscriber = context.Subscriber.First(s => s.Id == id);
                return View(subscriber);
            }
        }
        
        [HttpPost]
        public ActionResult Edit(Subscriber model)
        {
            using (var context = new CustomerContainer())
            {
                var subscriber = context.Subscriber.First(s => s.Id == model.Id);
                TryUpdateModel(subscriber, new[] {"Active","Email"});
                context.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        public ActionResult StartEmailSending()
        {
            //Thread emailSending = (Thread)HttpContext.Application["mailSender"];
            //if (emailSending.ThreadState == ThreadState.Aborted)
            //{
            //    emailSending.Start();
            //    HttpContext.Application["mailSender"] = emailSending;
            //}

            var newThread = new Thread(new ThreadStart(Listelli.Controllers.HomeController.ProcessSendEmail));
            newThread.Start();
            HttpContext.Application["mailSender"] = newThread;

            return RedirectToAction("Index");
        }

        public ActionResult StopEmailSending()
        {
            Thread emailSending = (Thread)HttpContext.Application["mailSender"];
            
            //if (emailSending.ThreadState == ThreadState.Running || emailSending.ThreadState == ThreadState.WaitSleepJoin)
            //{
            //    emailSending.Abort();
            //    HttpContext.Application["mailSender"] = emailSending;
            //}

            if (emailSending!=null && emailSending.IsAlive)
            {
                emailSending.Abort();
                HttpContext.Application["mailSender"] = null;
            }

            return RedirectToAction("Index");
        }


    }
}
