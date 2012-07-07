using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rakurs.Controllers
{
    public class CatalogueController : Controller
    {
        //
        // GET: /Catalogue/

        public ActionResult Index(string category, string subCategory)
        {
            return View();
        }

    }
}
