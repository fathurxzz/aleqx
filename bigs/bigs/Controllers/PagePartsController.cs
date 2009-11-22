using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using bigs.Models;
using System.Web.Script.Serialization;

namespace bigs.Controllers
{
    public class PagePartsController : Controller
    {
        //
        // GET: /PageParts/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Buttons()
        {
            using (DataStorage context = new DataStorage())
            {
                List<ButtonStatuses> buttons = (from button in context.ButtonStatuses where button.Language == SystemSettings.CurrentLanguage orderby button.SortOrder ascending select button).ToList();
                return View(buttons);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Buttons(FormCollection form)
        {
            using (DataStorage context = new DataStorage())
            {
                if (!string.IsNullOrEmpty(form["enablities"]))
                {
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    Dictionary<string, string> enables = serializer.Deserialize<Dictionary<string, string>>(form["enablities"]);

                    foreach (string key in enables.Keys)
                    {

                        int id = int.Parse(key);
                        ButtonStatuses sButton = (from button in context.ButtonStatuses where button.Id == id select button).First();
                        sButton.SwitchedOn = bool.Parse(enables[key]);
                    }
                    context.SaveChanges();
                }
                List<ButtonStatuses> buttons = (from button in context.ButtonStatuses where button.Language == SystemSettings.CurrentLanguage orderby button.SortOrder ascending select button).ToList();
                return View(buttons);
            }
        }
    }
}
