using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FashionIntention.UI.Models;
using Newtonsoft.Json;

namespace FashionIntention.UI.Controllers
{
    public class DefaultController : Controller
    {

        //protected readonly SiteContext _context;

        public DefaultController(SiteContext context)
        {

            ViewBag.Title = "Fashion intention";
            List<CalendarItem> calendar = context.Posts.Select(p => new CalendarItem { Year = p.Date.Year, Month = p.Date.Month }).ToList();
            var grouped = from c in calendar group c.Month by c.Year into g select new {year = g.Key, months = g.Distinct()};
            ViewBag.Calendar = "dataModels.calendar = " + JsonConvert.SerializeObject(grouped);
        }
    }
}
