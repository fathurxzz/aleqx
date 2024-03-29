﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SiteExtensions;

namespace Penetron.Models
{
    public class SiteModel : ISiteModel
    {
        public string Title { get; set; }
        public string SeoDescription { get; set; }
        public string SeoKeywords { get; set; }
        public Menu Menu { get; set; }
        public bool IsHomePage { get; set; }
        public Technology Technology { get; set; }
        public Building Building { get; set; }
        public Content Content { get; set; }
        public IEnumerable<Reason> Reasons { get; set; }
        public IEnumerable<Slider> Sliders { get; set; }


        public SiteModel(SiteContext context, string contentId)
        {
            Title = "ПЕНЕТРОН УКРАИНА";
            Content = context.Content.Include("ContentItems").FirstOrDefault(c => c.Name == contentId) ?? context.Content.First(c => c.MainPage);
            SeoDescription = Content.SeoDescription;
            SeoKeywords = Content.SeoKeywords;
            Reasons = context.Reason.ToList();
            Sliders = context.Slider.ToList();

        }

    }
}