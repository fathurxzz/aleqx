using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SiteExtensions;
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
                if (model.Category != null)
                    ViewBag.CategoryName = model.Category.Name;
                model.SetFilters();
                model.ApplyFilers();
                ViewBag.MainMenu = model.Menu;
                this.SetSeoContent(model);
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult Index(FormCollection form, string selector, string category)
        {
            using (var context = new SiteContainer())
            {
                var model = new CatalogueViewModel(context, category);
                if (model.Category != null)
                    ViewBag.CategoryName = model.Category.Name;
                switch (selector)
                {
                    case "layout":
                        WebSession.Layouts.Clear();
                        List<Layout> checkedLayouts = (from layout in model.Layouts where form["layout_" + layout.Id] == "true,false" select layout).ToList();
                        WebSession.Layouts.AddRange(checkedLayouts);
                        break;
                    case "brand":
                        WebSession.Brands.Clear();
                        List<Brand> checkedBrands = (from brand in model.Brands where form["brand_" + brand.Id] == "true,false" select brand).ToList();
                        WebSession.Brands.AddRange(checkedBrands);
                        break;
                    case "maker":
                        WebSession.Makers.Clear();
                        List<Maker> checkedMakers = (from maker in model.Makers where form["maker_" + maker.Id] == "true,false" select maker).ToList();
                        WebSession.Makers.AddRange(checkedMakers);
                        break;
                    case "attribute":
                        WebSession.Attributes.Clear();
                        List<ProductAttribute> checkedAttributes = (from attr in model.Attributes where form["attr_" + attr.Id] == "true,false" select attr).ToList();
                        WebSession.Attributes.AddRange(checkedAttributes);
                        break;
                }

                model.SetFilters();
                model.ApplyFilers();
                return View(model);
            }
        }

        [OutputCache(VaryByParam = "*", NoStore = true, Duration = 1)]
        public ActionResult ShowSelector(string selector, string category)
        {
            using (var context = new SiteContainer())
            {
                Category cat=null;
                if (category != null)
                {
                    cat = context.Category.Include("ProductAttributes").First(c => c.Name == category);
                    ViewBag.CategoryName = cat.Name;
                }
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
                        return PartialView("AttributesSelector", cat.ProductAttributes);
                }
                throw new Exception("unknown selector type");
            }
        }



    }
}
