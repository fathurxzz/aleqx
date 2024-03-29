﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SiteExtensions;

namespace Filimonov.Models
{
    public class SiteModel : ISiteModel
    {
        public string Title { get; set; }
        public string SeoDescription { get; set; }
        public string SeoKeywords { get; set; }
        public Menu Menu { get; set; }
        public bool IsHomePage { get; set; }
        public string RandomSiteBgFileName { get; set; }

        public IEnumerable<Content> Contents { get; set; }
        //public IEnumerable<Project> Projects { get; set; }

        public SiteModel(SiteContainer context)
        {
            Title = "FILIMONOV INTERIOR LAB";
            Contents = context.Content.Include("Projects").ToList();

            //Projects = context.Project.ToList();

            SeoDescription = Contents.First().SeoDescription;
            SeoKeywords = Contents.First().SeoKeywords;

            var allBackgrounds = context.SiteBackground.ToList();
            var sitebg = allBackgrounds.OrderBy(s => Guid.NewGuid()).Take(1).FirstOrDefault();
            if (sitebg != null)
            {
                RandomSiteBgFileName = sitebg.ImageSource;
            }


        }
    }
}