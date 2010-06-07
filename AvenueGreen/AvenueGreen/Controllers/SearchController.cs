using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using AvenueGreen.Models;

namespace AvenueGreen.Controllers
{
    public class SearchController : Controller
    {
        //
        // GET: /Search/

        public ActionResult Index(string searchString)
        {
            using (var context = new ContentStorage())
            {
                
            }

            return View();
        }

    }
}
