using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
                //Match match = Regex.Match(name, "([a-z]|[A-Z]|[0-9])+");
                Match match = Regex.Match(name, @"^[a-z0-9_]{1,100}$");

                if (!match.Success)
                {
                    return Json("Введите только маленькие латинские символы и цифры без пробелов!", JsonRequestBehavior.AllowGet);
                }

                var content = context.Content.FirstOrDefault(c => c.Name == name);
                if (content != null)
                {
                    return Json("Страница с таким именем уже существует, введите другое имя!", JsonRequestBehavior.AllowGet);
                }
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

    }
}
