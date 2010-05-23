using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using AvenueGreen.Models;

namespace AvenueGreen.Controllers
{
    public class NewsController : Controller
    {
        //
        // GET: /News/

        public ActionResult Index()
        {
            ViewData["typeData"] = "news";

            using (var context = new ContentStorage())
            {
                var newsList = context.Article.Select(c => c).ToList();
                return View(newsList);
            }
        }
    }
}
