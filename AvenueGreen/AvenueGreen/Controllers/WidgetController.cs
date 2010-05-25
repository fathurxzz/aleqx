using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using AvenueGreen.Helpers;
using AvenueGreen.Models;

namespace AvenueGreen.Controllers
{
    [Authorize]
    public class WidgetController : Controller
    {
        //
        // GET: /Widget/

        public ActionResult Index(int type)
        {
            using (var context = new ContentStorage())
            {
                ViewData["type"] = type;
                var widgetList = context.Widgets.Select(w => w).Where(w => w.Type == type).ToList();
                return View(widgetList);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AddWidgetItem(int widgetType, string title)
        {
            ViewData["type"] = widgetType;
            string file = Request.Files["image"].FileName;
            if (!string.IsNullOrEmpty(file))
            {
                string newFileName = IOHelper.GetUniqueFileName("~/Content/WidgetImages", file);
                string filePath = Path.Combine(Server.MapPath("~/Content/WidgetImages"), newFileName);
                Request.Files["image"].SaveAs(filePath);
                using (var context = new ContentStorage())
                {
                    var widgetItem = new Widgets { Type = widgetType, ImageSource = newFileName, Title = title };
                    context.AddToWidgets(widgetItem);
                    context.SaveChanges();
                }
            }
            return RedirectToAction("Index", "Widgets");
        }

        public ActionResult DeleteWidgetItem(int widgetType,int id)
        {
            return RedirectToAction("Index", "Widgets", new {type = widgetType});
        }
    }
}
