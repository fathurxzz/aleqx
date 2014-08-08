using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shop.DataAccess;
using Shop.DataAccess.Repositories;
using Shop.WebSite.Controllers;

namespace Shop.WebSite.Areas.Admin.Controllers
{
    [Authorize]
    public class AdminController : DefaultController
    {


        public AdminController(IShopRepository repository)
        {
            
        }

        public ActionResult Default()
        {
            return View();
        }

    }
}
