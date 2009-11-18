using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace bigs.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Bigs()
        {
            return View();
        }

        public ActionResult Services()
        {
            return View();
        }

        public ActionResult Request()
        {
            return View();
        }

        public ActionResult Capital()
        {
            return View();
        }

        public ActionResult Vacancy()
        {
            return View();
        }

        public ActionResult Contacts()
        {
            return View();
        }


        

        public ActionResult SetRussian(string returnUrl)
        {
            SystemSettings.CurrentLanguage = "ru-RU";
            if (!String.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        
        public ActionResult SetEnglish(string returnUrl)
        {
            SystemSettings.CurrentLanguage = "en-US";
            if (!String.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult SetItalian(string returnUrl)
        {
            SystemSettings.CurrentLanguage = "it-IT";
            if (!String.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }


}
