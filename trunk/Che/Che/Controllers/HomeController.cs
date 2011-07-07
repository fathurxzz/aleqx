using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Che.Models;

namespace Che.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        public ActionResult Index(string id)
        {



            return View();
        }

        public ActionResult Content(string id, int contentType)
        {

            using (var context = new ContentStorage())
            {
                
            }

            if(!string.IsNullOrEmpty(id))
            {
                
            }


            return View("Index");
        }
    }
}
