using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kiki.DataAccess.Repositories;
using Kiki.WebSite.Controllers;

namespace Kiki.WebSite.Areas.Admin.Controllers
{
    //[Authorize]
    public class AdminController : DefaultController
    {
        public AdminController(ISiteRepository repository) : base(repository)
        {

        }

        public ActionResult Index()
        {
            return View();
        }

    }
}
