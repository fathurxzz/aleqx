using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shop.WebSite.Controllers;

namespace Shop.WebSite.Areas.Admin.Controllers
{
    public class AdminController : DefaultController
    {
        public ActionResult Default()
        {
            return View();
        }

    }
}
