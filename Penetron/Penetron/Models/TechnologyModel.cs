﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Penetron.Models
{
    public class TechnologyModel : SiteModel
    {
        public IList<SiteMenuItem> TechnologyMenu { get; protected set; }
        private readonly string _contentId;
        private readonly IList<Technology> _technologies;
        public string ParentTitle { get; set; }


        public TechnologyModel(SiteContext context, string contentId)
            : base(context, contentId)
        {
            _contentId = contentId;
            
            _technologies = context.Technology.Include("Children").ToList();

            

            Technology = _technologies.FirstOrDefault(t => t.Name == contentId);

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
                if (technology.Parent == null)
                {
                    TechnologyMenu.Add(new SiteMenuItem { Name = technology.Name, Title = technology.Title, Parent = true, Id = technology.Id, HasChildren = technology.Children.Any() });

                    foreach (var child in technology.Children.OrderBy(t => t.SortOrder))
                    {
                        TechnologyMenu.Add(new SiteMenuItem { Name = child.Name, Title = child.Title, Parent = false, Id = child.Id, Current = child.Name == _contentId });
                    }
                }
            }
        }
    }
}