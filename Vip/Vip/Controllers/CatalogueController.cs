using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
    }
}
