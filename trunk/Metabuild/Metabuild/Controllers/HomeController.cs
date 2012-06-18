using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Metabuild.Models;

namespace Metabuild.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string id)
        {

            using (var context = new StructureContainer())
            {
                var menuList = Menu.GetMenuList(id, context);
                ViewBag.MenuList = menuList;


                var content = context.Content
                    .Include("Parent").Include("Children")
                    .Where(c => c.Name == id)
                    .FirstOrDefault();

                if (content == null)
                    content = context.Content
                        .Include("Parent").Include("Children")
                        .Where(c => c.MainPage)
                        .First();



                ViewBag.Title = content.Title;
                ViewBag.PageTitle = content.PageTitle;
                ViewBag.SeoDescription = content.SeoDescription;
                ViewBag.SeoKeywords = content.SeoKeywords;

                return View(content);
            }
        }
        
    }
}
