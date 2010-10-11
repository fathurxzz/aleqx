using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Excursions.Models;

namespace Excursions.Areas.Admin.Controllers
{
    [Authorize]
    public class SettingsController : Controller
    {
        //
        // GET: /Admin/Settings/

        public ActionResult Index()
        {
            ViewData["NewCommentNotificationEmail"] = ApplicationData.NewCommentNotificationEmail;
            ViewData["FeebbackNotificationEmail"] = ApplicationData.FeedbackNotificationEmail;
            ViewData["Email"] = ApplicationData.Email;

            return View();
        }

        [HttpPost]
        public ActionResult Update(FormCollection form)
        {
            ApplicationData.NewCommentNotificationEmail = form["NewCommentNotificationEmail"];
            ApplicationData.FeedbackNotificationEmail = form["FeebbackNotificationEmail"];
            ApplicationData.Email = form["Email"];
            return RedirectToAction("Index");
        }
    }
}
