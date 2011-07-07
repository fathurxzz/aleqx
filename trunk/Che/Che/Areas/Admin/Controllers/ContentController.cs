using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Che.Models;

namespace Che.Areas.Admin.Controllers
{
    public class ContentController : Controller
    {
        //
        // GET: /Admin/Content/

        public ActionResult Add(int id)
        {
            var content = new Content {Id = id};

            return View(content);
        }
        
        [HttpPost]
        public ActionResult Add(int id, FormCollection form)
        {
            var content = new Content { Id = id };



            return View(content);
        }

        public ActionResult Edit(int id)
        {
            var content = new Content();

            return View(content);
        }

    }
}
