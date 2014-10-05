using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shop.DataAccess.Repositories;

namespace Shop.WebSite.Areas.Admin.Controllers
{
    public class ShopSettingController : AdminController
    {
        //
        // GET: /Admin/ShopSetting/

        public ShopSettingController(IShopRepository repository) : base(repository)
        {
        }

        public ActionResult Index()
        {
            return View(_repository.GetShopSettings());
        }

    }
}
