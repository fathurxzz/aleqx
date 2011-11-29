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
        public static Dictionary<string,ThumbnailOptions> ThumbOptions = new Dictionary<string, ThumbnailOptions>(); 
        
        static SiteSettings()
        {
            ThumbOptions.Add("thumb", new ThumbnailOptions { OriginalPath = "~/Content/Images", Width = 250, Height = 200, CacheFolder = "thumb" });
        }
    }
}