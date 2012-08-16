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
        public ActionResult ShowSelector(string selector)
        {
            using (var context = new SiteContainer())
            {
                switch (selector)
                {
                    case "layout":
                        var layouts = context.Layout.Include("Parent").Include("Children").ToList();
                        return PartialView("LayoutsSelector", layouts);
                    case "brand":
                        var brands = context.Brand.ToList();
                        return PartialView("BrandsSelector", brands);
                    case "maker":
                        var makers = context.Maker.ToList();
                        return PartialView("MakersSelector", makers);
                    case "attribute":
                        var attributes = context.ProductAttribute.ToList();
                        return PartialView("AttributesSelector", attributes);
                }
                throw new Exception("unknown selector type");
            }
        }



    }
}
