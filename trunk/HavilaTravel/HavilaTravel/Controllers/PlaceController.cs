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

        public ActionResult Index(string id, bool? showSpa, bool? showPlacesReview, bool? showPrices)
        {
            using (var context = new ContentStorage())
            {
                if (string.IsNullOrEmpty(id))
                    id = "Countries";
                PlaceViewModel model = new PlaceViewModel(id, context, showSpa, showPlacesReview, showPrices,PlaceKind.Region);
                ViewBag.PageTitle = model.Content.PageTitle;
                ViewBag.SeoDescription = model.Content.SeoDescription;
                ViewBag.SeoKeywords = model.Content.SeoKeywords;
                ViewBag.CurrentContentId = model.CurrentContentId;

                ViewBag.SelectedCountryItem = model.Content.Name;
                ViewBag.PlaceKind = (int)PlaceKind.Region;
                return View(model);
            }
        }
        
        public ActionResult Spa(string id, bool? showSpa, bool? showPlacesReview, bool? showPrices)
        {
            using (var context = new ContentStorage())
            {
                if (string.IsNullOrEmpty(id))
                    id = "Spa";
                PlaceViewModel model = new PlaceViewModel(id, context, showSpa, showPlacesReview, showPrices,PlaceKind.SpaRegion);
                ViewBag.PageTitle = model.Content.PageTitle;
                ViewBag.SeoDescription = model.Content.SeoDescription;
                ViewBag.SeoKeywords = model.Content.SeoKeywords;
                ViewBag.CurrentContentId = model.CurrentContentId;

                ViewBag.SelectedCountryItem = model.Content.Name;
                ViewBag.PlaceKind = (int) PlaceKind.SpaRegion;
                return View("Index",model);
            }
        }

        

    }
}
