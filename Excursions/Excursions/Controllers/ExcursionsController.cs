using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Excursions.Models;


namespace Excursions.Controllers
{
    public class ExcursionsController : Controller
    {
        //
        // GET: /Excursions/

        public ActionResult Index()
        {
            using (var context = new ContentStorage())
            {
                var excursionList = context.Excursion.Select(e => e).ToList();
                return View(excursionList);
            }
        }
    }
}
