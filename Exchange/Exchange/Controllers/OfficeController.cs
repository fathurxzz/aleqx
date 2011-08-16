using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Exchange.Models;
using Exchange.Models.Helpers;

namespace Exchange.Controllers
{
    public class OfficeController : Controller
    {
        //
        // GET: /Office/

        public ActionResult Index()
        {
            using (var conn = DbConnector.Connection)
            {
                IList<Office> offices = Office.GetOffices(conn, false, true);
                return View(offices);
            }
        }

    }
}
