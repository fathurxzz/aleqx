using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Listelli.Models;

namespace Listelli.Controllers
{
    public class HomeController : DefaultController
    {
        public ActionResult Index(string id)
        {
            using (var context = new SiteContainer())
            {
                Content content = context.Content.FirstOrDefault(c => c.Name == id);

                if (content == null)
                {
                    content = context.Content.First(c => c.MainPage); 
                }

                content.CurrentLang = CurrentLang.Id;
                return View(content);
            }
        }

        public ActionResult Gallery()
        {
            ViewBag.CurrentMenuItem = "gallery";
            return View();
        }

        public ActionResult BrandDetails()
        {
            ViewBag.CurrentMenuItem = "brand-details";
            return View();
        }
    }
}
