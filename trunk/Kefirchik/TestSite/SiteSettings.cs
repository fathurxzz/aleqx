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

        public static Dictionary<string, ThumbnailParameters> ThumbnailParams = new Dictionary<string, ThumbnailParameters>();

        static SiteSettings()
        {
            ThumbnailParams.Add("thumb1",new ThumbnailParameters{});
        }

        public static ThumbnailParameters GetThumnailParameters(string key)
        {

        }


    }
}