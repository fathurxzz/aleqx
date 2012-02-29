using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HavilaTravel.Models;

namespace HavilaTravel.Areas.Admin.Controllers
{
    [Authorize]
    public class MailTemplateController : Controller
    {
        //
        // GET: /Admin/MailTemplate/

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(FormCollection form)
        {
            using (var context = new ContentStorage())
            {
                var template = new MailTemplate
                                   {
                                       Title = form["Title"], 
                                       Text = HttpUtility.HtmlDecode(form["Text"])
                                   };
                context.AddToMailTemplate(template);
                context.SaveChanges();
            }
            return RedirectToAction("Subscribers", "Subscribe", new { Area = "" });
        }

        public ActionResult Edit(int id)
        {
            using (var context = new ContentStorage())
            {
                var template = context.MailTemplate.First(t => t.Id == id);
                return View(template);
            }
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection form)
        {
            using (var context = new ContentStorage())
            {
                var template = context.MailTemplate.First(t => t.Id == id);
                template.Title = form["Title"];
                template.Text = HttpUtility.HtmlDecode(form["Text"]);
                context.SaveChanges();
            }
            return RedirectToAction("Subscribers", "Subscribe", new { Area = "" });
        }

        public ActionResult Delete(int id)
        {
            using (var context = new ContentStorage())
            {
                var template = context.MailTemplate.First(t => t.Id == id);
                context.DeleteObject(template);
                context.SaveChanges();
            }
            return RedirectToAction("Subscribers", "Subscribe", new { Area = "" });
        }
    }
}
