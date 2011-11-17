using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HavilaTravel.Models;

namespace HavilaTravel.Controllers
{
    public class PlaceController : Controller
    {
        //
        // GET: /Place/

        public ActionResult Index(string id)
        {
            using (var context = new ContentStorage())
            {

                var menuList = Menu.GetMenuList("countries", context);
                ViewBag.MenuList = menuList;

                if(string.IsNullOrEmpty(id))
                {
                    id = "Countries";
                }
                var content = context.Content
                    .Include("Parent").Include("Children").Include("Accordions")
                    .Where(c => c.Name == id)
                    .FirstOrDefault();



                var banners = context.Banner.ToList();
                ViewBag.Banners = banners;


                var regions = context.Content.Include("Children").Where(c => c.PlaceKind == 1).ToList();
                ViewBag.SelectCountryMenu = regions;




                return View(content);
            }
        }

    }
}
