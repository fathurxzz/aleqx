using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HavilaTravel.Models;

namespace HavilaTravel.Areas.Admin.Controllers
{
    public class ValidationController : Controller
    {
        //
        // GET: /Admin/Validation/

        public JsonResult IsNameAvailable(string name)
        {
            using (var context = new ContentStorage())
            {
                var content = context.Content.FirstOrDefault(c => c.Name == name);
                if (content !=null)
                {
                    return Json("Страница с таким именем уже существует, введите другое имя!", JsonRequestBehavior.AllowGet);
                }
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

    }
}
