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

                ViewBag.SelectedCountryItem = model.Content.Name;

                return View(model);
            }
        }

    }
}
