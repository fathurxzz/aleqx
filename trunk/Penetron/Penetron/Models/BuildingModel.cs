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
        private readonly string _categoryId;
        private readonly string _subCategoryId;

        public IList<BuildingObj> BuildingObjects { get; set; }

        public BuildingModel(SiteContext context, string categoryId, string subCategoryId)
            : base(context, null)
        {
            _categoryId = categoryId;
            _subCategoryId = subCategoryId;
            _contentId = subCategoryId ?? categoryId;

            _buildings = context.Building.Include("Children").ToList();

            Building = _buildings.First(t => t.Name == _contentId || t.CategoryLevel == 0);

            if (Building.CategoryLevel == 0 && !Building.Active)
            {
                Building = _buildings.FirstOrDefault(t => t.Parent != null);
                if (Building != null)
                {
                    _contentId = Building.Name;
                }
            }

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

            BuildingObjects = context.BuildingObj.ToList();

        }

        private void GetMenu()
        {
            BuildingMenu = new List<SiteMenuItem>();
            foreach (var technology in _buildings.OrderBy(t => t.SortOrder))
            {
                if (technology.Parent == null && technology.CategoryLevel != 0)
                {
                    BuildingMenu.Add(new SiteMenuItem
                    {
                        Name = technology.Name,
                        Title = technology.Title,
                        Parent = true,
                        Id = technology.Id,
                        HasChildren = technology.Children.Any(),
                        ContentActive = technology.Active,
                        SortOrder = technology.SortOrder,
                        Current = technology.Name == _categoryId
                    });

                    foreach (var child in technology.Children.OrderBy(t => t.SortOrder))
                    {
                        BuildingMenu.Add(new SiteMenuItem
                        {
                            Name = child.Name,
                            Title = child.Title,
                            Parent = false,
                            Id = child.Id,
                            ParentId = technology.Name,
                            Current = child.Name == _contentId,
                            Show = (technology.Name == _categoryId),
                            SortOrder = child.SortOrder,
                        });
                    }
                }
            }
        }
    }
}