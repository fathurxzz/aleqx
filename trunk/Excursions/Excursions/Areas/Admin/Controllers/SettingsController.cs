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
            ViewData["NotificationEmail"] = ApplicationData.NotificationEmail;
            return View();
        }

        [HttpPost]
        public ActionResult Update(FormCollection form)
        {
            ApplicationData.NotificationEmail = form["NotificationEmail"];

            return RedirectToAction("Index");
        }
    }
}
