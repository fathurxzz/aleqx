using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using AvenueGreen.Models;

namespace AvenueGreen.Controllers
{
    public class ContentController : Controller
    {
        //
        // GET: /Content/

        public ActionResult Index()
        {
            using (ContentStorage context = new ContentStorage())
            {
                Content content = context.Content.Select(c => c).FirstOrDefault(); 
                return View(content);
            }
        }

    }
}
