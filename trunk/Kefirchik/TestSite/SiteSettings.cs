using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kefirchik;


namespace TestSite
{
    public static class SiteSettings
    {
        public static Dictionary<string,GraphicsHelper.ThumbnailOptions> ThumbOptions = new Dictionary<string, GraphicsHelper.ThumbnailOptions>(); 
        
        static SiteSettings()
        {
            ThumbOptions.Add("thumb", new GraphicsHelper.ThumbnailOptions { OriginalPath = "~/Content/Images", Width = 150, Height = 250, CacheFolder = "thumb" });
        }
    }
}