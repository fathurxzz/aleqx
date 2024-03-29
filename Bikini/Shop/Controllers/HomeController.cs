﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shop.Models;
using SiteExtensions;

namespace Shop.Controllers
{
    public class HomeController : DefaultController
    {
        public ActionResult Index(string id)
        {
            using (var context = new ShopContainer())
            {
                var model = new SiteModel(context, id??"");
                ViewBag.IsHomePage = model.IsHomePage;
                ViewBag.MainMenu = model.Menu;
                ViewBag.CatalogueMenu = model.CatalogueMenu;
                this.SetSeoContent(model);
                //ViewBag.CurrentMenuItem = model.Content.Name;
                return View(model);
            }
        }

        public ActionResult Category(string id)
        {
            using (var context = new ShopContainer())
            {
                var model = new ShopModel(context, id, null);
                ViewBag.MainMenu = model.Menu;
                ViewBag.CatalogueMenu = model.CatalogueMenu;
                this.SetSeoContent(model);
                ViewBag.CurrentCategoryMenuItem = model.Category.Name;
                //ViewBag.HeaderClassName = "pink";
                return View(model);
            }
        }

        public ActionResult ProductDetails(string categoryId, int id)
        {
            using (var context = new ShopContainer())
            {
                var model = new ShopModel(context, categoryId,id);
                ViewBag.MainMenu = model.Menu;
                ViewBag.CatalogueMenu = model.CatalogueMenu;
                this.SetSeoContent(model);
                ViewBag.CurrentCategoryMenuItem = model.Category.Name;
                //ViewBag.HeaderClassName = "pink";
                return View(model);
            }
        }
    }
}
