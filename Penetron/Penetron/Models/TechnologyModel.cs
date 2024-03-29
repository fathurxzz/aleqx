﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Penetron.Models
{
    public class TechnologyModel : SiteModel
    {
        public IList<SiteMenuItem> TechnologyMenu { get; protected set; }
        private readonly string _contentId;
        private readonly IList<Technology> _technologies;
        public string ParentTitle { get; set; }
        private readonly string _categoryId;
        private readonly string _subCategoryId;

        public bool ActiveCategoryNotFound { get; set; }
        public string RedirectCategoryId { get; set; }
        public string RedirectSubCategoryId { get; set; }



        public TechnologyModel(SiteContext context, string categoryId, string subCategoryId)
            : base(context, null)
        {
            _categoryId = categoryId;
            _subCategoryId = subCategoryId;
            _contentId = subCategoryId ?? categoryId;

            _technologies = context.Technology.Include("Children").Include("TechnologyItems").ToList();

            foreach (var technologyItem in _technologies.SelectMany(item => item.TechnologyItems))
            {
                technologyItem.TechnologyImages.Load();
            }

            if (categoryId != null)
                if (subCategoryId == null)
                    ParentTitle = _technologies.Single(t => t.CategoryLevel == 0).Title;
            Technology = _technologies.FirstOrDefault(t => t.Name == _contentId) ?? _technologies.First(t => t.CategoryLevel == 0);



            if (!HttpContext.Current.Request.IsAuthenticated)
            {
                if (Technology.CategoryLevel == 0 && !Technology.Active)
                {
                    Technology = _technologies.FirstOrDefault(t => t.Parent == null && t.CategoryLevel != 0);
                    if (Technology != null)
                    {
                        ActiveCategoryNotFound = true;
                        RedirectCategoryId = Technology.Name;
                        return;
                    }
                }
                if (Technology != null)
                {
                    if (Technology.CategoryLevel != 0 && !Technology.Active && Technology.Children.Any())
                    {
                        ActiveCategoryNotFound = true;
                        RedirectCategoryId = Technology.Name;
                        RedirectSubCategoryId = Technology.Children.First().Name;
                        return;
                    }
                }
            }

            if (Technology != null)
            {
                if (Technology.Parent != null)
                {
                    ParentTitle = Technology.Parent.Title;
                }
                SeoDescription = Technology.SeoDescription;
                SeoKeywords = Technology.SeoKeywords;
            }


            GetMenu();

        }


        private void GetMenu()
        {
            TechnologyMenu = new List<SiteMenuItem>();
            foreach (var technology in _technologies.OrderBy(t => t.SortOrder))
            {
                if (technology.Parent == null && technology.CategoryLevel != 0)
                {
                    TechnologyMenu.Add(new SiteMenuItem
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
                        TechnologyMenu.Add(new SiteMenuItem
                                           {
                                               Name = child.Name,
                                               Title = child.Title,
                                               Parent = false,
                                               Id = child.Id,
                                               ParentId = technology.Name,
                                               Current = child.Name == _subCategoryId,
                                               Show = (technology.Name == _categoryId),
                                               SortOrder = child.SortOrder,
                                           });
                    }
                }
            }
        }



        public void Method<T>(T aaa)
        {

        }


    }
}