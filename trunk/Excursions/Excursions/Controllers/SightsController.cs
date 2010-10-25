using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Excursions.Controllers
{
    public class SightsController : Controller
    {
        //
        // GET: /Sights/
        
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

    }
}
