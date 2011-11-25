using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kefirchik.Graphics;

namespace TestSite
{
    public static class SiteSettings
    {
        public static Dictionary<string, ThumbnailDimensions> Thumbnails = new Dictionary<string, ThumbnailDimensions>();


        static SiteSettings()
        {
            Thumbnails.Add("thumb1", new ThumbnailDimensions {Height = 100, Width = 150});
        }


    }
}