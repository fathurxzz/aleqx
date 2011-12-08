using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HavilaTravel.Helpers;
using HavilaTravel.Models;

namespace HavilaTravel.Controllers
{
    public class PlaceController : Controller
    {
        //
        // GET: /Place/

        //private List<MenuItem> _placesMap = new List<MenuItem>();

        public ActionResult Index(string id, bool? showSpa, bool? showPlacesReview)
        {
            using (var context = new ContentStorage())
            {
                if (string.IsNullOrEmpty(id))
                    id = "Countries";
                PlaceViewModel model = new PlaceViewModel(id, context, showSpa, showPlacesReview);
                ViewBag.PageTitle = model.Content.PageTitle;
                ViewBag.SeoDescription = model.Content.SeoDescription;
                ViewBag.SeoKeywords = model.Content.SeoKeywords;

                //if (string.IsNullOrEmpty(id))
                //{
                //    id = "Countries";
                //}

                //var content = context.Content
                //    .Include("Parent").Include("Children").Include("Accordions")
                //    .Where(c => c.Name == id)
                //    .First();
                //FillPlacesMap(model.Content, context);
                //ViewBag.PlacesMap = model.PlacesMap;

                //ViewBag.Bellboy = context.Bellboy.GetRandomItem();

                /*var banners = context.Banner.ToList();
                ViewBag.MainBanners = banners.Where(b => b.BannerType == 1).ToList();
                ViewBag.LeftBanner = banners.Where(b => b.BannerType == 2).ToList().GetRandomItem();
                ViewBag.RightBanner = banners.Where(b => b.BannerType == 3).ToList().GetRandomItem();
                */
                
                //var regionsAndCountries = context.Content.Include("Children").Where(c => c.PlaceKind == 1).ToList();
                //ViewBag.SelectCountryMenu = regionsAndCountries;
                
                ViewBag.SelectedCountryItem = model.Content.Name;

                //ViewBag.ShowSpa = (showSpa.HasValue && showSpa.Value);

                return View(model);
            }
        }

    }
}
