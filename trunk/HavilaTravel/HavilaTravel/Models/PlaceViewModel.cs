using System.Collections.Generic;
using System.Linq;
using HavilaTravel.Helpers;

namespace HavilaTravel.Models
{
    public class PlaceViewModel : SiteViewModel
    {
        private bool? _showSpa;
        private bool? _showPlacesReview;
        private bool? _showPrices;

        public List<MenuItem> PlacesMap { get; set; }
        public List<Content> RegionsAndCountries { get; set; }
        public Content Spa { get; set; }
        public Content PlaceReview { get; set; }
        public Content PriceTable { get; set; }
        public List<MenuItem> LeftSubMenuItems { get; set; }
        public List<MenuItem> PlacesForSelector { get; set; }
        public List<MenuItem> ResortsForSelector { get; set; }
        


        public PlaceViewModel(string id, ContentStorage context, bool? showSpa, bool? showPlacesReview, bool? showPrices)
            : base(id, context,false, true)
        {
            
            _showSpa = showSpa;
            _showPlacesReview = showPlacesReview;
            _showPrices = showPrices;

            PlacesMap = FillPlacesMap(Content, Context);

            RegionsAndCountries = Context.Content.Include("Children").Where(c => c.PlaceKind == (int)WebSession.PlaceKind).ToList();

            Spa = GetSpa();
            PlaceReview = GetPlaceReview();
            PriceTable = GetPriceTable();

            LeftSubMenuItems = GetLeftSubMenuItems();
            PlacesForSelector = GetPlacesForSelector();
            ResortsForSelector = GetResortsSelector();
        }


        private Content GetSpa()
        {
            if (_showSpa.HasValue && _showSpa.Value)
            {
                PlaceKind = PlaceKind.SpaInfo;
                var spa = Content.Children.FirstOrDefault(c => c.PlaceKind == (int)PlaceKind.SpaInfo);
                if (spa != null)
                {
                    var spaId = spa.Id;
                    spa = Context.Content.Include("Accordions").First(c => c.Id == spaId);
                    foreach (var accordion in spa.Accordions)
                    {
                        accordion.AccordionImages.Load();
                    }
                    CurrentContentId = (int)spa.Id;
                    return spa;
                }
            }
            return null;
        }

        private Content GetPriceTable()
        {
            if (_showPrices.HasValue && _showPrices.Value)
            {
                PlaceKind=PlaceKind.PriceTable;
                var price = Content.Children.FirstOrDefault(c => c.PlaceKind == (int)PlaceKind.PriceTable);
                if (price != null)
                {
                    var priceId = price.Id;
                    price = Context.Content.Include("Accordions").First(c => c.Id == priceId);
                    foreach (var accordion in price.Accordions)
                    {
                        accordion.AccordionImages.Load();
                    }
                    CurrentContentId = (int)price.Id;
                    return price;
                }
            }
            return null;
        }


        private Content GetPlaceReview()
        {
            if (_showPlacesReview.HasValue && _showPlacesReview.Value)
            {
                PlaceKind=PlaceKind.PlacesReview;
                var review = Content.Children.FirstOrDefault(c => c.PlaceKind == (int)PlaceKind.PlacesReview);
                if (review != null)
                {
                    var reviewId = review.Id;
                    review = Context.Content.Include("Accordions").First(c => c.Id == reviewId);
                    foreach (var accordion in review.Accordions)
                    {
                        accordion.AccordionImages.Load();
                    }
                    CurrentContentId = (int)review.Id;
                    return review;
                }
            }
            return null;
        }

        private List<MenuItem> GetLeftSubMenuItems()
        {
            /*
            var placesLeftSubMenu = Content.Children
                    .Where(p => p.PlaceKind != 6 && (p.PlaceKind == (int)PlaceKind.Spa && _showSpa.HasValue || ( !_showSpa.HasValue && p.PlaceKind == (int)PlaceKind.Hotel)))
                    .Select(child => new MenuItem
                    {
                        Id = (int)child.Id,
                        Name = child.Name,
                        SortOrder = child.SortOrder,
                        Title = child.Title
                    }).ToList();



            if (Content.PlaceKind.In(new[] { (int)PlaceKind.Spa, (int)PlaceKind.Hotel }))
            {
                var parentId = Content.Parent.Id;
                var parent = Context.Content.Include("Children").First(c => c.Id == parentId);
                placesLeftSubMenu = parent.Children
                    .Where(p => p.PlaceKind == 5 || p.PlaceKind == 4).Select(child => new MenuItem
                    {
                        Id = (int)child.Id,
                        Name = child.Name,
                        SortOrder = child.SortOrder,
                        Title = child.Title,
                        Selected = child.Id == Content.Id
                    }).ToList();
            }
            */

            var placesLeftSubMenu = new List<MenuItem>();

            if (Content.PlaceKind.In(new[] { (int)PlaceKind.City, (int)PlaceKind.Resort }))
            {

                 placesLeftSubMenu = Content.Children
                    .Select(child => new MenuItem
                    {
                        Id = (int)child.Id,
                        Name = child.Name,
                        SortOrder = child.SortOrder,
                        Title = child.Title
                    }).ToList();
               
            }

            if (Content.PlaceKind.In(new[] { (int)PlaceKind.Spa}))
            {
                var parentId = Content.Parent.Id;
                var parent = Context.Content.Include("Children").First(c => c.Id == parentId);
                placesLeftSubMenu = parent.Children
                    .Where(p =>p.PlaceKind == 5)
                    .Select(child => new MenuItem
                    {
                        Id = (int)child.Id,
                        Name = child.Name,
                        SortOrder = child.SortOrder,
                        Title = child.Title,
                        Selected = child.Id == Content.Id
                    }).ToList();
            }


            if (placesLeftSubMenu.Count > 0)
            {
                return placesLeftSubMenu;
            }
            return null;
        }

        private List<MenuItem> GetPlacesForSelector()
        {
            var placesSelectorContent = Content.Children
                  .Where(p => p.PlaceKind == 2 || p.PlaceKind == 3 || p.PlaceKind == 4)
                  .Select(child => new MenuItem
                  {
                      Id = (int)child.Id,
                      Name = child.Name,
                      SortOrder = child.SortOrder,
                      Title = child.Title
                  }).ToList();
            return placesSelectorContent;
        }

        private List<MenuItem> GetResortsSelector()
        {
            var placesSelectorContent = Content.Children
                  .Where(p => p.PlaceKind == 12)
                  .Select(child => new MenuItem
                  {
                      Id = (int)child.Id,
                      Name = child.Name,
                      SortOrder = child.SortOrder,
                      Title = child.Title
                  }).ToList();
            return placesSelectorContent;
        }




        private List<MenuItem> FillPlacesMap(Content content, ContentStorage context)
        {
            var map = new List<MenuItem>
                            {
                                new MenuItem
                                    {
                                        Name = content.Name, 
                                        Title = content.Title, 
                                        SortOrder = (int) content.ContentLevel
                                    }
                            };


            GetPlacesMap(content, context, map);
            return map;
        }

        private void GetPlacesMap(Content content, ContentStorage context, List<MenuItem> map)
        {
            if (content.Parent != null)
            {
                var parentId = content.Parent.Id;
                content = context.Content.Include("Parent").First(c => c.Id == parentId);
                map.Add(new MenuItem
                {
                    Name = content.Name,
                    Title = content.Title,
                    SortOrder = (int)content.ContentLevel
                });
                GetPlacesMap(content, context, map);
            }
        }
    }
}