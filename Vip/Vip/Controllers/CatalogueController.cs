using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vip.Helpers;
using Vip.Models;

namespace Vip.Controllers
{
    public class CatalogueController : Controller
    {
        public ActionResult Index(string category)
        {
            using (var context = new SiteContainer())
            {
                var model = new CatalogueViewModel(context, category);
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            using (var context = new SiteContainer())
            {
                var model = new CatalogueViewModel(context, null);
                
                WebSession.Layouts.Clear();
                List<Layout> checkedLayouts = (from layout in model.Layouts where form["layout_" + layout.Id] == "true,false" select layout).ToList();
                WebSession.Layouts.AddRange(checkedLayouts);
                
                return View(model);
            }
        }

        [OutputCache(VaryByParam = "*", NoStore = true, Duration = 1)]
        public ActionResult ShowLayoutsSelector()
        {
            using (var context = new SiteContainer())
            {
                var layouts = context.Layout.Include("Parent").Include("Children").ToList();
                return PartialView("LayoutsSelector", layouts);
            }
        }

       

    }
}
