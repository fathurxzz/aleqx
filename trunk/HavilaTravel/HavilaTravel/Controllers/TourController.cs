using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HavilaTravel.Models;

namespace HavilaTravel.Controllers
{
    public class TourController : Controller
    {
        //
        // GET: /Tour/
        
        [OutputCache(NoStore = true, VaryByParam = "*", Duration = 1)]
        public ActionResult TourDetails(int id)
        {
            using (var context = new ContentStorage())
            {
                var tour = context.ActualTours.First(t => t.Id == id);
                return PartialView(tour);
            }
        }

    }
}
