using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rvk.Models;

namespace Rvk.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FeedbackForm()
        {
            return PartialView("FeedbackForm");
        }

        [HttpPost]
        public ActionResult FeedbackForm(FeedbackFormModel feedbackFormModel)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return PartialView("FeedbackForm", feedbackFormModel);
            }
        }
    }
}
