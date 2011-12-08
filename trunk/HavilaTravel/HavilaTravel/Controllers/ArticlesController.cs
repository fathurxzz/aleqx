using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HavilaTravel.Models;

namespace HavilaTravel.Controllers
{
    public class ArticlesController : Controller
    {
        //
        // GET: /Articles/

        public ActionResult Index()
        {
            using (var context = new ContentStorage())
            {
                SiteViewModel model = new SiteViewModel("articles", context, false)
                                          {
                                              Articles = context.Article.OrderBy(a => a.Date).ToList()
                                          };
                return View(model);
            }
        }

        public ActionResult Details(int id)
        {
            using (var context = new ContentStorage())
            {
                SiteViewModel model = new SiteViewModel("articles", context, false)
                {
                    Article = context.Article.Where(a => a.Id == id).First()
                };

                return View(model);
            }
        }

    }
}
