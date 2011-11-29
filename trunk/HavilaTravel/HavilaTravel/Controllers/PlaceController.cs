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

        private List<MenuItem> _placesMap = new List<MenuItem>();


        public void FillPlacesMap(Content content, ContentStorage context)
        {
            _placesMap.Add(new MenuItem(){Name = content.Name,Title = content.Title,SortOrder = (int)content.ContentLevel});
            if (content.Parent != null)
            {
                var parentId = content.Parent.Id;
                content = context.Content.Include("Parent").Where(c => c.Id == parentId).First();
                FillPlacesMap(content, context);
            }
        }


        


        public ActionResult Index(string id, bool? showSpa, bool? showPlacesReview)
        {
            using (var context = new ContentStorage())
            {

                var menuList = Menu.GetMenuList("countries", context);
                ViewBag.MenuList = menuList;

                if (string.IsNullOrEmpty(id))
                {
                    id = "Countries";
                }

                var content = context.Content
                    .Include("Parent").Include("Children").Include("Accordions")
                    .Where(c => c.Name == id)
                    .First();

                foreach (var accordion in content.Accordions)
                {
                    accordion.AccordionImages.Load();
                }

                FillPlacesMap(content, context);
                ViewBag.PlacesMap = _placesMap;

                ViewBag.PageTitle = content.PageTitle;
                ViewBag.SeoDescription = content.SeoDescription;
                ViewBag.SeoKeywords = content.SeoKeywords;
                ViewBag.CurrentContentId = content.Id;

                var banners = context.Banner.ToList();
                ViewBag.MainBanners = banners.Where(b => b.BannerType == 1).ToList();
                ViewBag.LeftBanner = banners.Where(b => b.BannerType == 2).ToList().GetRandomItem();
                ViewBag.RightBanner = banners.Where(b => b.BannerType == 3).ToList().GetRandomItem();

                var regionsAndCountries = context.Content.Include("Children").Where(c => c.PlaceKind == 1).ToList();
                ViewBag.SelectCountryMenu = regionsAndCountries;



                var spa = content.Children.Where(c => c.PlaceKind == 6).FirstOrDefault();
                if (spa != null && showSpa.HasValue && showSpa.Value)
                {
                    var spaId = spa.Id;
                    spa = context.Content
                    .Include("Accordions")
                    .Where(c => c.Id == spaId)
                    .First();
                    ViewBag.Spa = spa;
                }


                var review = content.Children.Where(c => c.PlaceKind == 7).FirstOrDefault();
                if (review != null && showPlacesReview.HasValue && showPlacesReview.Value)
                {
                    var reviewId = review.Id;
                    review = context.Content
                    .Include("Accordions")
                    .Where(c => c.Id == reviewId)
                    .First();
                    ViewBag.Review = review;
                }



                var placesLeftSubMenu = content.Children
                    .Where(p => p.PlaceKind != 6 && (p.PlaceKind == 5 && showSpa.HasValue || (!showSpa.HasValue && p.PlaceKind == 3 || !showSpa.HasValue && p.PlaceKind == 4)))
                    .Select(child => new MenuItem
                                                                             {
                                                                                 Id = (int)child.Id,
                                                                                 Name = child.Name,
                                                                                 SortOrder = child.SortOrder,
                                                                                 Title = child.Title
                                                                             }).ToList();
                //if (placesLeftSubMenu.Count > 0)
                //{
                //    ViewBag.PlacesLeftSubMenu = placesLeftSubMenu;
                //}

                if (content.PlaceKind == 5 || content.PlaceKind == 4)
                {
                    var parentId = content.Parent.Id;
                    var parent = context.Content.Include("Children").Where(c => c.Id == parentId).First();
                    placesLeftSubMenu = parent.Children
                        .Where(p => p.PlaceKind == 5 || p.PlaceKind == 4).Select(child => new MenuItem
                        {
                            Id = (int)child.Id,
                            Name = child.Name,
                            SortOrder = child.SortOrder,
                            Title = child.Title,
                            Selected = child.Id==content.Id
                        }).ToList();
                }

                if (placesLeftSubMenu.Count > 0)
                {
                    ViewBag.PlacesLeftSubMenu = placesLeftSubMenu;
                }



                var placesSelectorContent = content.Children
                    .Where(p => p.PlaceKind == 2 || p.PlaceKind == 3 || p.PlaceKind == 4)
                    .Select(child => new MenuItem
                    {
                        Id = (int)child.Id,
                        Name = child.Name,
                        SortOrder = child.SortOrder,
                        Title = child.Title
                    }).ToList();
                ViewBag.PlacesSelectorContent = placesSelectorContent;


                ViewBag.ShowSpa = (showSpa.HasValue && showSpa.Value);

                return View(content);
            }
        }

    }
}
