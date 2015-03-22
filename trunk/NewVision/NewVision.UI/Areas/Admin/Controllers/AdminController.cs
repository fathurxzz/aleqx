using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewVision.UI.Models;

namespace NewVision.UI.Areas.Admin.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {

        public ActionResult Default()
        {
            return View();
        }
    }
}
