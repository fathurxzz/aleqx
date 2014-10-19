using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shop.DataAccess.Entities;
using Shop.DataAccess.Repositories;
using Shop.WebSite.Helpers;

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

        public ActionResult Edit(int id)
        {
            var setting = _repository.GetShopSetting(id);
            return View(setting);
        }

        [HttpPost]
        public ActionResult Edit(ShopSetting model)
        {
            var setting = _repository.GetShopSetting(model.Id);
            int result;
            if (int.TryParse(model.Value, out result))
            {
                setting.Value = model.Value;
                WebSession.ShopSettings = _repository.GetShopSettings().ToList();
            }

            _repository.SaveShopSettings(setting);
            return RedirectToAction("Index");

        }
    }
}
