using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rakurs.Helpers;
using Rakurs.Models;

namespace Rakurs.Controllers
{
    public class CatalogueController : Controller
    {
        //
        // GET: /Catalogue/

        public ActionResult Index(string category, string subCategory, int? filter)
        {
            using (var context = new StructureContainer())
            {
                var model = new CatalogueViewModel(context, category, subCategory, filter);
                this.SetSeoContent(model);
                ViewBag.MainMenu = model.MainMenu;
                ViewBag.CategoryName = model.Category.Name;
                ViewBag.Filter = model.CurrentFilterId;
                return View(model);
            }
        }

    }
}
