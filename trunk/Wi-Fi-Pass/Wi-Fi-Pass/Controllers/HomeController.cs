using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wi_Fi_Pass.Models;

namespace Wi_Fi_Pass.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Edit()
        {
            using (var context = new SiteContainer())
            {
                MapContent content = context.MapContent.First();
                return View(content);
            }
            
        }

        [HttpPost]
        public ActionResult Edit(FormCollection form)
        {
            using (var context = new SiteContainer())
            {
                MapContent content = context.MapContent.First();
                content.Text = HttpUtility.HtmlDecode(form["Text"]);
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }


        public ActionResult Index()
        {
            using (var context = new SiteContainer())
            {
                MapContent content = context.MapContent.First();
                return View(content);
            }
        }
    }
}
