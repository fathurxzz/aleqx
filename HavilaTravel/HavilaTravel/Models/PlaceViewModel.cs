using System.Collections.Generic;
using System.Linq;

namespace HavilaTravel.Models
{
    public class PlaceViewModel : SiteViewModel
    {
        private bool? _showSpa;
        private bool? _showPlacesReview;

        public List<MenuItem> PlacesMap { get; set; }
        public List<Content> RegionsAndCountries { get; set; }
        public Content Spa { get; set; }
        public Content PlaceReview { get; set; }
        public List<MenuItem> LeftSubMenuItems { get; set; }
        public List<MenuItem> PlacesForSelector { get; set; }
        

        public PlaceViewModel(string id, ContentStorage context, bool? showSpa,bool? showPlacesReview, bool loadContent = true )
            : base(id, context, loadContent)
        {
            _showSpa = showSpa;
            _showPlacesReview = showPlacesReview;

            PlacesMap = FillPlacesMap(Content, Context);

            RegionsAndCountries = Context.Content.Include("Children").Where(c => c.PlaceKind == 1).ToList();

            Spa= GetSpa();
            PlaceReview = GetPlaceReview();
            LeftSubMenuItems = GetLeftSubMenuItems();

            PlacesForSelector = GetPlacesForSelector();


        }

        
        private Content GetSpa()
        {
            var spa = Content.Children.Where(c => c.PlaceKind == 6).FirstOrDefault();
            if (spa != null && _showSpa.HasValue && _showSpa.Value)
            {
                var spaId = spa.Id;
                spa = Context.Content
                .Include("Accordions")
                .Where(c => c.Id == spaId)
                .First();
                return spa;
            }
            return null;
        }


        private Content GetPlaceReview()
        {
            var review = Content.Children.Where(c => c.PlaceKind == 7).FirstOrDefault();
            if (review != null && _showPlacesReview.HasValue && _showPlacesReview.Value)
            {
                var reviewId = review.Id;
                review = Context.Content
                .Include("Accordions")
                .Where(c => c.Id == reviewId)
                .First();
                return review;
            }
            return null;
        }

        private List<MenuItem> GetLeftSubMenuItems()
        {
            var placesLeftSubMenu = Content.Children
                    .Where(p => p.PlaceKind != 6 && (p.PlaceKind == 5 && _showSpa.HasValue || (!_showSpa.HasValue && p.PlaceKind == 3 || !_showSpa.HasValue && p.PlaceKind == 4)))
                    .Select(child => new MenuItem
                    {
                        Id = (int)child.Id,
                        Name = child.Name,
                        SortOrder = child.SortOrder,
                        Title = child.Title
                    }).ToList();



            if (Content.PlaceKind == 5 || Content.PlaceKind == 4)
            {
                var parentId = Content.Parent.Id;
                var parent = Context.Content.Include("Children").Where(c => c.Id == parentId).First();
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

            
                GetPlacesMap(content, context,map);
            return map;
        }

        private void GetPlacesMap(Content content, ContentStorage context,List<MenuItem> map)
        {
            if (content.Parent != null)
            {
                var parentId = content.Parent.Id;
                content = context.Content.Include("Parent").Where(c => c.Id == parentId).First();
                map.Add(new MenuItem
                {
                    Name = content.Name,
                    Title = content.Title,
                    SortOrder = (int)content.ContentLevel
                });
                GetPlacesMap(content, context,map);
            }
        }
    }
}