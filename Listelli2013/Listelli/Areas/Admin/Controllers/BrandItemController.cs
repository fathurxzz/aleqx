using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Listelli.Helpers;
using Listelli.Models;
using SiteExtensions;
using SiteExtensions.Graphics;

namespace Listelli.Areas.Admin.Controllers
{
    public class BrandItemController : AdminController
    {
        public ActionResult CreateTextBlock(int brandId, int type)
        {
            using (var context = new SiteContainer())
            {
                var brand = context.Brand.First(b => b.Id == brandId);
                ViewBag.BrandName = brand.Name;
                int maxSortOrder = context.BrandItem.Where(b => b.BrandId == brand.Id).Max(c => (int?)c.SortOrder) ?? 0;
                return View(new BrandItem { SortOrder = maxSortOrder + 1, ContentType = type, CurrentLang = CurrentLang.Id, BrandId = brandId });
            }
        }

        [HttpPost]
        public ActionResult CreateTextBlock(BrandItem model)
        {
            try
            {
                using (var context = new SiteContainer())
                {
                    var brand = context.Brand.First(b => b.Id == model.BrandId);
                    var cache = new BrandItem
                    {
                        SortOrder = model.SortOrder,
                        ContentType = model.ContentType,
                        Brand = brand
                    };

                    model.Text = HttpUtility.HtmlDecode(model.Text);

                    var lang = context.Language.FirstOrDefault(p => p.Id == model.CurrentLang);
                    if (lang != null)
                    {
                        CreateOrChangeContentLang(context, model, cache, lang);
                    }

                    return RedirectToAction("BrandDetails", "Home", new { area = "", id = brand.Name });
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
                var brandItem = context.BrandItem.Include("Brand").First(b => b.Id == id);
                ViewBag.BrandName = brandItem.Brand.Name;
                brandItem.CurrentLang = CurrentLang.Id;
                return View(brandItem);
            }
        }

        [HttpPost]
        public ActionResult EditTextBlock(BrandItem model)
        {
            using (var context = new SiteContainer())
            {
                var cache = context.BrandItem.Include("Brand").First(p => p.Id == model.Id);

                model.Text = HttpUtility.HtmlDecode(model.Text);
                //cache.Text = model.Text;

                TryUpdateModel(cache, new[] { "SortOrder" });

                var lang = context.Language.FirstOrDefault(p => p.Id == model.CurrentLang);
                if (lang != null)
                {
                    CreateOrChangeContentLang(context, model, cache, lang);
                }


                return RedirectToAction("BrandDetails", "Home", new { area = "", id = cache.Brand.Name });
            }
        }

        public ActionResult DeleteTextBlock(int id)
        {
            using (var context = new SiteContainer())
            {
                var brandItem = context.BrandItem.Include("Brand").Include("BrandItemLangs").First(b => b.Id == id);
                var brandName = brandItem.Brand.Name;
                while (brandItem.BrandItemLangs.Any())
                {
                    var bal = brandItem.BrandItemLangs.First();
                    context.DeleteObject(bal);
                }
                context.DeleteObject(brandItem);
                context.SaveChanges();
                return RedirectToAction("BrandDetails", "Home", new { area = "", id = brandName });
            }
        }



        public ActionResult CreateImagesBlock(int brandId)
        {
            using (var context = new SiteContainer())
            {
                var brand = context.Brand.First(b => b.Id == brandId);
                ViewBag.BrandName = brand.Name;
                int maxSortOrder = context.BrandItem.Where(b => b.BrandId == brand.Id).Max(c => (int?)c.SortOrder) ?? 0;
                return View(new BrandItem { SortOrder = maxSortOrder + 1, BrandId = brandId });
            }
        }

        [HttpPost]
        public ActionResult CreateImagesBlock(BrandItem model)
        {
            using (var context = new SiteContainer())
            {
                var brand = context.Brand.First(b => b.Id == model.BrandId);
                var brandItem = new BrandItem {ContentType = 3, Brand = brand, SortOrder = model.SortOrder};

                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var file = Request.Files[i];

                    if (file == null) continue;
                    if (string.IsNullOrEmpty(file.FileName)) continue;

                    var ci = new BrandItemImage();
                    string fileName = IOHelper.GetUniqueFileName("~/Content/Images", file.FileName);
                    string filePath = Server.MapPath("~/Content/Images");

                    filePath = Path.Combine(filePath, fileName);
                    GraphicsHelper.SaveOriginalImage(filePath, fileName, file, 1500);

                    ci.ImageSource = fileName;
                    brandItem.BrandItemImages.Add(ci);
                }

                context.SaveChanges();


                return RedirectToAction("BrandDetails", "Home", new { area = "", id = brand.Name });
            }
        }


        public ActionResult EditImagesBlock(int id)
        {
            using (var context = new SiteContainer())
            {
                var brandItem = context.BrandItem.Include("Brand").First(b => b.Id == id);
                ViewBag.BrandName = brandItem.Brand.Name;
                return View(brandItem);
            }
        }

        [HttpPost]
        public ActionResult EditImagesBlock(BrandItem model)
        {
            using (var context = new SiteContainer())
            {
                var brandItem = context.BrandItem.Include("Brand").First(b => b.Id == model.Id);

                TryUpdateModel(brandItem, new[] {"SortOrder"});

                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var file = Request.Files[i];

                    if (file == null) continue;
                    if (string.IsNullOrEmpty(file.FileName)) continue;

                    var ci = new BrandItemImage();
                    string fileName = IOHelper.GetUniqueFileName("~/Content/Images", file.FileName);
                    string filePath = Server.MapPath("~/Content/Images");

                    filePath = Path.Combine(filePath, fileName);
                    GraphicsHelper.SaveOriginalImage(filePath, fileName, file, 1500);

                    ci.ImageSource = fileName;
                    brandItem.BrandItemImages.Add(ci);
                }

                context.SaveChanges();

                return RedirectToAction("BrandDetails", "Home", new { area = "", id = brandItem.Brand.Name });
            }
        }




        public ActionResult DeleteImagesBlock(int id)
        {
            using (var context = new SiteContainer())
            {
                var brandItem = context.BrandItem.Include("Brand").Include("BrandItemImages").First(b => b.Id ==id);
                var brandName = brandItem.Brand.Name;

                while (brandItem.BrandItemImages.Any())
                {
                    var image = brandItem.BrandItemImages.First();
                    ImageHelper.DeleteImage(image.ImageSource);
                    context.DeleteObject(image);
                }

                context.DeleteObject(brandItem);
                context.SaveChanges();

                return RedirectToAction("BrandDetails", "Home", new { area = "", id = brandName });
            }
        }




        private void CreateOrChangeContentLang(SiteContainer context, BrandItem instance, BrandItem cache, Language lang)
        {

            BrandItemLang contenttLang = null;
            if (cache != null)
            {
                contenttLang = context.BrandItemLang.FirstOrDefault(p => p.BrandItemId == cache.Id && p.LanguageId == lang.Id);
            }
            if (contenttLang == null)
            {
                var newPostLang = new BrandItemLang
                {
                    BrandItemId = instance.Id,
                    LanguageId = lang.Id,
                    Text = instance.Text
                };
                context.AddToBrandItemLang(newPostLang);
            }
            else
            {
                contenttLang.Text = instance.Text;
            }
            context.SaveChanges();

        }
    }
}
