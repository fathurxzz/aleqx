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

        [HttpGet]
        public ActionResult Index()
        {
            using (var context = new ContentStorage())
            {
                var excursionList = context.Excursion.Select(e => e).ToList();
                return View(excursionList);
            }
        }


        public ActionResult Details(int id)
        {
            using (var context = new ContentStorage())
            {
                var excursion = context.Excursion.Include("Comments").Select(e => e).Where(e => e.Id == id).First();
                return View(excursion);
            }
        }
    }
}
