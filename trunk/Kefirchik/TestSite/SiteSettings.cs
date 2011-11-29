﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kefirchik;


namespace TestSite
{
    public static class SiteSettings
    {
        public static Dictionary<string,ThumbnailOptions> ThumbOptions = new Dictionary<string, ThumbnailOptions>(); 
        
        static SiteSettings()
        {
            ThumbOptions.Add("thumb", new ThumbnailOptions { OriginalPath = "~/Content/Images", Width = 150, Height = 250, CacheFolder = "thumb" });
        }
    }
}