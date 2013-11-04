using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Penetron.Models
{
    public class BuildingModel : SiteModel
    {
        private readonly string _contentId;
        private readonly IList<Building> _buildings;
        public string ParentTitle { get; set; }
        public IList<SiteMenuItem> BuildingMenu { get; protected set; }

        public BuildingModel(SiteContext context, string contentId) : base(context, contentId)
        {
            _contentId = contentId;

            _buildings = context.Building.Include("Children").Include("BuildingObjs").ToList();

            Building = _buildings.FirstOrDefault(t => t.Name == contentId);

            if (Building != null)
            {
                if (Building.Parent != null)
                {
                    ParentTitle = Building.Parent.Title;
                }
                SeoDescription = Building.SeoDescription;
                SeoKeywords = Building.SeoKeywords;
            }
            GetMenu();
        }

        private void GetMenu()
        {
            BuildingMenu = new List<SiteMenuItem>();
            foreach (var technology in _buildings.OrderBy(t => t.SortOrder))
            {
                if (technology.Parent == null)
                {
                    BuildingMenu.Add(new SiteMenuItem { Name = technology.Name, Title = technology.Title, Parent = true, Id = technology.Id, HasChildren = technology.Children.Any() });

                    foreach (var child in technology.Children.OrderBy(t => t.SortOrder))
                    {
                        BuildingMenu.Add(new SiteMenuItem { Name = child.Name, Title = child.Title, Parent = false, Id = child.Id, Current = child.Name == _contentId });
                    }
                }
            }
        }
    }
}