using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using SiteExtensions;

namespace SiteExtensionsTest.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            return View();
        }


        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase uploadFile)
        {
            //string fn = Regex.Replace(file.FileName, @"[^a-zA-Z0-9._]", "");

            string fileName = IOHelper.GetUniqueFileName("~/Content/Images", uploadFile.FileName);
            string filePath = Server.MapPath("~/Content/Images");
            filePath = Path.Combine(filePath, fileName);
            uploadFile.SaveAs(filePath);

            return View("Index");
        }
    }
}
