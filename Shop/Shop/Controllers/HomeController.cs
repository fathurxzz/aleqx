using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shop.Models;
using Shop.Helpers;

namespace Shop.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index(string id)
        {
            using (var context = new ShopContainer())
            {
                SiteViewModel model = new SiteViewModel(context, id);
                this.SetSeoContent(model);
                ViewBag.MainMenu = model.MainMenu;
                ViewBag.isHomePage = model.IsHomePage;
                return View(model);
            }
        }

        public ActionResult NotFound()
        {
            using (var context = new ShopContainer())
            {
                SiteViewModel model = new SiteViewModel(context, null);
                ViewBag.MainMenu = model.MainMenu;
                return View(model);
            }
        }

        [HttpPost]
        public PartialViewResult AddWish(WishFormModel wishFormModel)
        {
            if (ModelState.IsValid)
            {
                using (var context = new ShopContainer())
                {
                    var wish = new Wish();
                    TryUpdateModel(wish, new[] { "UserName", "Category", "Brand", "Title", "Size", "Color", "Phone", "Email" });
                    context.AddToWish(wish);
                    context.SaveChanges();
                }
                return PartialView("Success");
                
            }
            else
            {
                return PartialView("_WishForm", wishFormModel);
            }
        }

        [HttpPost]
        public ActionResult AddComment(CommentFormModel commentFormModel)
        {
            if (ModelState.IsValid)
            {
                using (var context = new ShopContainer())
                {

                }
                //return PartialView("Success");
                return RedirectToAction("Index", "Home", new {id = "about"});
            }
            else
            {
                //return PartialView("_CommentForm", commentFormModel);
                return RedirectToAction("Index", "Home", new { id = "about" });
            }
        }

    }
}
