using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Daily.Models;

namespace Daily.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NewsDetails(int id)
        {
            using (var context = new ContentStorage())
            {
                var newsEntity = context.News.Where(n => n.Id == id).Select(n => n).FirstOrDefault();
                return View(newsEntity);    
            }
        }

    }
}
