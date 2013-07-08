using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Listelli.Models;

namespace Listelli.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrators")]
    public class BrandGroupItemController : AdminController
    {
        public ActionResult CreateTextBlock(int brandId, int type)
        {
            using (var context = new SiteContainer())
            {
                var brand = context.BrandGroup.First(b => b.Id == brandId);
                ViewBag.BrandGroupName = brand.Name;
                int maxSortOrder = context.BrandGroupItem.Where(b => b.BrandGroupId == brand.Id).Max(c => (int?)c.SortOrder) ?? 0;
                return View(new BrandGroupItem { SortOrder = maxSortOrder + 1, ContentType = type, CurrentLang = CurrentLang.Id, BrandGroupId = brandId });
            }
        }

        [HttpPost]
        public ActionResult CreateTextBlock(BrandGroupItem model)
        {
            try
            {
                using (var context = new SiteContainer())
                {
                    var brand = context.BrandGroup.First(b => b.Id == model.BrandGroupId);
                    var cache = new BrandGroupItem
                    {
                        SortOrder = model.SortOrder,
                        ContentType = model.ContentType,
                        BrandGroup = brand
                    };

                    model.Text = HttpUtility.HtmlDecode(model.Text);

                    var lang = context.Language.FirstOrDefault(p => p.Id == model.CurrentLang);
                    if (lang != null)
                    {
                        CreateOrChangeContentLang(context, model, cache, lang);
                    }

                    return RedirectToAction("BrandGroupDetails", "Home", new { area = "BrandCatalogue", id = brand.Name });
                }
            }
            catch
            {
                return View();
            }
        }



        public ActionResult EditTextBlock(int id)
        {
            using (var context = new SiteContainer())
            {
                var brandItem = context.BrandGroupItem.Include("BrandGroup").First(b => b.Id == id);

                ViewBag.BrandName = brandItem.BrandGroup.Name;
                brandItem.CurrentLang = CurrentLang.Id;
                return View(brandItem);
            }
        }

        [HttpPost]
        public ActionResult EditTextBlock(BrandGroupItem model)
        {
            using (var context = new SiteContainer())
            {
                var cache = context.BrandGroupItem.Include("BrandGroup").First(p => p.Id == model.Id);
                //var brand = context.Brand.Include("BrandGroup").First(b => b.Id == model.BrandId);
                model.Text = HttpUtility.HtmlDecode(model.Text);
                //cache.Text = model.Text;

                TryUpdateModel(cache, new[] { "SortOrder" });

                var lang = context.Language.FirstOrDefault(p => p.Id == model.CurrentLang);
                if (lang != null)
                {
                    CreateOrChangeContentLang(context, model, cache, lang);
                }

                return RedirectToAction("BrandGroupDetails", "Home", new { area = "BrandCatalogue", id = cache.BrandGroup.Name });
            }
        }


        public ActionResult DeleteTextBlock(int id)
        {
            using (var context = new SiteContainer())
            {
                var brandItem = context.BrandGroupItem.Include("BrandGroup").Include("BrandGroupItemLangs").First(b => b.Id == id);
                var brand = context.BrandGroup.First(b => b.Id == brandItem.BrandGroupId);

                while (brandItem.BrandGroupItemLangs.Any())
                {
                    var bal = brandItem.BrandGroupItemLangs.First();
                    context.DeleteObject(bal);
                }
                context.DeleteObject(brandItem);
                context.SaveChanges();
                return RedirectToAction("BrandGroupDetails", "Home", new { area = "BrandCatalogue", id = brand.Name });
            }
        }



        private void CreateOrChangeContentLang(SiteContainer context, BrandGroupItem instance, BrandGroupItem cache, Language lang)
        {

            BrandGroupItemLang contenttLang = null;
            if (cache != null)
            {
                contenttLang = context.BrandGroupItemLang.FirstOrDefault(p => p.BrandGroupItemId == cache.Id && p.LanguageId == lang.Id);
            }
            if (contenttLang == null)
            {
                var newPostLang = new BrandGroupItemLang
                {
                    BrandGroupItemId = instance.Id,
                    LanguageId = lang.Id,
                    Text = instance.Text
                };
                context.AddToBrandGroupItemLang(newPostLang);
            }
            else
            {
                contenttLang.Text = instance.Text;
            }
            context.SaveChanges();

        }
    }
}
