using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kiki.DataAccess.Repositories;

namespace Kiki.WebSite.Controllers
{
    public class HomeController : DefaultController
    {
        //
        // GET: /Home/

        public HomeController(ISiteRepository repository) : base(repository)
        {

        }

        public ActionResult Index()
        {
            return View();
        }

    }
}
