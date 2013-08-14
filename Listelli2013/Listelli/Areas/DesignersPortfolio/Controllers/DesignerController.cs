using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using Listelli.Models;

namespace Listelli.Areas.DesignersPortfolio.Controllers
{
    public class DesignerController : Controller
    {
        
        public ActionResult Details(string id)
        {
            using (var context = new PortfolioContainer())
            {
                var designer = context.Designer.First(d => d.Name == id);
                return View(designer);
            }
        }

    }
}
