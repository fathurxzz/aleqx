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

            ViewBag.Title = "Fashion Intension";
            List<CalendarItem> calendar = context.Posts.Select(p => new CalendarItem { Year = p.Date.Year, Month = p.Date.Month }).ToList();
            var grouped = from c in calendar group c.Month by c.Year into g select new {year = g.Key, months = g.Distinct()};
            ViewBag.Calendar = "dataModels.calendar = " + JsonConvert.SerializeObject(grouped);


            var tagList = new List<object>();
            var tags = context.Tags.ToList();
            foreach (var tag in tags)
            {
                tagList.Add(new { id = tag.Id, title = tag.Title });
            }
            ViewBag.Tags = "dataModels.tags = " + JsonConvert.SerializeObject(tagList);

            var bannerList = new List<object>();
            var banners = context.MainBanners.ToList();
            foreach (var banner in banners)
            {
                bannerList.Add(new { imageSrc = banner.ImageSrc, url = banner.Url});
            }
            ViewBag.MainBanners = "dataModels.mainBanners = " + JsonConvert.SerializeObject(bannerList);
           
        }
    }
}
