using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shop.Models;

namespace Shop.Areas.Admin.Controllers
{
    [Authorize]
    public class WishListController : Controller
    {
        //
        // GET: /Admin/WishList/

        public ActionResult Index()
        {
            using (var context = new ShopContainer())
            {
                var wishList = context.Wish.ToList();
                return View(wishList);
            }
        }
    }
}
