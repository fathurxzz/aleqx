﻿using System;
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

        public bool ActiveCategoryNotFound { get; set; }
        public string RedirectCategoryId { get; set; }
        public string RedirectSubCategoryId { get; set; }


        public IList<BuildingObj> BuildingObjects { get; set; }

        public IList<Article> Articles { get; set; }
        public Article Article { get; set; }

        public BuildingModel(SiteContext context, string categoryId, string subCategoryId, int contentType, string articleId=null)
            : base(context, null)
        {
            _categoryId = categoryId;
            _subCategoryId = subCategoryId;
            _contentId = subCategoryId ?? categoryId;

            _buildings = context.Building.Include("Children").Include("BuildingItems").Where(b => b.ContentType == contentType).ToList();

            if (categoryId != null)
                if (subCategoryId == null)
                    ParentTitle = _buildings.Single(t => t.CategoryLevel == 0).Title;

            Building = _buildings.FirstOrDefault(t => t.Name == _contentId) ?? _buildings.First(t => t.CategoryLevel == 0);




            if (!HttpContext.Current.Request.IsAuthenticated)
            {
                if (Building.CategoryLevel == 0 && !Building.Active)
                {
                    Building = _buildings.Where(t => t.Parent == null && t.CategoryLevel != 0).Where(t => t.SortOrder == _buildings.Min(c => (int?)c.SortOrder)).FirstOrDefault();
                    //Building = _buildings.FirstOrDefault(t => t.Parent == null && t.CategoryLevel != 0);
                    if (Building != null)
                    {
                        ActiveCategoryNotFound = true;
                        RedirectCategoryId = Building.Name;
                        return;
                    }
                }
                if (Building != null)
                {
                    if (Building.CategoryLevel != 0 && !Building.Active && Building.Children.Any())
                    {
                        ActiveCategoryNotFound = true;
                        RedirectCategoryId = Building.Name;
                        RedirectSubCategoryId = Building.Children.First().Name;
                        return;
                    }
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
            if (contentType == 1)
            {
                BuildingObjects = context.BuildingObj.ToList();
            }


            if (articleId != null)
                Article = context.Article.First(a => a.Name == articleId);
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
                        Current = technology.Name == _categoryId,
                        CurrentParent = technology.Name == _contentId
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